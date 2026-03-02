using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.EntityInterfaces;
using KssGroupPlanning.Repositories;

public class CoreService
{
    // Репозитории
    private readonly IProductRepository _productRepository;
    private readonly IWorkingPeriodRepository _workingPeriodRepository;
    private readonly IWorkingPeriodStageRepository _workingPeriodStageRepository;
    private readonly IProductSubTypeWorkingPeriodSampleRepository _sampleRepository;
    private readonly IFactoryRepository _factoryRepository;
    private readonly IBrigadeRepository _brigadeRepository;
    private readonly IWorkingPeriodStageBrigadeRelationRepository _wpStageBrigadeRepository;
    private readonly IWorkingPeriodRelationRepository _wpRelationRepository;
    private readonly IWorkingPeriodStageTypeRelationRepository _wpStageTypeRepository;
    private readonly IStageTypeRepository _stageTypeRepository;
    private readonly IStageRepository _stageRepository;
    private readonly IMaterialStageRepository _materialStageRepository;
    private readonly IProductSubTypeStageSampleRepository _stageSampleRepository;
    private readonly IProductSubTypeGroupMaterialRelationRepository _groupMaterialRepository;
    private readonly IWorkingPeriodStageMaterialRepository _workingPeriodStageMaterialRepository;
    private readonly ILogger<CoreService> _logger;

    // Загруженные данные (кэш)
    private List<ProductEntity> _products = null!;
    private List<WorkingPeriodEntity> _allWorkingPeriods = null!;
    private List<WorkingPeriodStageEntity> _allWorkingPeriodStages = null!;
    private Dictionary<Guid, List<BrigadeEntity>> _factoryBrigades = null!;
    private Dictionary<string, Guid> _stageTypeIds = null!;

    // Кэш для быстрого доступа к связанным данным
    private Dictionary<Guid, Guid> _stageTypeBySample = null!;          // ProductSubTypeWorkingPeriodSampleId -> StageTypeId
    private Dictionary<Guid, List<Guid>> _materialStagesByGroup = null!; // GroupMaterialId -> list of MaterialStageId
    private Dictionary<Guid, List<Guid>> _stageSamplesByMaterial = null!; // MaterialStageId -> list of ProductSubTypeStageSampleId
    private Dictionary<Guid, List<Guid>> _stagesBySample = null!;       // ProductSubTypeStageSampleId -> list of StageId (by product)
    // Для быстрого доступа к Stage по продукту
    private Dictionary<Guid, List<StageEntity>> _stagesByProduct = null!;
    // Для быстрого доступа к GroupMaterial по образцу этапа
    private Dictionary<Guid, List<Guid>> _groupMaterialsBySample = null!; // ProductSubTypeWorkingPeriodSampleId -> list of GroupMaterialId

    // Доступность бригад (UTC)
    private Dictionary<Guid, DateTime> _brigadeNextAvailableTime = new();
    private Dictionary<Guid, List<BrigadeTimeSlot>> _brigadeTimeSlots = new();

    // Иерархия продуктов
    private Dictionary<Guid, List<ProductEntity>> _childrenByParentId = null!;
    private Dictionary<Guid, DateTime> _productStageConstraintDates = null!; // StageId -> макс. дата завершения детей (UTC)

    private Dictionary<Guid, MaterialStageEntity> _materialStageById = new();

    // Константы рабочего времени (UTC)
    private readonly TimeSpan WORKDAY_START = new(8, 0, 0);
    private readonly TimeSpan WORKDAY_END = new(20, 0, 0);

    private DateTime _planningStartTimeUtc;
    private List<Guid> _plannedProductIdsInThisRun = null!;

    // Кэш для дат этапов (продукт, образец) -> список дат
    private Dictionary<(Guid productId, Guid sampleId), List<DateTime>> _stageDatesCache = new();

    public CoreService(
        IProductRepository productRepository,
        IWorkingPeriodRepository workingPeriodRepository,
        IWorkingPeriodStageRepository workingPeriodStageRepository,
        IProductSubTypeWorkingPeriodSampleRepository sampleRepository,
        IFactoryRepository factoryRepository,
        IBrigadeRepository brigadeRepository,
        IWorkingPeriodStageBrigadeRelationRepository wpStageBrigadeRepository,
        IWorkingPeriodRelationRepository wpRelationRepository,
        IWorkingPeriodStageTypeRelationRepository wpStageTypeRepository,
        IStageTypeRepository stageTypeRepository,
        IStageRepository stageRepository,
        IMaterialStageRepository materialStageRepository,
        IProductSubTypeStageSampleRepository stageSampleRepository,
        IProductSubTypeGroupMaterialRelationRepository groupMaterialRepository,
        IWorkingPeriodStageMaterialRepository workingPeriodStageMaterialRepository,
        ILogger<CoreService> logger)
    {
        _productRepository = productRepository;
        _workingPeriodRepository = workingPeriodRepository;
        _workingPeriodStageRepository = workingPeriodStageRepository;
        _sampleRepository = sampleRepository;
        _factoryRepository = factoryRepository;
        _brigadeRepository = brigadeRepository;
        _wpStageBrigadeRepository = wpStageBrigadeRepository;
        _wpRelationRepository = wpRelationRepository;
        _wpStageTypeRepository = wpStageTypeRepository;
        _stageTypeRepository = stageTypeRepository;
        _stageRepository = stageRepository;
        _materialStageRepository = materialStageRepository;
        _stageSampleRepository = stageSampleRepository;
        _groupMaterialRepository = groupMaterialRepository;
        _workingPeriodStageMaterialRepository = workingPeriodStageMaterialRepository;
        _logger = logger;
    }

    public async Task PlanProductionAsync()
    {
        try
        {
            _logger.LogWarning("\n Начало планирования производства \n");
            _plannedProductIdsInThisRun = new List<Guid>();
            _planningStartTimeUtc = DateTime.UtcNow;
            _logger.LogWarning($"\n Время старта (UTC): {_planningStartTimeUtc:dd.MM.yyyy HH:mm} \n");

            // Загрузка всех основных данных
            _products = await _productRepository.GetAll();
            _allWorkingPeriods = await _workingPeriodRepository.GetAll();
            _allWorkingPeriodStages = await _workingPeriodStageRepository.GetAll();
            _logger.LogWarning($"\n Загружено: {_products.Count} продуктов, {_allWorkingPeriods.Count} рабочих периодов, {_allWorkingPeriodStages.Count} этапов \n");

            // Определяем продукты для планирования в этом запуске
            var productIdsToPlan = _products
                .Where(p => !_allWorkingPeriods.Any(wp => wp.ProductId == p.Id))
                .Select(p => p.Id)
                .ToHashSet();
            _logger.LogWarning($"\n Продуктов, требующих планирования: {productIdsToPlan.Count} \n");

            // Загрузка справочников и построение кэшей
            await LoadAllReferenceDataAsync();

            // Загрузка фабрик и бригад
            await LoadFactoryBrigadesAsync();

            // Инициализация доступности бригад и занятых слотов
            foreach (var factoryId in _factoryBrigades.Keys)
            {
                InitializeBrigadeAvailability(factoryId);
                await LoadBrigadeTimeSlotsAsync(factoryId);
            }

            // Загрузка типов этапов
            await LoadStageTypeIdsAsync();

            if (_products.Count == 0)
            {
                _logger.LogWarning("\n Нет продуктов для планирования \n");
                return;
            }

            // Сортировка продуктов по EndDate (приоритет по срокам)
            SortProductsByEndDate();

            // Построение иерархии
            _childrenByParentId = _products
                .Where(p => p.ParentProductId.HasValue)
                .GroupBy(p => p.ParentProductId.Value)
                .ToDictionary(g => g.Key, g => g.ToList());
            _productStageConstraintDates = new Dictionary<Guid, DateTime>();

            // Планируем корневые продукты
            var rootProducts = _products.Where(p => p.ParentProductId == null).ToList();
            _logger.LogWarning($"\n Найдено {rootProducts.Count} корневых продуктов \n");
            foreach (var root in rootProducts)
            {
                await PlanProductHierarchyAsync(root);
            }

            // Дополнительная попытка для оставшихся (на случай циклов)
            var unplanned = _products.Where(p => !_allWorkingPeriods.Any(wp => wp.ProductId == p.Id)).ToList();
            if (unplanned.Any())
            {
                _logger.LogWarning($"\n Осталось незапланированных: {unplanned.Count}. Пробуем отдельно. \n");
                foreach (var p in unplanned)
                {
                    await PlanProductWithBrigadeSelectionAsync(p, p.FactoryId);
                }
            }

            var successCount = _plannedProductIdsInThisRun.Intersect(productIdsToPlan).Count();
            _logger.LogWarning($"\n Успешно запланировано {successCount} из {productIdsToPlan.Count} требовавших планирования. Всего продуктов в БД: {_products.Count} \n");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "\n Ошибка при планировании производства \n");
            throw;
        }
    }

    // Загрузка всех справочников в память для быстрого доступа
    private async Task LoadAllReferenceDataAsync()
    {
        _logger.LogDebug("Загрузка справочных данных...");

        var stageTypeRelations = await _wpStageTypeRepository.GetAll();
        _stageTypeBySample = stageTypeRelations?.ToDictionary(r => r.ProductSubTypeWorkingPeriodSampleId, r => r.StageTypeId)
                             ?? new Dictionary<Guid, Guid>();

        var materialStages = await _materialStageRepository.GetAll();
        _materialStageById = materialStages?.ToDictionary(ms => ms.Id, ms => ms)
                             ?? new Dictionary<Guid, MaterialStageEntity>();
        _materialStagesByGroup = materialStages?
            .Where(ms => ms.GroupMaterialId.HasValue)
            .GroupBy(ms => ms.GroupMaterialId.Value)
            .ToDictionary(g => g.Key, g => g.Select(ms => ms.Id).ToList())
            ?? new Dictionary<Guid, List<Guid>>();

        var stageSamples = await _stageSampleRepository.GetAll();
        _stageSamplesByMaterial = stageSamples?
            .GroupBy(ss => ss.MaterialStageId)
            .ToDictionary(g => g.Key, g => g.Select(ss => ss.Id).ToList())
            ?? new Dictionary<Guid, List<Guid>>();

        var allStages = await _stageRepository.GetAll();
        _stagesBySample = allStages?
            .GroupBy(s => s.ProductSubTypeStageSampleId)
            .ToDictionary(g => g.Key, g => g.Select(s => s.Id).ToList())
            ?? new Dictionary<Guid, List<Guid>>();
        _stagesByProduct = allStages?
            .GroupBy(s => s.ProductId)
            .ToDictionary(g => g.Key, g => g.ToList())
            ?? new Dictionary<Guid, List<StageEntity>>();

        var groupMaterialRelations = await _groupMaterialRepository.GetAll();
        _groupMaterialsBySample = groupMaterialRelations?
            .GroupBy(r => r.ProductSubTypeWorkingPeriodSampleId)
            .ToDictionary(g => g.Key, g => g.Select(r => r.GroupMaterialId).ToList())
            ?? new Dictionary<Guid, List<Guid>>();

        _logger.LogDebug("Справочные данные загружены");
    }

    private async Task<bool> CreateStagesForProductAsync(ProductEntity product)
    {
        _logger.LogDebug($"Создание Stage для продукта {product.Id}");

        // Проверка инициализации справочников
        if (_materialStageById == null)
        {
            _logger.LogError("Справочник _materialStageById не инициализирован.");
            return false;
        }
        if (_workingPeriodStageMaterialRepository == null)
        {
            _logger.LogError("Репозиторий _workingPeriodStageMaterialRepository не инициализирован.");
            return false;
        }

        // Определяем дату старта (UTC)
        DateTime? startDateUtc = await GetStartDateUtcAsync(product);
        if (!startDateUtc.HasValue)
            return false;

        // Получаем образцы стадий для подтипа продукта
        var samples = await _stageSampleRepository.GetByProductSubTypeId(product.ProductSubTypeId);
        if (samples == null)
        {
            _logger.LogError($"Ошибка при получении образцов стадий для подтипа {product.ProductSubTypeId} (репозиторий вернул null)");
            return false;
        }
        if (!samples.Any())
        {
            _logger.LogWarning($"Для подтипа {product.ProductSubTypeId} продукта {product.Id} нет образцов стадий. Продукт не будет запланирован.");
            return false;
        }

        // Загружаем все поставки материалов для этого продукта, исключая "мусорную" дату 2000-01-01
        var allDeliveries = await _workingPeriodStageMaterialRepository.GetByProductId(product.Id);
        var deliveriesByGroup = allDeliveries?
            .Where(d => d.DateDelivery != new DateOnly(2000, 1, 1))
            .ToLookup(d => d.GroupMaterialId)
            ?? Enumerable.Empty<WorkingPeriodStageMaterialEntity>().ToLookup(d => d.GroupMaterialId);

        var stagesToCreate = new List<StageEntity>();

        // ---- 1. Обработка стадии "С" (старт) ----
        StageEntity? stageC = null;
        foreach (var sample in samples)
        {
            if (!_materialStageById.TryGetValue(sample.MaterialStageId, out var materialStage))
            {
                _logger.LogError($"MaterialStage с Id {sample.MaterialStageId} не найдена в кэше");
                return false;
            }
            if (materialStage.StageName == "С")
            {
                stageC = new StageEntity
                {
                    Id = Guid.NewGuid(),
                    ProductSubTypeStageSampleId = sample.Id,
                    ProductId = product.Id,
                    Status = "Готов",
                    Date = startDateUtc.Value,
                    CreateTime = DateTime.UtcNow,
                    UpdateTime = DateTime.UtcNow
                };
                stagesToCreate.Add(stageC);
                break;
            }
        }
        if (stageC == null)
        {
            _logger.LogError($"Для продукта {product.Id} не найдена стадия с именем 'С'");
            return false;
        }

        // ---- 2. Обработка стадии "П" ----
        StageEntity? stageP = null;
        foreach (var sample in samples)
        {
            if (stagesToCreate.Any(s => s.ProductSubTypeStageSampleId == sample.Id))
                continue;

            if (!_materialStageById.TryGetValue(sample.MaterialStageId, out var materialStage))
            {
                _logger.LogError($"MaterialStage с Id {sample.MaterialStageId} не найдена в кэше");
                return false;
            }
            if (materialStage.StageName == "П")
            {
                if (!double.TryParse(sample.StandartTime, out double hours))
                {
                    _logger.LogError($"Не удалось распарсить StandartTime как число: '{sample.StandartTime}' для образца {sample.Id}");
                    return false;
                }
                var stageDate = stageC.Date.AddHours(hours);
                stageP = new StageEntity
                {
                    Id = Guid.NewGuid(),
                    ProductSubTypeStageSampleId = sample.Id,
                    ProductId = product.Id,
                    Status = "Готов",
                    Date = stageDate,
                    CreateTime = DateTime.UtcNow,
                    UpdateTime = DateTime.UtcNow
                };
                stagesToCreate.Add(stageP);
                break;
            }
        }
        if (stageP == null)
        {
            _logger.LogError($"Для продукта {product.Id} не найдена стадия с именем 'П'");
            return false;
        }

        // ---- 3. Обработка остальных стадий ----
        foreach (var sample in samples)
        {
            // Пропускаем уже созданные "С" и "П"
            if (stagesToCreate.Any(s => s.ProductSubTypeStageSampleId == sample.Id))
                continue;

            if (!_materialStageById.TryGetValue(sample.MaterialStageId, out var materialStage))
            {
                _logger.LogError($"MaterialStage с Id {sample.MaterialStageId} не найдена в кэше");
                return false;
            }

            DateTime stageDate;

            // Пытаемся получить дату из материалов, если есть GroupMaterialId и поставки
            if (materialStage.GroupMaterialId.HasValue)
            {
                var groupDeliveries = deliveriesByGroup[materialStage.GroupMaterialId.Value].ToList();
                if (groupDeliveries.Any())
                {
                    var maxDeliveryDate = groupDeliveries.Max(d => d.DateDelivery);
                    stageDate = maxDeliveryDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
                    _logger.LogDebug($"Для стадии {sample.Id} использована максимальная дата поставки: {maxDeliveryDate}");
                }
                else
                {
                    // Группа материалов есть, но поставок нет – используем норматив от даты "П"
                    if (!double.TryParse(sample.StandartTime, out double hours))
                    {
                        _logger.LogError($"Не удалось распарсить StandartTime как число: '{sample.StandartTime}' для образца {sample.Id}");
                        return false;
                    }
                    stageDate = stageP.Date.AddHours(hours);
                    _logger.LogDebug($"Для стадии {sample.Id} использован норматив {hours}ч от даты 'П' (нет поставок)");
                }
            }
            else
            {
                // Нет группы материалов – используем норматив от даты "П"
                if (!double.TryParse(sample.StandartTime, out double hours))
                {
                    _logger.LogError($"Не удалось распарсить StandartTime как число: '{sample.StandartTime}' для образца {sample.Id}");
                    return false;
                }
                stageDate = stageP.Date.AddHours(hours);
                _logger.LogDebug($"Для стадии {sample.Id} использован норматив {hours}ч от даты 'П' (нет GroupMaterialId)");
            }

            var newStage = new StageEntity
            {
                Id = Guid.NewGuid(),
                ProductSubTypeStageSampleId = sample.Id,
                ProductId = product.Id,
                Status = "Готов",
                Date = stageDate,
                CreateTime = DateTime.UtcNow,
                UpdateTime = DateTime.UtcNow
            };
            stagesToCreate.Add(newStage);
        }

        // Сохранение в БД
        foreach (var stage in stagesToCreate)
        {
            await _stageRepository.Add(stage);
            _logger.LogDebug($"Создана Stage {stage.Id} для продукта {product.Id}");
        }

        // Обновление кэша
        if (!_stagesByProduct.ContainsKey(product.Id))
            _stagesByProduct[product.Id] = new List<StageEntity>();
        _stagesByProduct[product.Id].AddRange(stagesToCreate);

        foreach (var stage in stagesToCreate)
        {
            if (!_stagesBySample.ContainsKey(stage.ProductSubTypeStageSampleId))
                _stagesBySample[stage.ProductSubTypeStageSampleId] = new List<Guid>();
            _stagesBySample[stage.ProductSubTypeStageSampleId].Add(stage.Id);
        }

        _logger.LogInformation($"Для продукта {product.Id} создано {stagesToCreate.Count} стадий");
        return true;
    }

    // Вспомогательный метод для определения даты старта
    private async Task<DateTime?> GetStartDateUtcAsync(ProductEntity product)
    {
        if (product.StartDate.HasValue)
            return product.StartDate.Value.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);

        if (!product.EndDate.HasValue)
        {
            _logger.LogError($"Продукт {product.Id} не имеет ни StartDate, ни EndDate. Невозможно определить дату старта.");
            return null;
        }

        var computedStartDate = product.EndDate.Value.AddDays(-14);
        _logger.LogWarning($"Продукт {product.Id} не имеет StartDate. Используем вычисленную дату: {computedStartDate} (EndDate - 14 дней)");
        return computedStartDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
    }

   

    private async Task LoadFactoryBrigadesAsync()
    {
        var factories = await _factoryRepository.GetAll();
        _factoryBrigades = new Dictionary<Guid, List<BrigadeEntity>>();
        foreach (var f in factories)
        {
            var brigades = await _brigadeRepository.GetByFactoryId(f.Id);
            if (brigades != null)
                _factoryBrigades[f.Id] = brigades;
        }
        _logger.LogWarning($"\n Загружено {_factoryBrigades.Count} фабрик \n");
    }

    private void InitializeBrigadeAvailability(Guid factoryId)
    {
        if (!_factoryBrigades.TryGetValue(factoryId, out var brigades)) return;
        foreach (var b in brigades)
            _brigadeNextAvailableTime[b.Id] = _planningStartTimeUtc;
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
        var relations = await _wpStageBrigadeRepository.GetByBrigadeId(brigadeId);
        if (relations == null) return slots;

        var stageIds = relations.Select(r => r.WorkingPeriodStageId).ToHashSet();
        var stages = _allWorkingPeriodStages.Where(s => stageIds.Contains(s.Id)).ToList();
        foreach (var s in stages)
        {
            var sample = await _sampleRepository.GetById(s.ProductSubTypeWorkingPeriodSampleId);
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

    private async Task LoadStageTypeIdsAsync()
    {
        var all = await _stageTypeRepository.GetAll();
        _stageTypeIds = all.ToDictionary(st => st.Name, st => st.Id);
        _logger.LogWarning($"\n Загружено {_stageTypeIds.Count} типов этапов \n");
    }

    private void SortProductsByEndDate()
    {
        _products.Sort((a, b) =>
        {
            if (a.EndDate == b.EndDate) return 0;
            if (a.EndDate == null) return 1;
            if (b.EndDate == null) return -1;
            return a.EndDate.Value.CompareTo(b.EndDate.Value);
        });
    }

    // ========== Иерархия ==========
    private async Task<bool> PlanProductHierarchyAsync(ProductEntity product)
    {
        if (_allWorkingPeriods.Any(wp => wp.ProductId == product.Id))
        {
            _logger.LogDebug($"\n Продукт {product.Id} уже запланирован, пропускаем \n");
            return true;
        }

        if (_childrenByParentId.TryGetValue(product.Id, out var children))
        {
            foreach (var child in children)
            {
                if (!await PlanProductHierarchyAsync(child))
                {
                    _logger.LogError($"\n Не удалось запланировать дочерний продукт {child.Id} для родителя {product.Id} \n");
                    return false;
                }
            }
        }

        var result = await PlanProductWithBrigadeSelectionAsync(product, product.FactoryId);
        if (result)
            _logger.LogInformation($"\n Продукт {product.Id} успешно запланирован \n");
        else
            _logger.LogError($"\n Не удалось запланировать продукт {product.Id} \n");
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
            _logger.LogError($"\n Stage {stageId} не найдена или не принадлежит родителю \n");
            return;
        }

        if (_productStageConstraintDates.TryGetValue(stageId, out var existing) && childEndUtc <= existing)
            return;

        _productStageConstraintDates[stageId] = childEndUtc;
        stage.Date = childEndUtc;
        stage.UpdateTime = DateTime.UtcNow;
        await _stageRepository.Update(stage);
        _logger.LogInformation($"\n Обновлена Stage {stageId} родительского продукта {childProduct.ParentProductId}: {childEndUtc:dd.MM.yyyy HH:mm} UTC \n");
    }

    // ========== Получение дат Stage ==========
    private async Task<List<DateTime>> GetStageDatesWithMaterialStageNamePAsync(Guid productId)
    {
        var result = new List<DateTime>();
        if (!_stagesByProduct.TryGetValue(productId, out var stages)) return result;

        foreach (var stage in stages)
        {
            if (!_stageSamplesByMaterial.TryGetValue(stage.ProductSubTypeStageSampleId, out var sampleIds)) continue;
        }

        foreach (var stage in stages)
        {
            var sample = await _stageSampleRepository.GetById(stage.ProductSubTypeStageSampleId);
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
        _logger.LogDebug($"\n Продукт {productId} старт планирования: {start:dd.MM.yyyy HH:mm} UTC \n");
        return start;
    }

    private async Task<List<DateTime>> GetStageDatesForWorkingPeriodSampleAsync(Guid productId, Guid sampleId)
    {
        var key = (productId, sampleId);
        if (_stageDatesCache.TryGetValue(key, out var cached))
            return cached;

        var result = new List<DateTime>();

        // Блок 1: даты через связи MaterialStage
        if (_groupMaterialsBySample.TryGetValue(sampleId, out var groupMaterialIds))
        {
            foreach (var gmId in groupMaterialIds)
            {
                if (!_materialStagesByGroup.TryGetValue(gmId, out var materialStageIds)) continue;
                foreach (var msId in materialStageIds)
                {
                    if (!_stageSamplesByMaterial.TryGetValue(msId, out var stageSampleIds)) continue;
                    foreach (var ssId in stageSampleIds)
                    {
                        if (!_stagesBySample.TryGetValue(ssId, out var stageIds)) continue;
                        var productStages = stageIds
                            .Select(id => _stagesByProduct.GetValueOrDefault(productId)?.FirstOrDefault(s => s.Id == id))
                            .Where(s => s != null && s.Date != DateTime.MinValue);
                        foreach (var st in productStages)
                            result.Add(st.Date);
                    }
                }
            }
        }

        // Блок 2: ограничения от детей
        if (_stagesByProduct.TryGetValue(productId, out var stages))
        {
            var stage = stages.FirstOrDefault(s => s.ProductSubTypeStageSampleId == sampleId);
            if (stage != null && _productStageConstraintDates.TryGetValue(stage.Id, out var constraint))
                result.Add(constraint);
        }

        _stageDatesCache[key] = result;
        return result;
    }

    // ========== Основное планирование продукта ==========
    private async Task<bool> PlanProductWithBrigadeSelectionAsync(ProductEntity product, Guid factoryId)
    {
        if (_allWorkingPeriods.Any(wp => wp.ProductId == product.Id))
        {
            _logger.LogWarning($"У продукта {product.Id} уже есть рабочий период");
            return false;
        }

        // Создание стадий, если их ещё нет
        var existingStages = await _stageRepository.GetByProductId(product.Id);
        if (existingStages == null || !existingStages.Any())
        {
            var created = await CreateStagesForProductAsync(product);
            if (!created)
            {
                _logger.LogError($"Не удалось создать стадии для продукта {product.Id}");
                return false;
            }
        }
        else
        {
            _logger.LogDebug($"Для продукта {product.Id} уже есть стадии, пропускаем создание");
        }

        // ---------- НОВАЯ ПРОВЕРКА ----------
        var pDates = await GetStageDatesWithMaterialStageNamePAsync(product.Id);
        if (!pDates.Any())
        {
            _logger.LogWarning($"Продукт {product.Id} не имеет дат 'П' (MaterialStage.Name = 'П'), планирование пропущено.");
            return false;
        }

        var stages = await _sampleRepository.GetByProductSubTypeId(product.ProductSubTypeId);
        if (stages == null || !stages.Any())
        {
            _logger.LogWarning($"\n Для продукта {product.Id} нет этапов \n");
            return false;
        }
        _logger.LogDebug($"\n Продукт {product.Id}: загружено {stages.Count} этапов \n");

        var graph = await BuildStageDependencyGraphAsync(stages);
        var plan = await GetStageExecutionPlanAsync(graph);
        var stageTypes = GetStageTypesForGraph(graph); // синхронный метод, использует кэш
        var assemblyTypeId = _stageTypeIds.GetValueOrDefault("Сборка");
        var hasAssembly = stageTypes.Any(kv => kv.Value == assemblyTypeId);

        bool result;
        if (hasAssembly)
        {
            _logger.LogDebug($"\n Продукт {product.Id} содержит этапы сборки \n");
            result = await PlanProductWithAssemblyStagesAsync(product, factoryId, graph, plan, stageTypes);
        }
        else
        {
            result = await PlanProductWithoutAssemblyStagesAsync(product, factoryId, graph, plan, stageTypes);
        }

        if (result)
            _plannedProductIdsInThisRun.Add(product.Id);
        return result;
    }

    private Dictionary<Guid, Guid> GetStageTypesForGraph(List<StageNode> graph)
    {
        var result = new Dictionary<Guid, Guid>();
        foreach (var n in graph)
            if (_stageTypeBySample.TryGetValue(n.Id, out var typeId))
                result[n.Id] = typeId;
        return result;
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
            _logger.LogError($"\n На фабрике {factoryId} нет бригад сборки \n");
            return false;
        }

        var productStart = await GetProductPlanningStartTimeAsync(product.Id);
        var best = await SelectBestAssemblyBrigadeAsync(product, assemblyBrigades, graph, plan, stageTypes, factoryId);
        if (!best.HasValue)
        {
            _logger.LogError($"\n Не удалось выбрать бригаду сборки для продукта {product.Id} \n");
            return false;
        }

        var (brigadeId, mapping, _, _) = best.Value;
        _logger.LogInformation($"\n Для продукта {product.Id} выбрана бригада сборки {brigadeId} \n");

        var planned = await PlanProductWithBrigadeMappingAsync(product, graph, plan, mapping, productStart, factoryId);
        if (!planned.Any())
        {
            _logger.LogWarning($"\n Не удалось запланировать этапы продукта {product.Id} \n");
            return false;
        }

        await CreateWorkingPeriodAsync(product, planned);
        if (planned.Any())
            await UpdateParentStageConstraintAsync(product, planned.Last().EndTime);

        return true;
    }

    private async Task<bool> PlanProductWithoutAssemblyStagesAsync(
        ProductEntity product, Guid factoryId,
        List<StageNode> graph, List<StageExecutionGroup> plan,
        Dictionary<Guid, Guid> stageTypes)
    {
        var mapping = await CreateStageToBrigadeMappingAsync(product, graph, stageTypes, null, factoryId);
        if (!mapping.Any())
        {
            _logger.LogError($"\n Не удалось создать маппинг бригад для продукта {product.Id} \n");
            return false;
        }

        var productStart = await GetProductPlanningStartTimeAsync(product.Id);
        var planned = await PlanProductWithBrigadeMappingAsync(product, graph, plan, mapping, productStart, factoryId);
        if (!planned.Any())
        {
            _logger.LogWarning($"\n Не удалось запланировать этапы продукта {product.Id} \n");
            return false;
        }

        await CreateWorkingPeriodAsync(product, planned);
        if (planned.Any())
            await UpdateParentStageConstraintAsync(product, planned.Last().EndTime);

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

        // Последовательная оценка (без параллелизма для предотвращения конфликтов DbContext)
        foreach (var brigade in candidates)
        {
            var mapping = await CreateStageToBrigadeMappingAsync(product, graph, stageTypes, brigade.Id, factoryId);
            if (!mapping.Any()) continue;

            var time = await EstimateProductTimeWithMappingAsync(product, graph, plan, mapping, factoryId, productStart);
            if (!time.HasValue) continue;

            evaluations.Add(new AssemblyBrigadeEvaluation
            {
                BrigadeId = brigade.Id,
                StageToBrigadeMapping = mapping,
                StartTime = time.Value.StartTime,
                EndTime = time.Value.EndTime,
                Duration = time.Value.EndTime - time.Value.StartTime,
                Score = CalculateBrigadeTimeScore(time.Value.StartTime, time.Value.EndTime, brigade)
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
                        _logger.LogWarning($"\n Нет бригад типа {typeId} для этапа {node.Id} \n");
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
                    current, duration, bId, stage.StandartEmployee, simSlots[bId], node.Parents.Any());

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
                    _logger.LogDebug($"\n Этап {node.Id} ограничен Stage {search:dd.MM.yyyy HH:mm} UTC \n");
                }

                search = AdjustToWorkHours(search);

                var slot = await FindAvailableTimeSlotForBrigadeAsync(
                    search, dur, bId, emp,
                    _brigadeTimeSlots.GetValueOrDefault(bId, new List<BrigadeTimeSlot>()),
                    hasDep);

                if (!slot.HasValue)
                {
                    _logger.LogError($"\n Не найден слот для этапа {node.Id} на бригаде {bId} \n");
                    return new List<PlannedStage>();
                }

                var end = slot.Value.Add(dur);
                var typeId = _stageTypeBySample.GetValueOrDefault(node.Id);
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

                _logger.LogDebug($"\n Этап {node.Id}: {slot.Value:HH:mm}–{end:HH:mm} UTC на бригаде {bId} \n");
            }
        }
        return planned;
    }

    // ========== Поиск временного слота (оптимизированный) ==========
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
        var totalEmployees = brigade.CountEmployee;

        while (days < maxDays)
        {
            if (current.DayOfWeek == DayOfWeek.Saturday || current.DayOfWeek == DayOfWeek.Sunday)
            {
                current = NextWorkDay(current);
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
                points.Sort();

                for (int i = 0; i < points.Count - 1; i++)
                {
                    var intervalStart = points[i];
                    var intervalEnd = points[i + 1];
                    if (intervalEnd - intervalStart < duration) continue;

                    var earliestStart = intervalStart;
                    if (hasDependencies && earliestStart < searchStartUtc)
                        earliestStart = searchStartUtc;
                    if (!hasDependencies && earliestStart < current)
                        earliestStart = current;

                    if (earliestStart + duration <= intervalEnd)
                    {
                        if (HasEnoughEmployeesFast(daySlots, earliestStart, duration, requiredEmployees, totalEmployees))
                            return earliestStart;
                    }
                }
            }

            days++;
            current = NextWorkDay(current.Date.AddDays(1));
        }
        return null;
    }

    private TimeSpan ParseHoursToTimeSpan(string hoursStr)
    {
        if (double.TryParse(hoursStr, out double hours))
            return TimeSpan.FromHours(hours);
        throw new InvalidOperationException($"Не удалось распарсить норматив как часы: {hoursStr}");
    }

    private bool HasEnoughEmployeesFast(
        List<BrigadeTimeSlot> daySlots,
        DateTime startUtc,
        TimeSpan duration,
        int requiredEmployees,
        int totalEmployees)
    {
        var end = startUtc.Add(duration);
        var changes = new List<(DateTime time, int delta)>();

        foreach (var s in daySlots)
        {
            if (s.End <= startUtc || s.Start >= end) continue;
            var intersectStart = s.Start > startUtc ? s.Start : startUtc;
            var intersectEnd = s.End < end ? s.End : end;
            if (intersectStart < intersectEnd)
            {
                changes.Add((intersectStart, +s.RequiredEmployees));
                changes.Add((intersectEnd, -s.RequiredEmployees));
            }
        }
        changes.Add((startUtc, 0));
        changes = changes.OrderBy(c => c.time).ToList();

        int busy = 0;
        foreach (var c in changes)
        {
            busy += c.delta;
            if (totalEmployees - busy < requiredEmployees)
                return false;
        }
        return true;
    }

    private DateTime NextWorkDay(DateTime date)
    {
        while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            date = date.AddDays(1);
        return date.Date.Add(WORKDAY_START);
    }

    // ========== Графы ==========
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
            var rels = await _wpRelationRepository.GetByChildProductSubTypeWorkingPeriodSampleId(s.Id);
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

    private double CalculateBrigadeTimeScore(DateTime startUtc, DateTime endUtc, BrigadeEntity brigade) =>
        (endUtc - startUtc).TotalMinutes * -1 + (10.0 / brigade.CountEmployee) * 100 +
        (DateTime.UtcNow - startUtc).TotalMinutes * 0.1;

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
        _logger.LogInformation($"\n Создан рабочий период {wp.Id} для продукта {product.Id} \n");

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
            await _wpStageBrigadeRepository.Add(rel);
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