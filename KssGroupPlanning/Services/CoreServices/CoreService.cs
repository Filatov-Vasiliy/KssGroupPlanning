using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.EntityInterfaces;

public class CoreService
{
    private readonly IProductRepository _productRepository;
    private readonly IWorkingPeriodRepository _workingPeriodRepository;
    private readonly IWorkingPeriodStageRepository _workingPeriodStageRepository;
    private readonly IProductSubTypeWorkingPeriodSampleRepository _productSubTypeWorkingPeriodSampleRepository;
    private readonly IFactoryRepository _factoryRepository;
    private readonly IBrigadeRepository _brigadeRepository;
    private readonly IWorkingPeriodStageBrigadeRelationRepository _workingPeriodStageBrigadeRelationRepository;
    private readonly IWorkingPeriodRelationRepository _workingPeriodRelationRepository;
    private readonly IWorkingPeriodStageTypeRelationRepository _workingPeriodStageTypeRelationRepository;
    private readonly IStageTypeRepository _stageTypeRepository;
    private readonly IStageRepository _stageRepository;
    private readonly IMaterialStageRepository _materialStageRepository;
    private readonly IProductSubTypeStageSampleRepository _productSubTypeStageSampleRepository;
    private readonly IProductSubTypeGroupMaterialRelationRepository _productSubTypeGroupMaterialRelationRepository;
    private readonly ILogger<CoreService> _logger;

    private List<ProductEntity>? _products;
    private List<WorkingPeriodEntity> _allWorkingPeriods;
    private List<WorkingPeriodStageEntity> _allWorkingPeriodStages;
    private Dictionary<Guid, List<BrigadeEntity>> _factoryBrigades;
    private Dictionary<string, Guid> _stageTypeIds;

    // Доступность бригад (UTC)
    private Dictionary<Guid, DateTime> _brigadeNextAvailableTime = new();
    // Занятые слоты бригад (UTC)
    private Dictionary<Guid, List<BrigadeTimeSlot>> _brigadeTimeSlots = new();

    // Иерархия продуктов
    private Dictionary<Guid, List<ProductEntity>> _childrenByParentId = null!;
    private Dictionary<Guid, DateTime> _productStageConstraintDates = null!; // StageId -> макс. дата завершения детей (UTC)

    // Рабочее время (UTC, фабрики в одном поясе)
    private readonly TimeSpan WORKDAY_START = new(8, 0, 0);
    private readonly TimeSpan WORKDAY_END = new(20, 0, 0);

    private DateTime _planningStartTimeUtc;
    private List<Guid> _plannedProductIdsInThisRun = null!;

    public CoreService(
        IProductRepository productRepository,
        IWorkingPeriodRepository workingPeriodRepository,
        IWorkingPeriodStageRepository workingPeriodStageRepository,
        IProductSubTypeWorkingPeriodSampleRepository productSubTypeWorkingPeriodSampleRepository,
        IFactoryRepository factoryRepository,
        IBrigadeRepository brigadeRepository,
        IWorkingPeriodStageBrigadeRelationRepository workingPeriodStageBrigadeRelationRepository,
        IWorkingPeriodRelationRepository workingPeriodRelationRepository,
        IWorkingPeriodStageTypeRelationRepository workingPeriodStageTypeRelationRepository,
        IStageTypeRepository stageTypeRepository,
        IStageRepository stageRepository,
        IMaterialStageRepository materialStageRepository,
        IProductSubTypeStageSampleRepository productSubTypeStageSampleRepository,
        IProductSubTypeGroupMaterialRelationRepository productSubTypeGroupMaterialRelationRepository,
        ILogger<CoreService> logger)
    {
        _productRepository = productRepository;
        _workingPeriodRepository = workingPeriodRepository;
        _workingPeriodStageRepository = workingPeriodStageRepository;
        _productSubTypeWorkingPeriodSampleRepository = productSubTypeWorkingPeriodSampleRepository;
        _factoryRepository = factoryRepository;
        _brigadeRepository = brigadeRepository;
        _workingPeriodStageBrigadeRelationRepository = workingPeriodStageBrigadeRelationRepository;
        _workingPeriodRelationRepository = workingPeriodRelationRepository;
        _workingPeriodStageTypeRelationRepository = workingPeriodStageTypeRelationRepository;
        _stageTypeRepository = stageTypeRepository;
        _stageRepository = stageRepository;
        _materialStageRepository = materialStageRepository;
        _productSubTypeStageSampleRepository = productSubTypeStageSampleRepository;
        _productSubTypeGroupMaterialRelationRepository = productSubTypeGroupMaterialRelationRepository;
        _logger = logger;
    }

    public async Task PlanProductionAsync()
    {
        try
        {
            _logger.LogWarning("Начало планирования производства");
            _plannedProductIdsInThisRun = new List<Guid>();
            _planningStartTimeUtc = DateTime.UtcNow;
            _logger.LogWarning($"Время старта (UTC): {_planningStartTimeUtc:dd.MM.yyyy HH:mm}");

            _products = await _productRepository.GetAll();
            _allWorkingPeriods = await _workingPeriodRepository.GetAll();
            _allWorkingPeriodStages = await _workingPeriodStageRepository.GetAll();

            var productIdsToPlan = _products!
                .Where(p => !_allWorkingPeriods.Any(wp => wp.ProductId == p.Id))
                .Select(p => p.Id)
                .ToHashSet();

            await TestStandartTimeParsing();
            await LoadFactoryBrigades();
            foreach (var factoryId in _factoryBrigades.Keys)
            {
                await InitializeBrigadeAvailability(factoryId);
                await LoadBrigadeTimeSlotsAsync(factoryId);
            }
            await LoadStageTypeIds();

            if (_products == null || !_products.Any())
            {
                _logger.LogWarning("Нет продуктов для планирования");
                return;
            }

            SortProductsByEndDate();

            // Построение иерархии
            _childrenByParentId = _products
                .Where(p => p.ParentProductId.HasValue)
                .GroupBy(p => p.ParentProductId.Value)
                .ToDictionary(g => g.Key, g => g.ToList());
            _productStageConstraintDates = new Dictionary<Guid, DateTime>();

            // Планируем корневые продукты
            foreach (var root in _products.Where(p => p.ParentProductId == null))
                await PlanProductHierarchyAsync(root);

            // Дополнительная попытка для оставшихся
            var unplanned = _products.Where(p => !_allWorkingPeriods.Any(wp => wp.ProductId == p.Id)).ToList();
            if (unplanned.Any())
            {
                _logger.LogWarning($"Осталось незапланированных: {unplanned.Count}. Пробуем отдельно.");
                foreach (var p in unplanned)
                    await PlanProductWithBrigadeSelectionAsync(p, p.FactoryId);
            }

            var successCount = _plannedProductIdsInThisRun.Intersect(productIdsToPlan).Count();
            _logger.LogWarning($"Запланировано {successCount} из {productIdsToPlan.Count} (требовавших планирования). Всего продуктов: {_products.Count}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при планировании");
            throw;
        }
    }

    // ========== Иерархия ==========
    private async Task<bool> PlanProductHierarchyAsync(ProductEntity product)
    {
        if (_allWorkingPeriods.Any(wp => wp.ProductId == product.Id))
            return true;

        if (_childrenByParentId.TryGetValue(product.Id, out var children))
        {
            foreach (var child in children)
                if (!await PlanProductHierarchyAsync(child))
                {
                    _logger.LogError($"Не удалось запланировать дочерний {child.Id} для родителя {product.Id}");
                    return false;
                }
        }

        var result = await PlanProductWithBrigadeSelectionAsync(product, product.FactoryId);
        if (result)
            _logger.LogWarning($"Продукт {product.Id} запланирован");
        else
            _logger.LogError($"Не удалось запланировать продукт {product.Id}");
        return result;
    }

    private async Task UpdateParentStageConstraintAsync(ProductEntity childProduct, DateTime childEndUtc)
    {
        if (!childProduct.ParentProductId.HasValue || !childProduct.SubProductStageId.HasValue)
            return;

        var stageId = childProduct.SubProductStageId.Value;
        var stage = await _stageRepository.GetById(stageId);
        if (stage == null || stage.ProductId != childProduct.ParentProductId)
        {
            _logger.LogError($"Stage {stageId} не найдена или не принадлежит родителю");
            return;
        }

        if (_productStageConstraintDates.TryGetValue(stageId, out var existing) && childEndUtc <= existing)
            return;

        _productStageConstraintDates[stageId] = childEndUtc;
        stage.Date = childEndUtc;
        stage.UpdateTime = DateTime.UtcNow;
        await _stageRepository.Update(stage);
        _logger.LogWarning($"Обновлена Stage {stageId} родителя: {childEndUtc:dd.MM.yyyy HH:mm} UTC");
    }

    // ========== Получение дат Stage ==========
    private async Task<List<DateTime>> GetStageDatesWithMaterialStageNamePAsync(Guid productId)
    {
        var result = new List<DateTime>();
        var product = await _productRepository.GetById(productId);
        if (product == null) return result;

        var stages = await _stageRepository.GetByProductId(productId);
        if (stages == null) return result;

        foreach (var stage in stages)
        {
            var sample = await _productSubTypeStageSampleRepository.GetById(stage.ProductSubTypeStageSampleId);
            if (sample == null) continue;
            var material = await _materialStageRepository.GetById(sample.MaterialStageId);
            if (material?.StageName?.Trim() == "П")
                result.Add(stage.Date);
        }
        return result;
    }

    private async Task<DateTime> GetProductPlanningStartTimeAsync(Guid productId)
    {
        var dates = await GetStageDatesWithMaterialStageNamePAsync(productId);
        var start = dates.Any() ? dates.Max() : _planningStartTimeUtc;
        start = AdjustToWorkHours(start);
        _logger.LogWarning($"Продукт {productId} старт: {start:dd.MM.yyyy HH:mm} UTC");
        return start;
    }

    private async Task<List<DateTime>> GetStageDatesForWorkingPeriodSampleAsync(Guid productId, Guid sampleId)
    {
        var result = new List<DateTime>();
        var product = await _productRepository.GetById(productId);
        if (product == null) return result;

        // Блок 1: даты через связи MaterialStage
        var relations = await _productSubTypeGroupMaterialRelationRepository
            .GetByProductSubTypeWorkingPeriodSampleId(sampleId);
        if (relations != null)
        {
            foreach (var rel in relations)
            {
                var materialStages = await _materialStageRepository.GetByGroupMaterialId(rel.GroupMaterialId);
                if (materialStages == null) continue;
                foreach (var ms in materialStages)
                {
                    var samples = await _productSubTypeStageSampleRepository.GetByMaterialStageId(ms.Id);
                    var filtered = samples?.Where(s => s?.ProductSubTypeId == product.ProductSubTypeId).ToList();
                    if (filtered == null) continue;
                    foreach (var s in filtered)
                    {
                        var stages = await _stageRepository.GetByProductSubTypeStageSampleId(s.Id);
                        var productStages = stages?.Where(st => st?.ProductId == productId).ToList();
                        if (productStages == null) continue;
                        foreach (var st in productStages)
                            if (st.Date != DateTime.MinValue)
                                result.Add(st.Date);
                    }
                }
            }
        }

        // Блок 2: ограничения от детей
        var allStages = await _stageRepository.GetByProductId(productId);
        var currentStage = allStages?.FirstOrDefault(s => s.ProductSubTypeStageSampleId == sampleId);
        if (currentStage != null && _productStageConstraintDates.TryGetValue(currentStage.Id, out var constraint))
            result.Add(constraint);

        return result;
    }

    // ========== Бригады ==========
    private async Task LoadFactoryBrigades()
    {
        _factoryBrigades = new Dictionary<Guid, List<BrigadeEntity>>();
        var factories = await _factoryRepository.GetAll();
        _logger.LogWarning($"Загружено {factories.Count} фабрик");
        foreach (var f in factories)
        {
            var br = await _brigadeRepository.GetByFactoryId(f.Id);
            if (br != null)
                _factoryBrigades[f.Id] = br;
        }
    }

    private async Task LoadBrigadeTimeSlotsAsync(Guid factoryId)
    {
        if (!_factoryBrigades.TryGetValue(factoryId, out var brigades)) return;
        foreach (var b in brigades)
            _brigadeTimeSlots[b.Id] = await GetBrigadeBusyTimeSlotsAsync(b.Id);
    }

    private async Task<List<BrigadeTimeSlot>> GetBrigadeBusyTimeSlotsAsync(Guid brigadeId)
    {
        var slots = new List<BrigadeTimeSlot>();
        var relations = await _workingPeriodStageBrigadeRelationRepository.GetByBrigadeId(brigadeId);
        if (relations == null) return slots;

        var stageIds = relations.Select(r => r.WorkingPeriodStageId).ToHashSet();
        var stages = _allWorkingPeriodStages.Where(s => stageIds.Contains(s.Id)).ToList();
        foreach (var s in stages)
        {
            var sample = await _productSubTypeWorkingPeriodSampleRepository.GetById(s.ProductSubTypeWorkingPeriodSampleId);
            if (sample != null)
            {
                slots.Add(new BrigadeTimeSlot
                {
                    Start = s.DateFrom,
                    End = s.DateTo,
                    ProductId = GetProductIdByWorkingPeriodId(s.WorkingPeriodId),
                    StageId = s.Id,
                    BrigadeId = brigadeId,
                    RequiredEmployees = sample.StandartEmployee
                });
            }
        }
        return slots;
    }

    private Guid GetProductIdByWorkingPeriodId(Guid wpId) =>
        _allWorkingPeriods.FirstOrDefault(wp => wp.Id == wpId)?.ProductId ?? Guid.Empty;

    private async Task InitializeBrigadeAvailability(Guid factoryId)
    {
        if (!_factoryBrigades.TryGetValue(factoryId, out var brigades)) return;
        foreach (var b in brigades)
            _brigadeNextAvailableTime[b.Id] = _planningStartTimeUtc;
    }

    // ========== Основное планирование продукта ==========
    private async Task<bool> PlanProductWithBrigadeSelectionAsync(ProductEntity product, Guid factoryId)
    {
        if (_allWorkingPeriods.Any(wp => wp.ProductId == product.Id))
            return false;

        var stages = await _productSubTypeWorkingPeriodSampleRepository.GetByProductSubTypeId(product.ProductSubTypeId);
        if (stages == null || !stages.Any())
        {
            _logger.LogWarning($"Для продукта {product.Id} нет этапов");
            return false;
        }

        var graph = await BuildStageDependencyGraphAsync(stages);
        var plan = await GetStageExecutionPlanAsync(graph);
        var stageTypes = await GetStageTypesForGraphAsync(graph);
        var assemblyTypeId = _stageTypeIds.GetValueOrDefault("Сборка");
        var hasAssembly = stageTypes.Any(kv => kv.Value == assemblyTypeId);

        return hasAssembly
            ? await PlanProductWithAssemblyStagesAsync(product, factoryId, graph, plan, stageTypes)
            : await PlanProductWithoutAssemblyStagesAsync(product, factoryId, graph, plan, stageTypes);
    }

    private async Task<bool> PlanProductWithAssemblyStagesAsync(
        ProductEntity product, Guid factoryId,
        List<StageNode> graph, List<StageExecutionGroup> plan,
        Dictionary<Guid, Guid> stageTypes)
    {
        var assemblyTypeId = _stageTypeIds["Сборка"];
        var assemblyBrigades = (await _brigadeRepository.GetByStageTypeId(assemblyTypeId))
            ?.Where(b => b.FactoryId == factoryId).ToList();
        if (assemblyBrigades == null || !assemblyBrigades.Any())
        {
            _logger.LogError($"На фабрике {factoryId} нет бригад сборки");
            return false;
        }

        var productStart = await GetProductPlanningStartTimeAsync(product.Id);
        var best = await SelectBestAssemblyBrigadeAsync(product, assemblyBrigades, graph, plan, stageTypes, factoryId);
        if (!best.HasValue) return false;

        var (brigadeId, mapping, _, _) = best.Value;
        var planned = await PlanProductWithBrigadeMappingAsync(product, graph, plan, mapping, productStart, factoryId);
        if (!planned.Any()) return false;

        await CreateWorkingPeriodAsync(product, planned);
        if (planned.Any())
            await UpdateParentStageConstraintAsync(product, planned.Last().EndTime);

        _plannedProductIdsInThisRun.Add(product.Id);
        return true;
    }

    private async Task<bool> PlanProductWithoutAssemblyStagesAsync(
        ProductEntity product, Guid factoryId,
        List<StageNode> graph, List<StageExecutionGroup> plan,
        Dictionary<Guid, Guid> stageTypes)
    {
        var mapping = await CreateStageToBrigadeMappingAsync(product, graph, stageTypes, null, factoryId);
        if (!mapping.Any()) return false;

        var productStart = await GetProductPlanningStartTimeAsync(product.Id);
        var planned = await PlanProductWithBrigadeMappingAsync(product, graph, plan, mapping, productStart, factoryId);
        if (!planned.Any()) return false;

        await CreateWorkingPeriodAsync(product, planned);
        if (planned.Any())
            await UpdateParentStageConstraintAsync(product, planned.Last().EndTime);

        _plannedProductIdsInThisRun.Add(product.Id);
        return true;
    }

    private async Task<(Guid, Dictionary<Guid, Guid>, DateTime, DateTime)?> SelectBestAssemblyBrigadeAsync(
        ProductEntity product,
        List<BrigadeEntity> candidates,
        List<StageNode> graph,
        List<StageExecutionGroup> plan,
        Dictionary<Guid, Guid> stageTypes,
        Guid factoryId)
    {
        var productStart = await GetProductPlanningStartTimeAsync(product.Id);
        var evaluations = new List<AssemblyBrigadeEvaluation>();

        foreach (var b in candidates)
        {
            var mapping = await CreateStageToBrigadeMappingAsync(product, graph, stageTypes, b.Id, factoryId);
            if (!mapping.Any()) continue;

            var time = await EstimateProductTimeWithMappingAsync(product, graph, plan, mapping, factoryId, productStart);
            if (!time.HasValue) continue;

            evaluations.Add(new AssemblyBrigadeEvaluation
            {
                BrigadeId = b.Id,
                StageToBrigadeMapping = mapping,
                StartTime = time.Value.StartTime,
                EndTime = time.Value.EndTime,
                Duration = time.Value.EndTime - time.Value.StartTime,
                Score = CalculateBrigadeTimeScore(time.Value.StartTime, time.Value.EndTime, b)
            });
        }

        if (!evaluations.Any()) return null;
        var best = evaluations.OrderBy(e => e.EndTime).ThenBy(e => e.Duration).First();
        return (best.BrigadeId, best.StageToBrigadeMapping, best.StartTime, best.EndTime);
    }

    private async Task<Dictionary<Guid, Guid>> CreateStageToBrigadeMappingAsync(
        ProductEntity product,
        List<StageNode> graph,
        Dictionary<Guid, Guid> stageTypes,
        Guid? assemblyBrigadeId,
        Guid factoryId)
    {
        var mapping = new Dictionary<Guid, Guid>();
        var factoryBrigades = await _brigadeRepository.GetByFactoryId(factoryId);
        if (factoryBrigades == null) return mapping;

        var byType = factoryBrigades.GroupBy(b => b.StageTypeId).ToDictionary(g => g.Key, g => g.ToList());
        var assemblyTypeId = _stageTypeIds.GetValueOrDefault("Сборка");
        var cache = new Dictionary<Guid, Guid>(); // тип -> выбранная бригада

        foreach (var node in graph)
        {
            var typeId = stageTypes[node.Id];
            if (typeId == assemblyTypeId && assemblyBrigadeId.HasValue)
            {
                mapping[node.Id] = assemblyBrigadeId.Value;
                continue;
            }

            if (!cache.TryGetValue(typeId, out var selected))
            {
                if (!byType.ContainsKey(typeId))
                {
                    if (assemblyBrigadeId.HasValue)
                        selected = assemblyBrigadeId.Value;
                    else
                    {
                        _logger.LogError($"Нет бригад типа {typeId} для этапа {node.Id}");
                        continue;
                    }
                }
                else
                {
                    var suitable = byType[typeId].Where(b => b.CountEmployee >= node.Stage.StandartEmployee).ToList();
                    if (!suitable.Any())
                    {
                        if (assemblyBrigadeId.HasValue)
                            selected = assemblyBrigadeId.Value;
                        else
                            continue;
                    }
                    else
                    {
                        selected = suitable
                            .OrderBy(b => _brigadeNextAvailableTime.GetValueOrDefault(b.Id, _planningStartTimeUtc))
                            .ThenBy(b => b.CountEmployee)
                            .First().Id;
                    }
                }
                cache[typeId] = selected;
            }
            mapping[node.Id] = selected;
        }
        return mapping;
    }

    private async Task<(DateTime StartTime, DateTime EndTime)?> EstimateProductTimeWithMappingAsync(
        ProductEntity product,
        List<StageNode> graph,
        List<StageExecutionGroup> plan,
        Dictionary<Guid, Guid> mapping,
        Guid factoryId,
        DateTime productStartUtc)
    {
        var simBrigadeTimes = new Dictionary<Guid, DateTime>();
        var simSlots = new Dictionary<Guid, List<BrigadeTimeSlot>>();
        foreach (var bId in mapping.Values.Distinct())
        {
            simBrigadeTimes[bId] = productStartUtc;
            simSlots[bId] = _brigadeTimeSlots.ContainsKey(bId)
                ? new List<BrigadeTimeSlot>(_brigadeTimeSlots[bId])
                : new List<BrigadeTimeSlot>();
        }

        var stageEnd = new Dictionary<Guid, DateTime>();

        foreach (var group in plan.OrderBy(g => g.Level))
        {
            foreach (var stage in group.Stages)
            {
                var node = graph.FirstOrDefault(n => n.Id == stage.Id);
                if (node == null) continue;

                var bId = mapping[node.Id];
                var current = simBrigadeTimes[bId];

                // Учёт родителей
                if (node.Parents.Any())
                {
                    var parentEnds = node.Parents.Where(p => stageEnd.ContainsKey(p)).Select(p => stageEnd[p]).ToList();
                    if (parentEnds.Any() && parentEnds.Max() > current)
                        current = parentEnds.Max();
                }

                // Учёт Stage-дат
                var stageDates = await GetStageDatesForWorkingPeriodSampleAsync(product.Id, node.Id);
                if (stageDates.Any() && stageDates.Max() > current)
                    current = stageDates.Max();

                var duration = ParseStandartTimeToTimeSpan(stage.StandartTime);
                var slot = await FindAvailableTimeSlotForBrigadeAsync(
                    current, duration, bId, stage.StandartEmployee, simSlots[bId], hasDependencies: node.Parents.Any());

                if (!slot.HasValue) return null;

                var end = slot.Value.Add(duration);
                stageEnd[node.Id] = end;
                simBrigadeTimes[bId] = end;
                simSlots[bId].Add(new BrigadeTimeSlot
                {
                    Start = slot.Value,
                    End = end,
                    ProductId = product.Id,
                    StageId = node.Id,
                    BrigadeId = bId,
                    RequiredEmployees = stage.StandartEmployee
                });
            }
        }

        if (!stageEnd.Any()) return null;
        return (stageEnd.Values.Min(), stageEnd.Values.Max());
    }

    private async Task<List<PlannedStage>> PlanProductWithBrigadeMappingAsync(
        ProductEntity product,
        List<StageNode> graph,
        List<StageExecutionGroup> plan,
        Dictionary<Guid, Guid> mapping,
        DateTime productStartUtc,
        Guid factoryId)
    {
        var planned = new List<PlannedStage>();
        var stageEnd = new Dictionary<Guid, DateTime>();

        foreach (var group in plan.OrderBy(g => g.Level))
        {
            var items = new List<(StageNode Node, Guid BrigadeId, int Emp, TimeSpan Dur, bool HasDep)>();
            foreach (var s in group.Stages)
            {
                var node = graph.FirstOrDefault(n => n.Id == s.Id);
                if (node == null) continue;
                items.Add((node, mapping[node.Id], s.StandartEmployee,
                    ParseStandartTimeToTimeSpan(s.StandartTime), node.Parents.Any()));
            }

            foreach (var (node, bId, emp, dur, hasDep) in items.OrderBy(x => x.HasDep ? 1 : 0).ThenBy(x => x.Dur))
            {
                var search = productStartUtc;
                if (hasDep)
                {
                    var parents = node.Parents.Where(p => stageEnd.ContainsKey(p)).Select(p => stageEnd[p]).ToList();
                    if (parents.Any() && parents.Max() > search)
                        search = parents.Max();
                }

                var stageDates = await GetStageDatesForWorkingPeriodSampleAsync(product.Id, node.Id);
                if (stageDates.Any() && stageDates.Max() > search)
                {
                    search = stageDates.Max();
                    _logger.LogWarning($"Этап {node.Id} ограничен Stage {search:dd.MM.yyyy HH:mm} UTC");
                }

                search = AdjustToWorkHours(search);

                var slot = await FindAvailableTimeSlotForBrigadeAsync(
                    search, dur, bId, emp,
                    _brigadeTimeSlots.GetValueOrDefault(bId, new List<BrigadeTimeSlot>()),
                    hasDependencies: hasDep);

                if (!slot.HasValue)
                {
                    _logger.LogError($"Не найден слот для этапа {node.Id} на бригаде {bId}");
                    return new List<PlannedStage>();
                }

                var end = slot.Value.Add(dur);
                var typeId = await GetStageTypeIdAsync(node.Id);
                var ps = new PlannedStage
                {
                    ProductId = product.Id,
                    StageSampleId = node.Id,
                    BrigadeId = bId,
                    StartTime = slot.Value,
                    EndTime = end,
                    StageTypeId = typeId,
                    RequiredEmployees = emp
                };
                planned.Add(ps);
                stageEnd[node.Id] = end;

                if (!_brigadeTimeSlots.ContainsKey(bId))
                    _brigadeTimeSlots[bId] = new List<BrigadeTimeSlot>();
                _brigadeTimeSlots[bId].Add(new BrigadeTimeSlot
                {
                    Start = slot.Value,
                    End = end,
                    ProductId = product.Id,
                    StageId = node.Id,
                    BrigadeId = bId,
                    RequiredEmployees = emp
                });

                _logger.LogWarning($"Этап {node.Id}: {slot.Value:HH:mm}–{end:HH:mm} UTC на бригаде {bId}");
            }
        }
        return planned;
    }

    // ========== Поиск временного слота ==========
    private async Task<DateTime?> FindAvailableTimeSlotForBrigadeAsync(
        DateTime searchStartUtc,
        TimeSpan duration,
        Guid brigadeId,
        int requiredEmployees,
        List<BrigadeTimeSlot> existingSlotsUtc,
        bool hasDependencies)
    {
        var brigade = await _brigadeRepository.GetById(brigadeId);
        if (brigade == null || requiredEmployees > brigade.CountEmployee)
            return null;

        var current = AdjustToWorkHours(searchStartUtc);
        int maxDays = 30, days = 0;

        while (days < maxDays)
        {
            if (current.DayOfWeek == DayOfWeek.Saturday || current.DayOfWeek == DayOfWeek.Sunday)
            {
                current = current.Date.AddDays(1).Add(WORKDAY_START);
                days++;
                continue;
            }

            var dayStart = current.Date.Add(WORKDAY_START);
            var dayEnd = current.Date.Add(WORKDAY_END);
            var daySlots = existingSlotsUtc
                .Where(s => s.BrigadeId == brigadeId && s.Start.Date == current.Date)
                .OrderBy(s => s.Start).ToList();

            if (!daySlots.Any())
            {
                var candidate = current > dayStart ? current : dayStart;
                if (candidate.Add(duration) <= dayEnd)
                    return candidate;
            }
            else
            {
                var points = new List<DateTime> { dayStart };
                points.AddRange(daySlots.SelectMany(s => new[] { s.Start, s.End }));
                points.Add(dayEnd);
                points = points.OrderBy(t => t).Distinct().ToList();

                for (int i = 0; i < points.Count - 1; i++)
                {
                    var start = points[i];
                    var end = points[i + 1];
                    if (end - start <= TimeSpan.Zero) continue;

                    var actual = start;
                    if (hasDependencies)
                    {
                        if (end <= searchStartUtc) continue;
                        if (start < searchStartUtc) actual = searchStartUtc;
                    }
                    else
                    {
                        if (start < current)
                        {
                            if (end <= current) continue;
                            actual = current;
                        }
                    }

                    if (actual + duration <= end && await HasEnoughEmployees(daySlots, actual, duration, requiredEmployees))
                        return actual;
                }
            }

            days++;
            current = current.Date.AddDays(1).Add(WORKDAY_START);
        }
        return null;
    }

    private async Task<bool> HasEnoughEmployees(
        List<BrigadeTimeSlot> daySlots,
        DateTime startUtc,
        TimeSpan duration,
        int required)
    {
        var brigade = await _brigadeRepository.GetById(daySlots.First().BrigadeId);
        if (brigade == null) return false;
        var total = brigade.CountEmployee;
        var end = startUtc.Add(duration);

        for (var t = startUtc; t < end; t = t.AddMinutes(1))
        {
            var busy = daySlots.Where(s => t >= s.Start && t < s.End).Sum(s => s.RequiredEmployees);
            if (total - busy < required) return false;
        }
        return true;
    }

    // ========== Графы и типы ==========
    private async Task<List<StageNode>> BuildStageDependencyGraphAsync(List<ProductSubTypeWorkingPeriodSampleEntity> stages)
    {
        var nodes = stages.ToDictionary(s => s.Id, s => new StageNode
        {
            Id = s.Id,
            Stage = s,
            Parents = new List<Guid>(),
            Children = new List<Guid>()
        });

        foreach (var s in stages)
        {
            var rels = await _workingPeriodRelationRepository.GetByChildProductSubTypeWorkingPeriodSampleId(s.Id);
            if (rels == null) continue;
            foreach (var r in rels)
                if (nodes.ContainsKey(r.ParentProductSubTypeWorkingPeriodSampleId))
                {
                    nodes[s.Id].Parents.Add(r.ParentProductSubTypeWorkingPeriodSampleId);
                    nodes[r.ParentProductSubTypeWorkingPeriodSampleId].Children.Add(s.Id);
                }
        }
        return nodes.Values.ToList();
    }

    private async Task<List<StageExecutionGroup>> GetStageExecutionPlanAsync(List<StageNode> graph)
    {
        var plan = new List<StageExecutionGroup>();
        var remaining = graph.ToList();
        int level = 0;
        while (remaining.Any())
        {
            level++;
            var ready = remaining.Where(n => !n.Parents.Any() || n.Parents.All(p => !remaining.Any(r => r.Id == p))).ToList();
            if (!ready.Any()) throw new InvalidOperationException("Циклическая зависимость");
            plan.Add(new StageExecutionGroup
            {
                Stages = ready.Select(n => n.Stage).ToList(),
                Level = level
            });
            remaining.RemoveAll(n => ready.Contains(n));
        }
        return plan;
    }

    private async Task<Dictionary<Guid, Guid>> GetStageTypesForGraphAsync(List<StageNode> graph)
    {
        var result = new Dictionary<Guid, Guid>();
        foreach (var n in graph)
        {
            var id = await GetStageTypeIdAsync(n.Id);
            if (id.HasValue) result[n.Id] = id.Value;
        }
        return result;
    }

    private async Task<Guid?> GetStageTypeIdAsync(Guid sampleId)
    {
        var rels = await _workingPeriodStageTypeRelationRepository.GetByProductSubTypeWorkingPeriodSampleId(sampleId);
        return rels?.FirstOrDefault()?.StageTypeId;
    }

    private double CalculateBrigadeTimeScore(DateTime startUtc, DateTime endUtc, BrigadeEntity brigade) =>
        (endUtc - startUtc).TotalMinutes * -1 + (10.0 / brigade.CountEmployee) * 100 +
        (DateTime.UtcNow - startUtc).TotalMinutes * 0.1;

    private async Task LoadStageTypeIds()
    {
        _stageTypeIds = (await _stageTypeRepository.GetAll()).ToDictionary(st => st.Name, st => st.Id);
        _logger.LogWarning($"Загружено {_stageTypeIds.Count} типов этапов");
    }

    private TimeSpan ParseStandartTimeToTimeSpan(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return TimeSpan.Zero;
        if (int.TryParse(input, out int mins)) return TimeSpan.FromMinutes(mins);
        if (TimeSpan.TryParse(input, out var ts)) return ts;
        return TimeSpan.Zero;
    }

    private DateTime AdjustToWorkHours(DateTime utc)
    {
        if (utc.Kind != DateTimeKind.Utc) utc = DateTime.SpecifyKind(utc, DateTimeKind.Utc);
        if (utc.TimeOfDay >= WORKDAY_START && utc.TimeOfDay < WORKDAY_END) return utc;
        if (utc.TimeOfDay < WORKDAY_START) return utc.Date.Add(WORKDAY_START);
        var next = utc.Date.AddDays(1).Add(WORKDAY_START);
        while (next.DayOfWeek == DayOfWeek.Saturday || next.DayOfWeek == DayOfWeek.Sunday)
            next = next.AddDays(1);
        return next;
    }

    private async Task CreateWorkingPeriodAsync(ProductEntity product, List<PlannedStage> planned)
    {
        if (!planned.Any()) return;
        var wp = new WorkingPeriodEntity
        {
            Id = Guid.NewGuid(),
            Name = $"Период для продукта {product.Number}",
            Status = "В работе",
            ProductId = product.Id,
            DateFrom = planned.First().StartTime,
            DateTo = planned.Last().EndTime,
            CreateTime = DateTime.UtcNow,
            UpdateTime = DateTime.UtcNow
        };
        await _workingPeriodRepository.Add(wp);
        _allWorkingPeriods.Add(wp);

        foreach (var ps in planned)
        {
            var wps = new WorkingPeriodStageEntity
            {
                Id = Guid.NewGuid(),
                WorkingPeriodId = wp.Id,
                DateFrom = ps.StartTime,
                DateTo = ps.EndTime,
                Status = "В работе",
                ProductSubTypeWorkingPeriodSampleId = ps.StageSampleId,
                CreateTime = DateTime.UtcNow,
                UpdateTime = DateTime.UtcNow
            };
            await _workingPeriodStageRepository.Add(wps);
            _allWorkingPeriodStages.Add(wps);

            var rel = new WorkingPeriodStageBrigadeRelationEntity
            {
                Id = Guid.NewGuid(),
                WorkingPeriodStageId = wps.Id,
                BrigadeId = ps.BrigadeId
            };
            await _workingPeriodStageBrigadeRelationRepository.Add(rel);
        }
    }

    private void SortProductsByEndDate() =>
        _products?.Sort((a, b) =>
        {
            if (a.EndDate == b.EndDate) return 0;
            if (a.EndDate == null) return 1;
            if (b.EndDate == null) return -1;
            return a.EndDate.Value.CompareTo(b.EndDate.Value);
        });

    private async Task TestStandartTimeParsing()
    {
        var samples = await _productSubTypeWorkingPeriodSampleRepository.GetAll();
        if (samples?.Any() == true)
        {
            _logger.LogWarning("Тест StandartTime:");
            foreach (var s in samples.Take(5))
                _logger.LogWarning($"  {s.Id}: '{s.StandartTime}' -> {ParseStandartTimeToTimeSpan(s.StandartTime).TotalMinutes} мин");
        }
    }

    // ========== Вспомогательные классы ==========
    private class BrigadeTimeSlot
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Guid ProductId { get; set; }
        public Guid StageId { get; set; }
        public Guid BrigadeId { get; set; }
        public int RequiredEmployees { get; set; }
    }

    private class PlannedStage
    {
        public Guid ProductId { get; set; }
        public Guid StageSampleId { get; set; }
        public Guid BrigadeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid? StageTypeId { get; set; }
        public int RequiredEmployees { get; set; }
    }

    private class StageNode
    {
        public Guid Id { get; set; }
        public ProductSubTypeWorkingPeriodSampleEntity Stage { get; set; } = null!;
        public List<Guid> Parents { get; set; } = new();
        public List<Guid> Children { get; set; } = new();
    }

    private class StageExecutionGroup
    {
        public List<ProductSubTypeWorkingPeriodSampleEntity> Stages { get; set; } = new();
        public int Level { get; set; }
    }

    private class AssemblyBrigadeEvaluation
    {
        public Guid BrigadeId { get; set; }
        public Dictionary<Guid, Guid> StageToBrigadeMapping { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public double Score { get; set; }
    }
}