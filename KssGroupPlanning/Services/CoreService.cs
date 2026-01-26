using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;

public class CoreService
{
    private readonly INewProductRepository _productRepository;
    private readonly INewWorkingPeriodRepository _workingPeriodRepository;
    private readonly INewWorkingPeriodStageRepository _workingPeriodStageRepository;
    private readonly INewProductSubTypeWorkingPeriodSampleRepository _productSubTypeWorkingPeriodSampleRepository;
    private readonly INewFactoryRepository _factoryRepository;
    private readonly INewBrigadeRepository _brigadeRepository;
    private readonly INewWorkingPeriodStageBrigadeRelationRepository _workingPeriodStageBrigadeRelationRepository;
    private readonly INewWorkingPeriodRelationRepository _workingPeriodRelationRepository;
    private readonly INewWorkingPeriodStageTypeRelationRepository _workingPeriodStageTypeRelationRepository;
    private readonly INewStageTypeRepository _stageTypeRepository;
    private readonly ILogger<CoreService> _logger;

    private List<ProductEntity>? _products;
    private List<WorkingPeriodEntity> _allWorkingPeriods;
    private List<WorkingPeriodStageEntity> _allWorkingPeriodStages;
    private Dictionary<Guid, List<BrigadeEntity>> _factoryBrigades;
    private Dictionary<string, Guid> _stageTypeIds;

    private Dictionary<Guid, BrigadeScheduler> _factoryBrigadeSchedulers = new Dictionary<Guid, BrigadeScheduler>();

    // Словарь для отслеживания времени завершения бригад
    private Dictionary<Guid, DateTime> _brigadeNextAvailableTime = new Dictionary<Guid, DateTime>();

    // Словарь для отслеживания занятых слотов по бригадам
    private Dictionary<Guid, List<BrigadeTimeSlot>> _brigadeTimeSlots = new Dictionary<Guid, List<BrigadeTimeSlot>>();

    // Словарь для отслеживания бригад сборки, назначенных на продукты
    private Dictionary<Guid, Guid> _productAssemblyBrigadeMapping = new Dictionary<Guid, Guid>();

    // Константы для рабочего времени
    private readonly TimeSpan WORKDAY_START = new TimeSpan(8, 0, 0);
    private readonly TimeSpan WORKDAY_END = new TimeSpan(20, 0, 0);
    private readonly TimeSpan WORKDAY_DURATION = new TimeSpan(12, 0, 0);

    public CoreService(
        INewProductRepository productRepository,
        INewWorkingPeriodRepository workingPeriodRepository,
        INewWorkingPeriodStageRepository workingPeriodStageRepository,
        INewProductSubTypeWorkingPeriodSampleRepository productSubTypeWorkingPeriodSampleRepository,
        INewFactoryRepository factoryRepository,
        INewBrigadeRepository brigadeRepository,
        INewWorkingPeriodStageBrigadeRelationRepository workingPeriodStageBrigadeRelationRepository,
        INewWorkingPeriodRelationRepository workingPeriodRelationRepository,
        INewWorkingPeriodStageTypeRelationRepository workingPeriodStageTypeRelationRepository,
        INewStageTypeRepository stageTypeRepository,
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
        _logger = logger;
    }

    public async Task PlanProductionAsync()
    {
        try
        {
            _logger.LogWarning("Начало планирования производства");

            _products = await _productRepository.GetAll();
            _allWorkingPeriods = await _workingPeriodRepository.GetAll();
            _allWorkingPeriodStages = await _workingPeriodStageRepository.GetAll();

            _logger.LogWarning($"Загружено: {_products?.Count ?? 0} продуктов, " +
                                 $"{_allWorkingPeriods.Count} рабочих периодов, " +
                                 $"{_allWorkingPeriodStages.Count} этапов");

            // Тестируем преобразование времени
            await TestStandartTimeParsing();

            // Загружаем бригады по фабрикам
            await LoadFactoryBrigades();

            // Загружаем ID типов этапов
            await LoadStageTypeIds();

            if (_products == null || !_products.Any())
            {
                _logger.LogWarning("Нет продуктов для планирования");
                return;
            }

            // 1. Сортируем продукты по EndDate (приоритет по срокам)
            SortProductsByEndDate();
            _logger.LogWarning($"Отсортировано {_products.Count} продуктов");

            // 2. Группируем продукты по фабрикам
            var productsByFactory = _products
                .GroupBy(p => p.FactoryId)
                .ToDictionary(g => g.Key, g => g.ToList());

            _logger.LogWarning($"Продукты сгруппированы по {productsByFactory.Count} фабрикам");

            // 3. Для каждой фабрики планируем продукты
            int plannedCount = 0;
            foreach (var factoryGroup in productsByFactory)
            {
                var factoryId = factoryGroup.Key;
                var factoryProducts = factoryGroup.Value;

                _logger.LogWarning($"Планирование для фабрики {factoryId}: {factoryProducts.Count} продуктов");

                // Инициализируем время доступности бригад
                await InitializeBrigadeAvailability(factoryId);

                // Загружаем занятые слоты для бригад
                await LoadBrigadeTimeSlotsAsync(factoryId);

                // Планируем каждый продукт
                foreach (var product in factoryProducts)
                {
                    _logger.LogWarning($"Обработка продукта {product.Id} ({product.Number})");
                    var result = await PlanProductWithBrigadeSelectionAsync(product, factoryId);
                    if (result) plannedCount++;
                }
            }

            _logger.LogWarning($"Успешно запланировано {plannedCount} продуктов из {_products.Count}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при планировании производства");
            throw;
        }
    }

    private async Task LoadBrigadeTimeSlotsAsync(Guid factoryId)
    {
        try
        {
            if (_factoryBrigades.TryGetValue(factoryId, out var factoryBrigades))
            {
                foreach (var brigade in factoryBrigades)
                {
                    _brigadeTimeSlots[brigade.Id] = await GetBrigadeBusyTimeSlotsAsync(brigade.Id);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при загрузке временных слотов для бригад фабрики {factoryId}");
        }
    }

    private async Task<List<BrigadeTimeSlot>> GetBrigadeBusyTimeSlotsAsync(Guid brigadeId)
    {
        var timeSlots = new List<BrigadeTimeSlot>();

        try
        {
            // Получаем связи этой бригады с этапами
            var brigadeRelations = await _workingPeriodStageBrigadeRelationRepository
                .GetByBrigadeId(brigadeId);

            if (brigadeRelations != null && brigadeRelations.Any())
            {
                var stageIds = brigadeRelations.Select(r => r.WorkingPeriodStageId).ToList();
                var stages = _allWorkingPeriodStages
                    .Where(s => stageIds.Contains(s.Id))
                    .ToList();

                foreach (var stage in stages)
                {
                    // Получаем информацию о требуемых сотрудниках
                    var stageSample = await _productSubTypeWorkingPeriodSampleRepository
                        .GetById(stage.ProductSubTypeWorkingPeriodSampleId);

                    if (stageSample != null)
                    {
                        timeSlots.Add(new BrigadeTimeSlot
                        {
                            Start = FromUtc(stage.DateFrom),
                            End = FromUtc(stage.DateTo),
                            ProductId = GetProductIdByWorkingPeriodId(stage.WorkingPeriodId),
                            StageId = stage.Id,
                            BrigadeId = brigadeId,
                            RequiredEmployees = stageSample.StandartEmployee
                        });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при получении занятых слотов для бригады {brigadeId}");
        }

        return timeSlots;
    }

    private Guid GetProductIdByWorkingPeriodId(Guid workingPeriodId)
    {
        var workingPeriod = _allWorkingPeriods.FirstOrDefault(wp => wp.Id == workingPeriodId);
        return workingPeriod?.ProductId ?? Guid.Empty;
    }

    private async Task InitializeBrigadeAvailability(Guid factoryId)
    {
        _brigadeNextAvailableTime.Clear();

        // Получаем все бригады на фабрике
        if (_factoryBrigades.TryGetValue(factoryId, out var factoryBrigades))
        {
            foreach (var brigade in factoryBrigades)
            {
                // Инициализируем время доступности как текущее время
                _brigadeNextAvailableTime[brigade.Id] = DateTime.Now;

                // Логируем инициализацию
                _logger.LogWarning($"Бригада {brigade.Id} инициализирована, доступна с {_brigadeNextAvailableTime[brigade.Id]}");
            }
        }
    }

    private async Task<bool> PlanProductWithBrigadeSelectionAsync(ProductEntity product, Guid factoryId)
    {
        try
        {
            _logger.LogWarning($"Начало планирования продукта {product.Id} с выбором бригады");

            // Проверяем, есть ли уже рабочий период
            var existingWorkingPeriod = _allWorkingPeriods
                .FirstOrDefault(wp => wp.ProductId == product.Id);

            if (existingWorkingPeriod != null)
            {
                _logger.LogWarning($"У продукта {product.Id} уже есть рабочий период {existingWorkingPeriod.Id}");
                return false;
            }

            // Получаем этапы сборки для продукта
            _logger.LogDebug($"Получение этапов для ProductSubTypeId: {product.ProductSubTypeId}");
            var productStages = await _productSubTypeWorkingPeriodSampleRepository
                .GetByProductSubTypeId(product.ProductSubTypeId);

            if (productStages == null || !productStages.Any())
            {
                _logger.LogWarning($"Для продукта {product.Id} не найдены этапы сборки");
                return false;
            }

            _logger.LogWarning($"Найдено {productStages.Count} этапов");

            // Строим граф зависимостей этапов
            var stageGraph = await BuildStageDependencyGraphAsync(productStages);
            _logger.LogWarning($"Построен граф из {stageGraph.Count} узлов");

            // Получаем план выполнения с учетом зависимостей
            var executionPlan = await GetStageExecutionPlanAsync(stageGraph);
            _logger.LogWarning($"Создан план из {executionPlan.Count} групп выполнения");

            // Проверяем, что у нас есть корневые этапы
            var rootStages = stageGraph.Where(n => !n.Parents.Any()).ToList();
            _logger.LogWarning($"Корневых этапов (без родителей): {rootStages.Count}");

            if (rootStages.Count == 0)
            {
                _logger.LogError("Нет корневых этапов! Возможно, цикличные зависимости.");
                return false;
            }

            // Получаем типы для каждого этапа
            var stageTypes = await GetStageTypesForGraphAsync(stageGraph);

            // Определяем, есть ли этапы сборки
            var assemblyStageTypeId = _stageTypeIds.GetValueOrDefault("Сборка");
            var hasAssemblyStages = stageTypes.Any(kv => kv.Value == assemblyStageTypeId);

            if (hasAssemblyStages)
            {
                // Для продуктов с этапами сборки используем специальную логику
                return await PlanProductWithAssemblyStagesAsync(
                    product, factoryId, stageGraph, executionPlan, stageTypes);
            }
            else
            {
                // Для продуктов без этапов сборки распределяем этапы по соответствующим бригадам
                return await PlanProductWithoutAssemblyStagesAsync(
                    product, factoryId, stageGraph, executionPlan, stageTypes);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при планировании продукта {product.Id}");
            return false;
        }
    }

    private async Task<bool> PlanProductWithAssemblyStagesAsync(
        ProductEntity product,
        Guid factoryId,
        List<StageNode> stageGraph,
        List<StageExecutionGroup> executionPlan,
        Dictionary<Guid, Guid> stageTypes)
    {
        try
        {
            _logger.LogWarning($"Планирование продукта {product.Id} с этапами сборки");

            var assemblyStageTypeId = _stageTypeIds.GetValueOrDefault("Сборка");

            // 1. Выбираем бригаду для этапов сборки
            var assemblyBrigades = await _brigadeRepository.GetByStageTypeId(assemblyStageTypeId);
            var factoryAssemblyBrigades = assemblyBrigades?
                .Where(b => b.FactoryId == factoryId)
                .ToList();

            if (factoryAssemblyBrigades == null || !factoryAssemblyBrigades.Any())
            {
                _logger.LogError($"На фабрике {factoryId} нет бригад типа 'Сборка'");
                return false;
            }

            // Выбираем лучшую бригаду для сборки
            var bestAssemblyBrigade = await SelectBestAssemblyBrigadeAsync(
                product, factoryAssemblyBrigades, stageGraph, executionPlan, stageTypes, factoryId);

            if (!bestAssemblyBrigade.HasValue)
            {
                _logger.LogError($"Не удалось выбрать бригаду сборки для продукта {product.Id}");
                return false;
            }

            var (assemblyBrigadeId, stageToBrigadeMapping, startTime, endTime) = bestAssemblyBrigade.Value;

            // Запоминаем назначение бригады сборки для этого продукта
            _productAssemblyBrigadeMapping[product.Id] = assemblyBrigadeId;

            // 2. Планируем продукт с выбранным маппингом бригад
            var plannedStages = await PlanProductWithBrigadeMappingAsync(
                product, stageGraph, executionPlan, stageToBrigadeMapping, startTime, factoryId);

            if (plannedStages == null || !plannedStages.Any())
            {
                _logger.LogWarning($"Не удалось запланировать этапы для продукта {product.Id}");
                return false;
            }

            // 3. Создаем рабочий период
            await CreateWorkingPeriodAsync(product, plannedStages);
            _logger.LogInformation($"Создан рабочий период для продукта {product.Id}");

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при планировании продукта с этапами сборки {product.Id}");
            return false;
        }
    }

    private async Task<bool> PlanProductWithoutAssemblyStagesAsync(
        ProductEntity product,
        Guid factoryId,
        List<StageNode> stageGraph,
        List<StageExecutionGroup> executionPlan,
        Dictionary<Guid, Guid> stageTypes)
    {
        try
        {
            _logger.LogWarning($"Планирование продукта {product.Id} без этапов сборки");

            // Создаем маппинг этапов на бригады
            var stageToBrigadeMapping = await CreateStageToBrigadeMappingAsync(
                product, stageGraph, stageTypes, null, factoryId);

            if (!stageToBrigadeMapping.Any())
            {
                _logger.LogError($"Не удалось создать маппинг бригад для продукта {product.Id}");
                return false;
            }

            // Определяем время начала планирования
            DateTime startTime = DateTime.Now;
            foreach (var brigadeId in stageToBrigadeMapping.Values.Distinct())
            {
                if (_brigadeNextAvailableTime.ContainsKey(brigadeId) &&
                    _brigadeNextAvailableTime[brigadeId] > startTime)
                {
                    startTime = _brigadeNextAvailableTime[brigadeId];
                }
            }

            // Планируем продукт
            var plannedStages = await PlanProductWithBrigadeMappingAsync(
                product, stageGraph, executionPlan, stageToBrigadeMapping, startTime, factoryId);

            if (plannedStages == null || !plannedStages.Any())
            {
                _logger.LogWarning($"Не удалось запланировать этапы для продукта {product.Id}");
                return false;
            }

            // Создаем рабочий период
            await CreateWorkingPeriodAsync(product, plannedStages);
            _logger.LogInformation($"Создан рабочий период для продукта {product.Id}");

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при планировании продукта без этапов сборки {product.Id}");
            return false;
        }
    }

    private async Task<(Guid BrigadeId, Dictionary<Guid, Guid> StageMapping, DateTime StartTime, DateTime EndTime)?>
        SelectBestAssemblyBrigadeAsync(
            ProductEntity product,
            List<BrigadeEntity> assemblyBrigades,
            List<StageNode> stageGraph,
            List<StageExecutionGroup> executionPlan,
            Dictionary<Guid, Guid> stageTypes,
            Guid factoryId)
    {
        var evaluations = new List<AssemblyBrigadeEvaluation>();
        var assemblyStageTypeId = _stageTypeIds.GetValueOrDefault("Сборка");

        foreach (var brigade in assemblyBrigades)
        {
            try
            {
                // Создаем маппинг этапов на бригады для этой бригады сборки
                var stageToBrigadeMapping = await CreateStageToBrigadeMappingAsync(
                    product, stageGraph, stageTypes, brigade.Id, factoryId);

                if (!stageToBrigadeMapping.Any())
                {
                    _logger.LogDebug($"Не удалось создать маппинг для бригады {brigade.Id}");
                    continue;
                }

                // Оцениваем время выполнения с этим маппингом
                var timeEstimation = await EstimateProductTimeWithMappingAsync(
                    product, stageGraph, executionPlan, stageToBrigadeMapping, factoryId);

                if (!timeEstimation.HasValue)
                {
                    _logger.LogDebug($"Не удалось оценить время для бригады {brigade.Id}");
                    continue;
                }

                var (startTime, endTime) = timeEstimation.Value;

                evaluations.Add(new AssemblyBrigadeEvaluation
                {
                    BrigadeId = brigade.Id,
                    StageToBrigadeMapping = stageToBrigadeMapping,
                    StartTime = startTime,
                    EndTime = endTime,
                    Duration = endTime - startTime,
                    Score = CalculateBrigadeTimeScore(startTime, endTime, brigade)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при оценке бригады {brigade.Id}");
            }
        }

        if (!evaluations.Any())
        {
            return null;
        }

        // Выбираем бригаду с наилучшим результатом
        var bestEvaluation = evaluations
            .OrderBy(b => b.EndTime)
            .ThenBy(b => b.Duration)
            .First();

        return (bestEvaluation.BrigadeId, bestEvaluation.StageToBrigadeMapping,
                bestEvaluation.StartTime, bestEvaluation.EndTime);
    }

    private async Task<Dictionary<Guid, Guid>> CreateStageToBrigadeMappingAsync(
        ProductEntity product,
        List<StageNode> stageGraph,
        Dictionary<Guid, Guid> stageTypes,
        Guid? assemblyBrigadeId,
        Guid factoryId)
    {
        var stageToBrigadeMapping = new Dictionary<Guid, Guid>();
        var assemblyStageTypeId = _stageTypeIds.GetValueOrDefault("Сборка");

        // Получаем все бригады на фабрике
        var factoryBrigades = await _brigadeRepository.GetByFactoryId(factoryId);
        if (factoryBrigades == null || !factoryBrigades.Any())
        {
            _logger.LogError($"На фабрике {factoryId} нет бригад");
            return stageToBrigadeMapping;
        }

        // Группируем бригады по типу
        var brigadesByType = factoryBrigades
            .GroupBy(b => b.StageTypeId)
            .ToDictionary(g => g.Key, g => g.ToList());

        // Словарь для кэширования выбранных бригад для типов этапов
        var brigadeForStageType = new Dictionary<Guid, Guid>();

        foreach (var node in stageGraph)
        {
            var stageTypeId = stageTypes[node.Id];
            Guid selectedBrigadeId;

            // Если это этап сборки и указана бригада сборки
            if (stageTypeId == assemblyStageTypeId && assemblyBrigadeId.HasValue)
            {
                selectedBrigadeId = assemblyBrigadeId.Value;
            }
            else
            {
                // Для других типов этапов
                if (!brigadeForStageType.ContainsKey(stageTypeId))
                {
                    if (!brigadesByType.ContainsKey(stageTypeId))
                    {
                        _logger.LogWarning($"Нет бригад типа {stageTypeId} на фабрике {factoryId}");

                        // Если есть бригада сборки, используем ее как fallback
                        if (assemblyBrigadeId.HasValue)
                        {
                            selectedBrigadeId = assemblyBrigadeId.Value;
                        }
                        else
                        {
                            _logger.LogError($"Не удалось найти бригаду для этапа {node.Id}");
                            continue;
                        }
                    }
                    else
                    {
                        // Выбираем подходящую бригаду для этого типа этапа
                        var suitableBrigades = brigadesByType[stageTypeId]
                            .Where(b => b.CountEmployee >= node.Stage.StandartEmployee)
                            .ToList();

                        if (!suitableBrigades.Any())
                        {
                            _logger.LogWarning($"Нет подходящих бригад для этапа {node.Id} (требуется {node.Stage.StandartEmployee} сотрудников)");

                            // Если есть бригада сборки, используем ее как fallback
                            if (assemblyBrigadeId.HasValue)
                            {
                                selectedBrigadeId = assemblyBrigadeId.Value;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            // Выбираем бригаду с наименьшей текущей загрузкой
                            var bestBrigade = suitableBrigades
                                .OrderBy(b => _brigadeNextAvailableTime.GetValueOrDefault(b.Id, DateTime.MinValue))
                                .First();

                            brigadeForStageType[stageTypeId] = bestBrigade.Id;
                            selectedBrigadeId = bestBrigade.Id;
                        }
                    }
                }
                else
                {
                    selectedBrigadeId = brigadeForStageType[stageTypeId];
                }
            }

            stageToBrigadeMapping[node.Id] = selectedBrigadeId;
        }

        return stageToBrigadeMapping;
    }

    private async Task<(DateTime StartTime, DateTime EndTime)?> EstimateProductTimeWithMappingAsync(
        ProductEntity product,
        List<StageNode> stageGraph,
        List<StageExecutionGroup> executionPlan,
        Dictionary<Guid, Guid> stageToBrigadeMapping,
        Guid factoryId)
    {
        try
        {
            // Создаем временные копии для симуляции
            var simulatedBrigadeTimes = new Dictionary<Guid, DateTime>();
            var simulatedTimeSlots = new Dictionary<Guid, List<BrigadeTimeSlot>>();

            // Инициализируем временные копии
            foreach (var brigadeId in stageToBrigadeMapping.Values.Distinct())
            {
                simulatedBrigadeTimes[brigadeId] = _brigadeNextAvailableTime.ContainsKey(brigadeId)
                    ? _brigadeNextAvailableTime[brigadeId]
                    : DateTime.Now;

                simulatedTimeSlots[brigadeId] = _brigadeTimeSlots.ContainsKey(brigadeId)
                    ? new List<BrigadeTimeSlot>(_brigadeTimeSlots[brigadeId])
                    : new List<BrigadeTimeSlot>();
            }

            var stageCompletionTimes = new Dictionary<Guid, DateTime>();

            // Симулируем выполнение этапов
            foreach (var group in executionPlan.OrderBy(g => g.Level))
            {
                foreach (var stage in group.Stages)
                {
                    var node = stageGraph.FirstOrDefault(n => n.Id == stage.Id);
                    if (node == null) continue;

                    var brigadeId = stageToBrigadeMapping[node.Id];
                    var currentBrigadeTime = simulatedBrigadeTimes[brigadeId];

                    // Учитываем зависимости от родительских этапов
                    if (node.Parents.Any())
                    {
                        var parentEndTimes = node.Parents
                            .Where(parentId => stageCompletionTimes.ContainsKey(parentId))
                            .Select(parentId => stageCompletionTimes[parentId])
                            .ToList();

                        if (parentEndTimes.Any())
                        {
                            var maxParentEndTime = parentEndTimes.Max();
                            if (maxParentEndTime > currentBrigadeTime)
                            {
                                currentBrigadeTime = maxParentEndTime;
                            }
                        }
                    }

                    // Ищем свободное время для этапа
                    var duration = ParseStandartTimeToTimeSpan(stage.StandartTime);
                    var plannedTime = await FindAvailableTimeSlotForBrigadeAsync(
                        currentBrigadeTime,
                        duration,
                        brigadeId,
                        stage.StandartEmployee,
                        simulatedTimeSlots[brigadeId],
                        factoryId);

                    if (!plannedTime.HasValue)
                    {
                        return null;
                    }

                    var stageEndTime = plannedTime.Value.Add(duration);
                    stageCompletionTimes[stage.Id] = stageEndTime;
                    simulatedBrigadeTimes[brigadeId] = stageEndTime;

                    // Добавляем слот в симулированные слоты
                    simulatedTimeSlots[brigadeId].Add(new BrigadeTimeSlot
                    {
                        Start = plannedTime.Value,
                        End = stageEndTime,
                        ProductId = product.Id,
                        StageId = stage.Id,
                        BrigadeId = brigadeId,
                        RequiredEmployees = stage.StandartEmployee
                    });
                }
            }

            if (!stageCompletionTimes.Any())
            {
                return null;
            }

            var startTime = stageCompletionTimes.Values.Min();
            var endTime = stageCompletionTimes.Values.Max();

            return (startTime, endTime);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при оценке времени с маппингом");
            return null;
        }
    }

    private async Task<List<PlannedStage>> PlanProductWithBrigadeMappingAsync(
        ProductEntity product,
        List<StageNode> stageGraph,
        List<StageExecutionGroup> executionPlan,
        Dictionary<Guid, Guid> stageToBrigadeMapping,
        DateTime startTime,
        Guid factoryId)
    {
        try
        {
            _logger.LogWarning($"Планирование продукта {product.Id} с распределением по бригадам");

            var plannedStages = new List<PlannedStage>();
            var stageCompletionTimes = new Dictionary<Guid, DateTime>();

            // Словарь для отслеживания времени бригад
            var brigadeCurrentTimes = new Dictionary<Guid, DateTime>();

            // Инициализируем время бригад
            foreach (var brigadeId in stageToBrigadeMapping.Values.Distinct())
            {
                brigadeCurrentTimes[brigadeId] = _brigadeNextAvailableTime.ContainsKey(brigadeId)
                    ? _brigadeNextAvailableTime[brigadeId]
                    : startTime;
            }

            // Проходим по плану выполнения
            foreach (var group in executionPlan.OrderBy(g => g.Level))
            {
                foreach (var stage in group.Stages)
                {
                    var node = stageGraph.FirstOrDefault(n => n.Id == stage.Id);
                    if (node == null) continue;

                    var brigadeId = stageToBrigadeMapping[node.Id];
                    var currentBrigadeTime = brigadeCurrentTimes[brigadeId];

                    // Учитываем зависимости
                    if (node.Parents.Any())
                    {
                        var parentEndTimes = node.Parents
                            .Where(parentId => stageCompletionTimes.ContainsKey(parentId))
                            .Select(parentId => stageCompletionTimes[parentId])
                            .ToList();

                        if (parentEndTimes.Any())
                        {
                            var maxParentEndTime = parentEndTimes.Max();
                            if (maxParentEndTime > currentBrigadeTime)
                            {
                                currentBrigadeTime = maxParentEndTime;
                            }
                        }
                    }

                    // Ищем свободное время для этапа
                    var duration = ParseStandartTimeToTimeSpan(stage.StandartTime);
                    var plannedTime = await FindAvailableTimeSlotForBrigadeAsync(
                        currentBrigadeTime,
                        duration,
                        brigadeId,
                        stage.StandartEmployee,
                        _brigadeTimeSlots.GetValueOrDefault(brigadeId, new List<BrigadeTimeSlot>()),
                        factoryId);

                    if (!plannedTime.HasValue)
                    {
                        _logger.LogError($"Не удалось найти время для этапа {stage.Id} на бригаде {brigadeId}");
                        return new List<PlannedStage>();
                    }

                    var stageEndTime = plannedTime.Value.Add(duration);

                    // Получаем тип этапа
                    var stageTypeId = await GetStageTypeIdAsync(stage.Id);

                    // Создаем запланированный этап
                    var plannedStage = new PlannedStage
                    {
                        ProductId = product.Id,
                        StageSampleId = stage.Id,
                        BrigadeId = brigadeId,
                        StartTime = plannedTime.Value,
                        EndTime = stageEndTime,
                        StageTypeId = stageTypeId,
                        RequiredEmployees = stage.StandartEmployee
                    };

                    plannedStages.Add(plannedStage);
                    stageCompletionTimes[stage.Id] = stageEndTime;
                    brigadeCurrentTimes[brigadeId] = stageEndTime;

                    // Обновляем занятые слоты бригады
                    if (!_brigadeTimeSlots.ContainsKey(brigadeId))
                    {
                        _brigadeTimeSlots[brigadeId] = new List<BrigadeTimeSlot>();
                    }

                    _brigadeTimeSlots[brigadeId].Add(new BrigadeTimeSlot
                    {
                        Start = plannedStage.StartTime,
                        End = plannedStage.EndTime,
                        ProductId = product.Id,
                        StageId = stage.Id,
                        BrigadeId = brigadeId,
                        RequiredEmployees = stage.StandartEmployee
                    });
                }
            }

            // Обновляем время доступности бригад
            foreach (var brigadeId in brigadeCurrentTimes.Keys)
            {
                _brigadeNextAvailableTime[brigadeId] = brigadeCurrentTimes[brigadeId];
            }

            return plannedStages;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при планировании продукта {product.Id} с маппингом бригад");
            return new List<PlannedStage>();
        }
    }

    private async Task<DateTime?> FindAvailableTimeSlotForBrigadeAsync(
        DateTime startFrom,
        TimeSpan duration,
        Guid brigadeId,
        int requiredEmployees,
        List<BrigadeTimeSlot> brigadeTimeSlots,
        Guid factoryId)
    {
        try
        {
            _logger.LogDebug($"Поиск слота для бригады {brigadeId}: начало с {startFrom}, " +
                           $"длительность={duration.TotalMinutes} минут, " +
                           $"сотрудников={requiredEmployees}");

            // Получаем информацию о бригаде
            var brigade = await _brigadeRepository.GetById(brigadeId);
            if (brigade == null)
            {
                _logger.LogError($"Бригада {brigadeId} не найдена");
                return null;
            }

            var totalEmployees = brigade.CountEmployee;

            // Если требуется больше сотрудников, чем есть в бригаде
            if (requiredEmployees > totalEmployees)
            {
                _logger.LogError($"Бригада {brigadeId} имеет {totalEmployees} сотрудников, " +
                               $"требуется {requiredEmployees}");
                return null;
            }

            DateTime currentTime = AdjustToWorkHours(startFrom);
            int maxDaysToSearch = 30;
            int daysSearched = 0;

            while (daysSearched < maxDaysToSearch)
            {
                // Пропускаем выходные
                if (currentTime.DayOfWeek == DayOfWeek.Saturday || currentTime.DayOfWeek == DayOfWeek.Sunday)
                {
                    currentTime = currentTime.Date.AddDays(1).Add(WORKDAY_START);
                    daysSearched++;
                    continue;
                }

                // Рабочий день
                DateTime workDayStart = currentTime.Date.Add(WORKDAY_START);
                DateTime workDayEnd = currentTime.Date.Add(WORKDAY_END);

                // Начинаем поиск с currentTime или начала рабочего дня
                DateTime searchStart = currentTime > workDayStart ? currentTime : workDayStart;

                // Получаем занятые слоты на этот день для этой бригады
                var dayBusySlots = brigadeTimeSlots
                    .Where(s => s.BrigadeId == brigadeId &&
                               s.Start.Date == currentTime.Date)
                    .OrderBy(s => s.Start)
                    .ToList();

                // Если день полностью свободен
                if (!dayBusySlots.Any())
                {
                    // Проверяем, помещается ли этап в рабочий день
                    DateTime candidateStart = searchStart;
                    DateTime candidateEnd = candidateStart.Add(duration);

                    if (candidateEnd.TimeOfDay <= WORKDAY_END)
                    {
                        // Проверяем доступность сотрудников (все свободны)
                        return candidateStart;
                    }
                }
                else
                {
                    // Ищем промежутки между занятыми слотами
                    DateTime lastEnd = searchStart;

                    foreach (var busySlot in dayBusySlots)
                    {
                        if (busySlot.End <= lastEnd)
                            continue;

                        if (busySlot.Start > lastEnd)
                        {
                            TimeSpan gap = busySlot.Start - lastEnd;

                            if (gap >= duration)
                            {
                                DateTime candidateStart = lastEnd;
                                DateTime candidateEnd = candidateStart.Add(duration);

                                if (candidateEnd.TimeOfDay <= WORKDAY_END)
                                {
                                    // Проверяем, достаточно ли сотрудников в этом промежутке
                                    // (упрощенная проверка - в реальности нужен более сложный алгоритм)
                                    if (await CheckBrigadeAvailabilityAsync(brigadeId, candidateStart, duration, factoryId))
                                    {
                                        return candidateStart;
                                    }
                                }
                            }
                        }

                        // Проверяем, сколько сотрудников занято в этот момент
                        var overlappingSlots = dayBusySlots
                            .Where(s => s.Start < busySlot.End && s.End > busySlot.Start)
                            .ToList();

                        int busyEmployees = overlappingSlots.Sum(s => s.RequiredEmployees);
                        int availableEmployees = totalEmployees - busyEmployees;

                        if (availableEmployees >= requiredEmployees)
                        {
                            // Можно выполнять параллельно
                            DateTime candidateStart = busySlot.Start;
                            DateTime candidateEnd = candidateStart.Add(duration);

                            if (candidateEnd.TimeOfDay <= WORKDAY_END &&
                                candidateEnd <= busySlot.End)
                            {
                                return candidateStart;
                            }
                        }

                        if (busySlot.End > lastEnd)
                        {
                            lastEnd = busySlot.End;
                        }
                    }

                    // Проверяем конец дня
                    if (workDayEnd - lastEnd >= duration)
                    {
                        DateTime candidateStart = lastEnd;
                        DateTime candidateEnd = candidateStart.Add(duration);

                        if (candidateEnd.TimeOfDay <= WORKDAY_END)
                        {
                            return candidateStart;
                        }
                    }
                }

                // Переходим к следующему рабочему дню
                daysSearched++;
                currentTime = currentTime.Date.AddDays(1).Add(WORKDAY_START);
            }

            _logger.LogWarning($"Не удалось найти свободный слот за {daysSearched} дней для бригады {brigadeId}");
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при поиске свободного слота для бригады");
            return null;
        }
    }

    private async Task<bool> CheckBrigadeAvailabilityAsync(
        Guid brigadeId,
        DateTime startTime,
        TimeSpan duration,
        Guid factoryId)
    {
        try
        {
            // Получаем все этапы, где занята эта бригада
            var brigadeRelations = await _workingPeriodStageBrigadeRelationRepository
                .GetByBrigadeId(brigadeId);

            if (brigadeRelations == null || !brigadeRelations.Any()) return true;

            // Получаем этапы для этих отношений
            var brigadeStageIds = brigadeRelations.Select(r => r.WorkingPeriodStageId).ToList();
            var brigadeStages = _allWorkingPeriodStages
                .Where(s => brigadeStageIds.Contains(s.Id))
                .ToList();

            DateTime endTime = startTime.Add(duration);

            // Преобразуем startTime в UTC для сравнения с данными из БД
            var startTimeUtc = ToUtc(startTime);
            var endTimeUtc = ToUtc(endTime);

            // Проверяем пересечения по времени
            foreach (var stage in brigadeStages)
            {
                // Данные из БД уже в UTC
                if (startTimeUtc < stage.DateTo && endTimeUtc > stage.DateFrom)
                {
                    return false;
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при проверке доступности бригады {brigadeId}");
            return false;
        }
    }

    private async Task<Dictionary<Guid, Guid>> GetStageTypesForGraphAsync(List<StageNode> stageGraph)
    {
        var stageTypes = new Dictionary<Guid, Guid>();

        foreach (var node in stageGraph)
        {
            var stageTypeId = await GetStageTypeIdAsync(node.Id);
            if (stageTypeId.HasValue)
            {
                stageTypes[node.Id] = stageTypeId.Value;
            }
        }

        return stageTypes;
    }

    private async Task<Guid?> GetStageTypeIdAsync(Guid stageSampleId)
    {
        try
        {
            var stageTypeRelations = await _workingPeriodStageTypeRelationRepository
                .GetByProductSubTypeWorkingPeriodSampleId(stageSampleId);

            return stageTypeRelations?.FirstOrDefault()?.StageTypeId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при получении типа этапа для {stageSampleId}");
            return null;
        }
    }

    private double CalculateBrigadeTimeScore(DateTime startTime, DateTime endTime, BrigadeEntity brigade)
    {
        // Чем раньше завершение - тем лучше
        double score = (endTime - DateTime.Now).TotalMinutes * -1;

        // Учитываем загрузку бригады
        score += (10.0 / brigade.CountEmployee) * 100;

        return score;
    }

    // Остальные методы остаются в основном без изменений, но адаптируются под новую логику

    private async Task<List<StageNode>> BuildStageDependencyGraphAsync(
        List<ProductSubTypeWorkingPeriodSampleEntity> stages)
    {
        var nodes = new Dictionary<Guid, StageNode>();

        // Создаем узлы для всех этапов
        foreach (var stage in stages)
        {
            nodes[stage.Id] = new StageNode
            {
                Stage = stage,
                Id = stage.Id,
                Parents = new List<Guid>(),
                Children = new List<Guid>(),
                Status = StageStatus.Pending
            };
        }

        // Заполняем связи между узлами
        foreach (var stage in stages)
        {
            // Получаем зависимости этого этапа
            var parentRelations = await _workingPeriodRelationRepository
                .GetByChildProductSubTypeWorkingPeriodSampleId(stage.Id);

            if (parentRelations != null)
            {
                foreach (var relation in parentRelations)
                {
                    if (nodes.ContainsKey(relation.ParentProductSubTypeWorkingPeriodSampleId))
                    {
                        nodes[stage.Id].Parents.Add(relation.ParentProductSubTypeWorkingPeriodSampleId);
                        nodes[relation.ParentProductSubTypeWorkingPeriodSampleId].Children.Add(stage.Id);
                    }
                }
            }
        }

        return nodes.Values.ToList();
    }

    private async Task<List<StageExecutionGroup>> GetStageExecutionPlanAsync(
        List<StageNode> stageGraph)
    {
        var executionPlan = new List<StageExecutionGroup>();
        var remainingNodes = stageGraph.ToList();
        int level = 0;

        while (remainingNodes.Any())
        {
            level++;

            // Находим узлы, у которых все родители уже обработаны
            var readyNodes = remainingNodes
                .Where(node => !node.Parents.Any() ||
                              node.Parents.All(parentId =>
                                  !remainingNodes.Any(n => n.Id == parentId)))
                .ToList();

            if (!readyNodes.Any())
            {
                throw new InvalidOperationException("Обнаружен цикл в зависимостях этапов");
            }

            var executionGroup = new StageExecutionGroup
            {
                Stages = readyNodes.Select(n => n.Stage).ToList(),
                CanExecuteInParallel = true,
                Level = level
            };

            executionPlan.Add(executionGroup);

            // Удаляем обработанные узлы
            foreach (var node in readyNodes)
            {
                remainingNodes.RemoveAll(n => n.Id == node.Id);
            }
        }

        return executionPlan;
    }

    private async Task LoadFactoryBrigades()
    {
        try
        {
            _factoryBrigades = new Dictionary<Guid, List<BrigadeEntity>>();
            _factoryBrigadeSchedulers = new Dictionary<Guid, BrigadeScheduler>();

            var allFactories = await _factoryRepository.GetAll();
            _logger.LogWarning($"Загружено {allFactories.Count} фабрик");

            foreach (var factory in allFactories)
            {
                var factoryBrigades = await _brigadeRepository.GetByFactoryId(factory.Id);
                if (factoryBrigades != null)
                {
                    _factoryBrigades[factory.Id] = factoryBrigades;

                    var scheduler = new BrigadeScheduler(_logger);
                    scheduler.InitializeBrigades(factoryBrigades);
                    _factoryBrigadeSchedulers[factory.Id] = scheduler;

                    _logger.LogWarning($"Фабрика {factory.Id}: {factoryBrigades.Count} бригад");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при загрузке бригад по фабрикам");
            throw;
        }
    }

    private async Task LoadStageTypeIds()
    {
        try
        {
            _stageTypeIds = new Dictionary<string, Guid>();
            var allStageTypes = await _stageTypeRepository.GetAll();

            foreach (var stageType in allStageTypes)
            {
                _stageTypeIds[stageType.Name] = stageType.Id;
            }

            _logger.LogWarning($"Загружено {_stageTypeIds.Count} типов этапов");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при загрузке типов этапов");
            throw;
        }
    }

    private TimeSpan ParseStandartTimeToTimeSpan(string standartTime)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(standartTime))
            {
                return TimeSpan.Zero;
            }

            if (int.TryParse(standartTime, out int minutes))
            {
                return TimeSpan.FromMinutes(minutes);
            }

            if (TimeSpan.TryParse(standartTime, out TimeSpan timeSpan))
            {
                return timeSpan;
            }

            return TimeSpan.Zero;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при преобразовании StandartTime: '{standartTime}'");
            return TimeSpan.Zero;
        }
    }

    private DateTime AdjustToWorkHours(DateTime dateTime)
    {
        // Если это выходные, перемещаем на понедельник
        if (dateTime.DayOfWeek == DayOfWeek.Saturday)
        {
            dateTime = dateTime.AddDays(2).Date.Add(WORKDAY_START);
        }
        else if (dateTime.DayOfWeek == DayOfWeek.Sunday)
        {
            dateTime = dateTime.AddDays(1).Date.Add(WORKDAY_START);
        }

        // Если время вне рабочего дня, перемещаем на начало следующего рабочего дня
        if (dateTime.TimeOfDay < WORKDAY_START || dateTime.TimeOfDay >= WORKDAY_END)
        {
            var nextDay = dateTime.Date.AddDays(1);

            // Пропускаем выходные
            if (nextDay.DayOfWeek == DayOfWeek.Saturday)
            {
                nextDay = nextDay.AddDays(2);
            }
            else if (nextDay.DayOfWeek == DayOfWeek.Sunday)
            {
                nextDay = nextDay.AddDays(1);
            }

            return nextDay.Add(WORKDAY_START);
        }

        return dateTime;
    }

    private async Task CreateWorkingPeriodAsync(ProductEntity product, List<PlannedStage> plannedStages)
    {
        if (!plannedStages.Any()) return;

        try
        {
            _logger.LogWarning($"Создание рабочего периода для продукта {product.Id} с {plannedStages.Count} этапами");

            // Преобразуем даты в UTC перед сохранением
            var firstStartTimeUtc = ToUtc(plannedStages.First().StartTime);
            var lastEndTimeUtc = ToUtc(plannedStages.Last().EndTime);

            // Создаем рабочий период с UTC временем
            var workingPeriod = new WorkingPeriodEntity
            {
                Id = Guid.NewGuid(),
                Name = $"Период для продукта {product.Number}",
                Status = "В работе",
                ProductId = product.Id,
                DateFrom = firstStartTimeUtc,
                DateTo = lastEndTimeUtc,
                CreateTime = ToUtc(DateTime.Now),
                UpdateTime = ToUtc(DateTime.Now)
            };

            _logger.LogWarning($"Создан WorkingPeriod: {workingPeriod.Id}");

            await _workingPeriodRepository.Add(workingPeriod);
            _allWorkingPeriods.Add(workingPeriod);

            // Создаем этапы рабочего периода
            foreach (var plannedStage in plannedStages)
            {
                var startTimeUtc = ToUtc(plannedStage.StartTime);
                var endTimeUtc = ToUtc(plannedStage.EndTime);

                var workingPeriodStage = new WorkingPeriodStageEntity
                {
                    Id = Guid.NewGuid(),
                    WorkingPeriodId = workingPeriod.Id,
                    DateFrom = startTimeUtc,
                    DateTo = endTimeUtc,
                    Status = "В работе",
                    Recycling = null,
                    ProductSubTypeWorkingPeriodSampleId = plannedStage.StageSampleId,
                    CreateTime = ToUtc(DateTime.Now),
                    UpdateTime = ToUtc(DateTime.Now)
                };

                _logger.LogWarning($"Создан WorkingPeriodStage: {workingPeriodStage.Id}");

                await _workingPeriodStageRepository.Add(workingPeriodStage);
                _allWorkingPeriodStages.Add(workingPeriodStage);

                // Создаем связь с бригадой
                var brigadeRelation = new WorkingPeriodStageBrigadeRelationEntity
                {
                    Id = Guid.NewGuid(),
                    WorkingPeriodStageId = workingPeriodStage.Id,
                    BrigadeId = plannedStage.BrigadeId
                };

                _logger.LogWarning($"Создана связь с бригадой: {brigadeRelation.Id}");

                await _workingPeriodStageBrigadeRelationRepository.Add(brigadeRelation);
            }

            // Обновляем даты продукта
            product.StartDate = DateOnly.FromDateTime(plannedStages.First().StartTime);
            product.EndDate = DateOnly.FromDateTime(plannedStages.Last().EndTime);
            product.UpdateTime = DateTime.Now;

            await _productRepository.Update(product);

            _logger.LogWarning($"Продукт {product.Id} обновлен: StartDate={product.StartDate}, EndDate={product.EndDate}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при создании рабочего периода для продукта {product.Id}");
            throw;
        }
    }

    private DateTime ToUtc(DateTime dateTime)
    {
        if (dateTime.Kind == DateTimeKind.Unspecified)
        {
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Local).ToUniversalTime();
        }
        else if (dateTime.Kind == DateTimeKind.Local)
        {
            return dateTime.ToUniversalTime();
        }
        else
        {
            return dateTime;
        }
    }

    private DateTime FromUtc(DateTime dateTime)
    {
        if (dateTime.Kind == DateTimeKind.Unspecified)
        {
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc).ToLocalTime();
        }
        else if (dateTime.Kind == DateTimeKind.Utc)
        {
            return dateTime.ToLocalTime();
        }
        else
        {
            return dateTime;
        }
    }

    private void SortProductsByEndDate()
    {
        _products?.Sort((p1, p2) =>
        {
            if (p1.EndDate == p2.EndDate) return 0;
            if (p1.EndDate == null) return 1;
            if (p2.EndDate == null) return -1;
            return p1.EndDate.Value.CompareTo(p2.EndDate.Value);
        });
    }

    private async Task TestStandartTimeParsing()
    {
        try
        {
            var sampleStages = await _productSubTypeWorkingPeriodSampleRepository.GetAll();

            if (sampleStages != null && sampleStages.Any())
            {
                _logger.LogWarning("Тестирование преобразования StandartTime:");

                foreach (var stage in sampleStages.Take(5))
                {
                    TimeSpan duration = ParseStandartTimeToTimeSpan(stage.StandartTime);
                    _logger.LogWarning($"  Этап {stage.Id}: '{stage.StandartTime}' -> {duration.TotalMinutes} минут");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при тестировании преобразования StandartTime");
        }
    }

    private void AnalyzeUnplannedStages(
        List<ProductSubTypeWorkingPeriodSampleEntity> allStages,
        List<PlannedStage> plannedStages,
        List<StageNode> stageGraph)
    {
        var plannedIds = plannedStages.Select(p => p.StageSampleId).ToList();
        var unplanned = allStages.Where(s => !plannedIds.Contains(s.Id)).ToList();

        if (!unplanned.Any())
        {
            _logger.LogWarning("Все этапы успешно запланированы!");
            return;
        }

        _logger.LogError($"Не запланировано {unplanned.Count} этапов из {allStages.Count}");

        foreach (var stage in unplanned)
        {
            var node = stageGraph.FirstOrDefault(n => n.Id == stage.Id);
            if (node != null)
            {
                var plannedParents = node.Parents
                    .Where(p => plannedIds.Contains(p))
                    .ToList();
                var unplannedParents = node.Parents
                    .Where(p => !plannedIds.Contains(p))
                    .ToList();

                _logger.LogError($"Этап {stage.Id} ({stage.WorkingPeriodName}): " +
                               $"Родители всего: {node.Parents.Count}, " +
                               $"Запланировано: {plannedParents.Count}, " +
                               $"Не запланировано: {unplannedParents.Count}");
            }
        }
    }

    // Вспомогательные классы

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
        public ProductSubTypeWorkingPeriodSampleEntity Stage { get; set; }
        public List<Guid> Parents { get; set; } = new List<Guid>();
        public List<Guid> Children { get; set; } = new List<Guid>();
        public StageStatus Status { get; set; }
    }

    private enum StageStatus
    {
        Pending,
        Planned,
        InProgress,
        Completed
    }

    private class StageExecutionGroup
    {
        public List<ProductSubTypeWorkingPeriodSampleEntity> Stages { get; set; }
        public bool CanExecuteInParallel { get; set; }
        public DateTime? EarliestStartTime { get; set; }
        public int Level { get; set; }
    }

    private class AssemblyBrigadeEvaluation
    {
        public Guid BrigadeId { get; set; }
        public Dictionary<Guid, Guid> StageToBrigadeMapping { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public double Score { get; set; }
    }

    private class BrigadeScheduler
    {
        private readonly Dictionary<Guid, BrigadeWorkload> _brigadeWorkloads;
        private readonly ILogger<CoreService> _logger;

        public BrigadeScheduler(ILogger<CoreService> logger)
        {
            _brigadeWorkloads = new Dictionary<Guid, BrigadeWorkload>();
            _logger = logger;
        }

        public void InitializeBrigades(List<BrigadeEntity> brigades)
        {
            foreach (var brigade in brigades)
            {
                _brigadeWorkloads[brigade.Id] = new BrigadeWorkload
                {
                    BrigadeId = brigade.Id,
                    TotalEmployees = brigade.CountEmployee,
                    AvailableEmployees = brigade.CountEmployee,
                    NextAvailableTime = null,
                    ScheduledSlots = new List<BrigadeTimeSlot>(),
                    StageTypeId = brigade.StageTypeId
                };
            }
            _logger.LogWarning($"Инициализировано {_brigadeWorkloads.Count} бригад");
        }
    }

    private class BrigadeWorkload
    {
        public Guid BrigadeId { get; set; }
        public Guid StageTypeId { get; set; }
        public int TotalEmployees { get; set; }
        public int AvailableEmployees { get; set; }
        public DateTime? NextAvailableTime { get; set; }
        public List<BrigadeTimeSlot> ScheduledSlots { get; set; } = new List<BrigadeTimeSlot>();
        public double UtilizationRate { get; set; }
    }
}