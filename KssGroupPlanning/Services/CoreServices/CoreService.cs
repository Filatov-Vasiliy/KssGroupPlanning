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

    private Dictionary<Guid, BrigadeScheduler> _factoryBrigadeSchedulers = new();

    // Словарь для отслеживания времени завершения бригад (UTC)
    private Dictionary<Guid, DateTime> _brigadeNextAvailableTime = new();

    // Словарь для отслеживания занятых слотов по бригадам (даты в UTC)
    private Dictionary<Guid, List<BrigadeTimeSlot>> _brigadeTimeSlots = new();

    // Словарь для отслеживания бригад сборки, назначенных на продукты
    private Dictionary<Guid, Guid> _productAssemblyBrigadeMapping = new();

    // Словари для учета дочерних изделий
    private Dictionary<Guid, List<ProductEntity>> _childrenByParentId = null!;
    private Dictionary<Guid, DateTime> _productStageConstraintDates = null!; // ключ – StageId, значение – UTC

    // Константы для рабочего времени (время задано в UTC, т.к. фабрики в одном поясе и мы работаем в UTC)
    private readonly TimeSpan WORKDAY_START = new(8, 0, 0);
    private readonly TimeSpan WORKDAY_END = new(20, 0, 0);
    private readonly TimeSpan WORKDAY_DURATION = new(12, 0, 0);

    private DateTime _planningStartTimeUtc; // всегда UTC
    private List<Guid> _plannedProductIdsInThisRun = null!; // ID продуктов, запланированных в текущем запуске

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
            _logger.LogWarning($"Время старта планирования (UTC): {_planningStartTimeUtc:dd.MM.yyyy HH:mm}");

            _products = await _productRepository.GetAll();
            _allWorkingPeriods = await _workingPeriodRepository.GetAll();
            _allWorkingPeriodStages = await _workingPeriodStageRepository.GetAll();

            _logger.LogWarning($"Загружено: {_products?.Count ?? 0} продуктов, " +
                               $"{_allWorkingPeriods.Count} рабочих периодов, " +
                               $"{_allWorkingPeriodStages.Count} этапов");

            // Определяем продукты, которые нужно запланировать в этом запуске
            var productIdsToPlan = _products!
                .Where(p => !_allWorkingPeriods.Any(wp => wp.ProductId == p.Id))
                .Select(p => p.Id)
                .ToHashSet();

            await TestStandartTimeParsing();

            // Загружаем все фабрики и их бригады
            await LoadFactoryBrigades();

            // Инициализируем доступность бригад и загружаем занятые слоты для всех фабрик
            foreach (var factoryId in _factoryBrigades.Keys)
            {
                await InitializeBrigadeAvailability(factoryId);
                await LoadBrigadeTimeSlotsAsync(factoryId);
            }

            // Загружаем ID типов этапов
            await LoadStageTypeIds();

            if (_products == null || !_products.Any())
            {
                _logger.LogWarning("Нет продуктов для планирования");
                return;
            }

            // Сортируем продукты по EndDate (приоритет по срокам)
            SortProductsByEndDate();

            // --- Построение иерархии продуктов ---
            _childrenByParentId = _products
                .Where(p => p.ParentProductId.HasValue)
                .GroupBy(p => p.ParentProductId.Value)
                .ToDictionary(g => g.Key, g => g.ToList());

            _productStageConstraintDates = new Dictionary<Guid, DateTime>();

            // Планируем продукты, у которых нет родителя (корни иерархии)
            var rootProducts = _products.Where(p => p.ParentProductId == null).ToList();
            foreach (var rootProduct in rootProducts)
            {
                await PlanProductHierarchyAsync(rootProduct);
            }

            // На случай циклических зависимостей или продуктов с родителем, который не является корнем
            var unplannedProducts = _products
                .Where(p => !_allWorkingPeriods.Any(wp => wp.ProductId == p.Id))
                .ToList();
            if (unplannedProducts.Any())
            {
                _logger.LogWarning($"Остались незапланированные продукты: {unplannedProducts.Count}. Пробуем запланировать отдельно.");
                foreach (var product in unplannedProducts)
                {
                    await PlanProductWithBrigadeSelectionAsync(product, product.FactoryId);
                }
            }

            // Итоговый подсчёт: сколько из требуемых продуктов реально запланировано в этом запуске
            var successfullyPlannedCount = _plannedProductIdsInThisRun.Intersect(productIdsToPlan).Count();
            _logger.LogWarning($"Успешно запланировано {successfullyPlannedCount} продуктов из {productIdsToPlan.Count} (требовавших планирования). Всего продуктов в БД: {_products.Count}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при планировании производства");
            throw;
        }
    }

    // ========== Иерархия продуктов ==========
    private async Task<bool> PlanProductHierarchyAsync(ProductEntity product)
    {
        // Если уже запланирован – пропускаем
        if (_allWorkingPeriods.Any(wp => wp.ProductId == product.Id))
            return true;

        // Сначала планируем всех детей
        if (_childrenByParentId.TryGetValue(product.Id, out var children))
        {
            foreach (var child in children)
            {
                if (!await PlanProductHierarchyAsync(child))
                {
                    _logger.LogError($"Не удалось запланировать дочерний продукт {child.Id} для родителя {product.Id}");
                    return false;
                }
            }
        }

        // Планируем сам продукт
        var result = await PlanProductWithBrigadeSelectionAsync(product, product.FactoryId);
        if (result)
            _logger.LogWarning($"Продукт {product.Id} успешно запланирован");
        else
            _logger.LogError($"Не удалось запланировать продукт {product.Id}");

        return result;
    }

    private async Task UpdateParentStageConstraintAsync(ProductEntity childProduct, DateTime childWorkingPeriodEndTimeUtc)
    {
        if (!childProduct.ParentProductId.HasValue || !childProduct.SubProductStageId.HasValue)
            return;

        var parentProductId = childProduct.ParentProductId.Value;
        var stageId = childProduct.SubProductStageId.Value;

        try
        {
            var stage = await _stageRepository.GetById(stageId);
            if (stage == null)
            {
                _logger.LogError($"Stage с Id {stageId} не найдена для родительского продукта {parentProductId}");
                return;
            }

            if (stage.ProductId != parentProductId)
            {
                _logger.LogError($"Stage {stageId} принадлежит продукту {stage.ProductId}, а не ожидаемому родителю {parentProductId}");
                return;
            }

            // Обновляем словарь ограничений – храним МАКСИМАЛЬНУЮ дату (UTC)
            if (_productStageConstraintDates.TryGetValue(stageId, out var existingDate))
            {
                if (childWorkingPeriodEndTimeUtc <= existingDate)
                    return;
                _productStageConstraintDates[stageId] = childWorkingPeriodEndTimeUtc;
                _logger.LogWarning($"Обновлена дата ограничения для Stage {stageId}: {childWorkingPeriodEndTimeUtc:dd.MM.yyyy HH:mm} UTC");
            }
            else
            {
                _productStageConstraintDates[stageId] = childWorkingPeriodEndTimeUtc;
                _logger.LogWarning($"Установлена дата ограничения для Stage {stageId}: {childWorkingPeriodEndTimeUtc:dd.MM.yyyy HH:mm} UTC");
            }

            // Обновляем сущность Stage в БД (дата в UTC)
            stage.Date = _productStageConstraintDates[stageId];
            stage.UpdateTime = DateTime.UtcNow;

            await _stageRepository.Update(stage);
            _logger.LogWarning($"Обновлена Stage {stageId} родительского продукта {parentProductId}: Date = {stage.Date:dd.MM.yyyy HH:mm} UTC");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при обновлении Stage родительского продукта {parentProductId}");
        }
    }

    // ========== Вспомогательные методы ==========
    private async Task<List<DateTime>> GetStageDatesWithMaterialStageNamePAsync(Guid productId)
    {
        var stageDates = new List<DateTime>();

        try
        {
            _logger.LogWarning($"Поиск Stage с MaterialStage.Name = 'П' для продукта {productId}");

            var product = await _productRepository.GetById(productId);
            if (product == null)
            {
                _logger.LogError($"Продукт {productId} не найден");
                return stageDates;
            }

            var productStages = await _stageRepository.GetByProductId(productId);
            if (productStages == null || !productStages.Any())
            {
                _logger.LogWarning($"Для продукта {productId} не найдены Stage");
                return stageDates;
            }

            _logger.LogWarning($"Найдено {productStages.Count} Stage для продукта {productId}");

            foreach (var stage in productStages)
            {
                if (stage == null) continue;

                try
                {
                    var stageSample = await _productSubTypeStageSampleRepository.GetById(stage.ProductSubTypeStageSampleId);
                    if (stageSample == null) continue;

                    var materialStage = await _materialStageRepository.GetById(stageSample.MaterialStageId);
                    if (materialStage == null) continue;

                    if (materialStage.StageName?.Trim() == "П")
                    {
                        stageDates.Add(stage.Date);
                        _logger.LogWarning($"Найден Stage с MaterialStage.Name = 'П': {stage.Id}, дата: {stage.Date:dd.MM.yyyy HH:mm} UTC");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Ошибка при обработке Stage {stage?.Id}");
                }
            }

            _logger.LogWarning($"Для продукта {productId} найдено {stageDates.Count} Stage с MaterialStage.Name = 'П'");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при поиске Stage с MaterialStage.Name = 'П' для продукта {productId}");
        }

        return stageDates;
    }

    private async Task<DateTime> GetProductPlanningStartTimeAsync(Guid productId)
    {
        try
        {
            _logger.LogWarning($"Определение времени старта планирования для продукта {productId}");

            var stageDates = await GetStageDatesWithMaterialStageNamePAsync(productId);

            DateTime productPlanningStartTimeUtc;

            if (stageDates.Any())
            {
                var maxStageDate = stageDates.Max(); // уже UTC
                productPlanningStartTimeUtc = maxStageDate;
                _logger.LogWarning($"Продукт {productId}: используем дату Stage с 'П' = {maxStageDate:dd.MM.yyyy HH:mm} UTC");
            }
            else
            {
                productPlanningStartTimeUtc = _planningStartTimeUtc;
                _logger.LogWarning($"Продукт {productId}: нет Stage с 'П', используем общее время старта планирования = {_planningStartTimeUtc:dd.MM.yyyy HH:mm} UTC");
            }

            // Корректируем на рабочие часы
            productPlanningStartTimeUtc = AdjustToWorkHours(productPlanningStartTimeUtc);

            _logger.LogWarning($"Окончательное время старта планирования для продукта {productId}: {productPlanningStartTimeUtc:dd.MM.yyyy HH:mm} UTC");

            return productPlanningStartTimeUtc;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при определении времени старта планирования для продукта {productId}");
            return AdjustToWorkHours(_planningStartTimeUtc);
        }
    }

    private async Task<List<DateTime>> GetStageDatesForWorkingPeriodSampleAsync(
        Guid productId,
        Guid productSubTypeWorkingPeriodSampleId)
    {
        var stageDates = new List<DateTime>();

        try
        {
            _logger.LogWarning($"Получение дат Stage для продукта {productId} и этапа рабочего периода {productSubTypeWorkingPeriodSampleId}");

            var product = await _productRepository.GetById(productId);
            if (product == null)
            {
                _logger.LogError($"Продукт {productId} не найден");
                return stageDates;
            }

            // ---------- БЛОК 1: Даты из связи с MaterialStage (старая логика) ----------
            var relations = await _productSubTypeGroupMaterialRelationRepository
                .GetByProductSubTypeWorkingPeriodSampleId(productSubTypeWorkingPeriodSampleId);

            if (relations != null && relations.Any())
            {
                _logger.LogWarning($"Для этапа рабочего периода {productSubTypeWorkingPeriodSampleId} найдено {relations.Count} связей с GroupMaterial");

                foreach (var relation in relations)
                {
                    var materialStages = await _materialStageRepository.GetByGroupMaterialId(relation.GroupMaterialId);
                    if (materialStages != null && materialStages.Any())
                    {
                        foreach (var materialStage in materialStages)
                        {
                            var stageSamples = await _productSubTypeStageSampleRepository.GetByMaterialStageId(materialStage.Id);
                            var filteredSamples = stageSamples?
                                .Where(s => s?.ProductSubTypeId == product.ProductSubTypeId)
                                .ToList();

                            if (filteredSamples != null && filteredSamples.Any())
                            {
                                foreach (var stageSample in filteredSamples)
                                {
                                    var stages = await _stageRepository.GetByProductSubTypeStageSampleId(stageSample.Id);
                                    var productStages = stages?
                                        .Where(s => s?.ProductId == productId)
                                        .ToList();

                                    if (productStages != null && productStages.Any())
                                    {
                                        foreach (var stage in productStages)
                                        {
                                            if (stage != null && stage.Date != DateTime.MinValue)
                                            {
                                                stageDates.Add(stage.Date);
                                                _logger.LogWarning($"Найдена дата Stage: {stage.Date:dd.MM.yyyy HH:mm} UTC (StageId: {stage.Id}) для этапа {productSubTypeWorkingPeriodSampleId}");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                _logger.LogWarning($"Для этапа рабочего периода {productSubTypeWorkingPeriodSampleId} не найдены связи с GroupMaterial");
            }

            // ---------- БЛОК 2: Даты ограничений от дочерних продуктов ----------
            var productStagesForConstraint = await _stageRepository.GetByProductId(productId);
            var currentStage = productStagesForConstraint?
                .FirstOrDefault(s => s.ProductSubTypeStageSampleId == productSubTypeWorkingPeriodSampleId);

            if (currentStage != null)
            {
                if (_productStageConstraintDates.TryGetValue(currentStage.Id, out var constraintDateUtc))
                {
                    stageDates.Add(constraintDateUtc);
                    _logger.LogWarning($"Добавлена дата ограничения от подпродукта: {constraintDateUtc:dd.MM.yyyy HH:mm} UTC (StageId: {currentStage.Id})");
                }
            }
            else
            {
                _logger.LogWarning($"Не найдена Stage у продукта {productId} с ProductSubTypeStageSampleId = {productSubTypeWorkingPeriodSampleId} – ограничения от подпродуктов не будут учтены для этого этапа");
            }

            _logger.LogWarning($"Для продукта {productId} и этапа {productSubTypeWorkingPeriodSampleId} найдено {stageDates.Count} дат Stage");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при получении дат Stage для этапа рабочего периода {productSubTypeWorkingPeriodSampleId}");
        }

        return stageDates;
    }

    // ========== Работа с бригадами и их занятостью ==========
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
            var brigadeRelations = await _workingPeriodStageBrigadeRelationRepository.GetByBrigadeId(brigadeId);
            if (brigadeRelations != null && brigadeRelations.Any())
            {
                var stageIds = brigadeRelations.Select(r => r.WorkingPeriodStageId).ToList();
                var stages = _allWorkingPeriodStages.Where(s => stageIds.Contains(s.Id)).ToList();

                foreach (var stage in stages)
                {
                    var stageSample = await _productSubTypeWorkingPeriodSampleRepository.GetById(stage.ProductSubTypeWorkingPeriodSampleId);
                    if (stageSample != null)
                    {
                        timeSlots.Add(new BrigadeTimeSlot
                        {
                            Start = stage.DateFrom, // уже UTC
                            End = stage.DateTo,     // уже UTC
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
        if (_factoryBrigades.TryGetValue(factoryId, out var factoryBrigades))
        {
            foreach (var brigade in factoryBrigades)
            {
                _brigadeNextAvailableTime[brigade.Id] = _planningStartTimeUtc;
                _logger.LogWarning($"Бригада {brigade.Id} инициализирована, доступна с {_brigadeNextAvailableTime[brigade.Id]:dd.MM.yyyy HH:mm} UTC");
            }
        }
    }

    // ========== Основная логика планирования ==========
    private async Task<bool> PlanProductWithBrigadeSelectionAsync(ProductEntity product, Guid factoryId)
    {
        try
        {
            _logger.LogWarning($"Начало планирования продукта {product.Id} с выбором бригады");

            if (_allWorkingPeriods.Any(wp => wp.ProductId == product.Id))
            {
                _logger.LogWarning($"У продукта {product.Id} уже есть рабочий период");
                return false;
            }

            var productStages = await _productSubTypeWorkingPeriodSampleRepository.GetByProductSubTypeId(product.ProductSubTypeId);
            if (productStages == null || !productStages.Any())
            {
                _logger.LogWarning($"Для продукта {product.Id} не найдены этапы сборки");
                return false;
            }

            _logger.LogWarning($"Найдено {productStages.Count} этапов");

            var stageGraph = await BuildStageDependencyGraphAsync(productStages);
            _logger.LogWarning($"Построен граф из {stageGraph.Count} узлов");

            var executionPlan = await GetStageExecutionPlanAsync(stageGraph);
            _logger.LogWarning($"Создан план из {executionPlan.Count} групп выполнения");

            var rootStages = stageGraph.Where(n => !n.Parents.Any()).ToList();
            _logger.LogWarning($"Корневых этапов (без родителей): {rootStages.Count}");

            if (rootStages.Count == 0)
            {
                _logger.LogError("Нет корневых этапов! Возможно, цикличные зависимости.");
                return false;
            }

            var stageTypes = await GetStageTypesForGraphAsync(stageGraph);
            var assemblyStageTypeId = _stageTypeIds.GetValueOrDefault("Сборка");
            var hasAssemblyStages = stageTypes.Any(kv => kv.Value == assemblyStageTypeId);

            if (hasAssemblyStages)
            {
                return await PlanProductWithAssemblyStagesAsync(product, factoryId, stageGraph, executionPlan, stageTypes);
            }
            else
            {
                return await PlanProductWithoutAssemblyStagesAsync(product, factoryId, stageGraph, executionPlan, stageTypes);
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
            var assemblyBrigades = await _brigadeRepository.GetByStageTypeId(assemblyStageTypeId);
            var factoryAssemblyBrigades = assemblyBrigades?
                .Where(b => b.FactoryId == factoryId)
                .ToList();

            if (factoryAssemblyBrigades == null || !factoryAssemblyBrigades.Any())
            {
                _logger.LogError($"На фабрике {factoryId} нет бригад типа 'Сборка'");
                return false;
            }

            DateTime productPlanningStartTimeUtc = await GetProductPlanningStartTimeAsync(product.Id);
            _logger.LogWarning($"Продукт {product.Id} (с этапами сборки) начинает планирование с {productPlanningStartTimeUtc:dd.MM.yyyy HH:mm} UTC");

            var bestAssemblyBrigade = await SelectBestAssemblyBrigadeAsync(
                product, factoryAssemblyBrigades, stageGraph, executionPlan, stageTypes, factoryId);

            if (!bestAssemblyBrigade.HasValue)
            {
                _logger.LogError($"Не удалось выбрать бригаду сборки для продукта {product.Id}");
                return false;
            }

            var (assemblyBrigadeId, stageToBrigadeMapping, _, _) = bestAssemblyBrigade.Value;
            _productAssemblyBrigadeMapping[product.Id] = assemblyBrigadeId;

            var plannedStages = await PlanProductWithBrigadeMappingAsync(
                product, stageGraph, executionPlan, stageToBrigadeMapping, productPlanningStartTimeUtc, factoryId);

            if (plannedStages == null || !plannedStages.Any())
            {
                _logger.LogWarning($"Не удалось запланировать этапы для продукта {product.Id}");
                return false;
            }

            await CreateWorkingPeriodAsync(product, plannedStages);
            _logger.LogInformation($"Создан рабочий период для продукта {product.Id}");

            if (plannedStages.Any())
                await UpdateParentStageConstraintAsync(product, plannedStages.Last().EndTime);

            _plannedProductIdsInThisRun.Add(product.Id);
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

            var stageToBrigadeMapping = await CreateStageToBrigadeMappingAsync(
                product, stageGraph, stageTypes, null, factoryId);

            if (!stageToBrigadeMapping.Any())
            {
                _logger.LogError($"Не удалось создать маппинг бригад для продукта {product.Id}");
                return false;
            }

            DateTime productPlanningStartTimeUtc = await GetProductPlanningStartTimeAsync(product.Id);
            _logger.LogWarning($"Продукт {product.Id} начинает планирование с {productPlanningStartTimeUtc:dd.MM.yyyy HH:mm} UTC");

            var plannedStages = await PlanProductWithBrigadeMappingAsync(
                product, stageGraph, executionPlan, stageToBrigadeMapping, productPlanningStartTimeUtc, factoryId);

            if (plannedStages == null || !plannedStages.Any())
            {
                _logger.LogWarning($"Не удалось запланировать этапы для продукта {product.Id}");
                return false;
            }

            await CreateWorkingPeriodAsync(product, plannedStages);
            _logger.LogInformation($"Создан рабочий период для продукта {product.Id}");

            if (plannedStages.Any())
                await UpdateParentStageConstraintAsync(product, plannedStages.Last().EndTime);

            _plannedProductIdsInThisRun.Add(product.Id);
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
        DateTime productPlanningStartTimeUtc = await GetProductPlanningStartTimeAsync(product.Id);

        foreach (var brigade in assemblyBrigades)
        {
            try
            {
                var stageToBrigadeMapping = await CreateStageToBrigadeMappingAsync(
                    product, stageGraph, stageTypes, brigade.Id, factoryId);

                if (!stageToBrigadeMapping.Any())
                {
                    _logger.LogDebug($"Не удалось создать маппинг для бригады {brigade.Id}");
                    continue;
                }

                var timeEstimation = await EstimateProductTimeWithMappingAsync(
                    product, stageGraph, executionPlan, stageToBrigadeMapping, factoryId, productPlanningStartTimeUtc);

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
            return null;

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

        var factoryBrigades = await _brigadeRepository.GetByFactoryId(factoryId);
        if (factoryBrigades == null || !factoryBrigades.Any())
        {
            _logger.LogError($"На фабрике {factoryId} нет бригад");
            return stageToBrigadeMapping;
        }

        var brigadesByType = factoryBrigades
            .GroupBy(b => b.StageTypeId)
            .ToDictionary(g => g.Key, g => g.ToList());

        var brigadeForStageType = new Dictionary<Guid, Guid>();

        foreach (var node in stageGraph)
        {
            var stageTypeId = stageTypes[node.Id];
            Guid selectedBrigadeId;

            if (stageTypeId == assemblyStageTypeId && assemblyBrigadeId.HasValue)
            {
                selectedBrigadeId = assemblyBrigadeId.Value;
            }
            else
            {
                if (!brigadeForStageType.ContainsKey(stageTypeId))
                {
                    if (!brigadesByType.ContainsKey(stageTypeId))
                    {
                        _logger.LogWarning($"Нет бригад типа {stageTypeId} на фабрике {factoryId}");
                        if (assemblyBrigadeId.HasValue)
                            selectedBrigadeId = assemblyBrigadeId.Value;
                        else
                        {
                            _logger.LogError($"Не удалось найти бригаду для этапа {node.Id}");
                            continue;
                        }
                    }
                    else
                    {
                        var suitableBrigades = brigadesByType[stageTypeId]
                            .Where(b => b.CountEmployee >= node.Stage.StandartEmployee)
                            .ToList();

                        if (!suitableBrigades.Any())
                        {
                            _logger.LogWarning($"Нет подходящих бригад для этапа {node.Id} (требуется {node.Stage.StandartEmployee} сотрудников)");
                            if (assemblyBrigadeId.HasValue)
                                selectedBrigadeId = assemblyBrigadeId.Value;
                            else
                                continue;
                        }
                        else
                        {
                            var bestBrigade = suitableBrigades
                                .OrderBy(b =>
                                {
                                    var availableTime = _brigadeNextAvailableTime.ContainsKey(b.Id)
                                        ? _brigadeNextAvailableTime[b.Id]
                                        : _planningStartTimeUtc;
                                    return availableTime;
                                })
                                .ThenBy(b => b.CountEmployee)
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
        Guid factoryId,
        DateTime productPlanningStartTimeUtc)
    {
        try
        {
            _logger.LogWarning($"Оценка времени для продукта {product.Id}, старт с {productPlanningStartTimeUtc:dd.MM.yyyy HH:mm} UTC");

            var simulatedBrigadeTimes = new Dictionary<Guid, DateTime>();
            var simulatedTimeSlots = new Dictionary<Guid, List<BrigadeTimeSlot>>();

            foreach (var brigadeId in stageToBrigadeMapping.Values.Distinct())
            {
                simulatedBrigadeTimes[brigadeId] = productPlanningStartTimeUtc;
                simulatedTimeSlots[brigadeId] = _brigadeTimeSlots.ContainsKey(brigadeId)
                    ? new List<BrigadeTimeSlot>(_brigadeTimeSlots[brigadeId])
                    : new List<BrigadeTimeSlot>();
            }

            var stageCompletionTimes = new Dictionary<Guid, DateTime>();

            foreach (var group in executionPlan.OrderBy(g => g.Level))
            {
                foreach (var stage in group.Stages)
                {
                    var node = stageGraph.FirstOrDefault(n => n.Id == stage.Id);
                    if (node == null) continue;

                    var brigadeId = stageToBrigadeMapping[node.Id];
                    var currentBrigadeTime = simulatedBrigadeTimes[brigadeId];

                    bool hasDependencies = node.Parents.Any();
                    if (hasDependencies)
                    {
                        var parentEndTimes = node.Parents
                            .Where(parentId => stageCompletionTimes.ContainsKey(parentId))
                            .Select(parentId => stageCompletionTimes[parentId])
                            .ToList();

                        if (parentEndTimes.Any())
                        {
                            var maxParentEndTime = parentEndTimes.Max();
                            if (maxParentEndTime > currentBrigadeTime)
                                currentBrigadeTime = maxParentEndTime;
                        }
                    }

                    var stageDates = await GetStageDatesForWorkingPeriodSampleAsync(product.Id, node.Id);
                    if (stageDates.Any())
                    {
                        var maxStageDate = stageDates.Max();
                        if (maxStageDate > currentBrigadeTime)
                            currentBrigadeTime = maxStageDate;
                    }

                    var duration = ParseStandartTimeToTimeSpan(stage.StandartTime);
                    var plannedTime = await FindAvailableTimeSlotForBrigadeAsync(
                        currentBrigadeTime,
                        duration,
                        brigadeId,
                        stage.StandartEmployee,
                        simulatedTimeSlots[brigadeId],
                        factoryId,
                        hasDependencies);

                    if (!plannedTime.HasValue)
                    {
                        _logger.LogWarning($"Не удалось найти время для этапа {stage.Id} в симуляции");
                        return null;
                    }

                    var stageEndTime = plannedTime.Value.Add(duration);
                    stageCompletionTimes[stage.Id] = stageEndTime;
                    simulatedBrigadeTimes[brigadeId] = stageEndTime;

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
                _logger.LogWarning($"Не удалось оценить время для продукта {product.Id}");
                return null;
            }

            var startTime = stageCompletionTimes.Values.Min();
            var endTime = stageCompletionTimes.Values.Max();

            _logger.LogWarning($"Оценка времени для продукта {product.Id}: {startTime:dd.MM.yyyy HH:mm} UTC - {endTime:HH:mm} UTC");
            return (startTime, endTime);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при оценке времени с маппингом для продукта {product.Id}");
            return null;
        }
    }

    private async Task<List<PlannedStage>> PlanProductWithBrigadeMappingAsync(
        ProductEntity product,
        List<StageNode> stageGraph,
        List<StageExecutionGroup> executionPlan,
        Dictionary<Guid, Guid> stageToBrigadeMapping,
        DateTime planningStartTimeUtc,
        Guid factoryId)
    {
        try
        {
            _logger.LogWarning($"Планирование продукта {product.Id} с распределением по бригадам");

            var plannedStages = new List<PlannedStage>();
            var stageCompletionTimes = new Dictionary<Guid, DateTime>();

            foreach (var group in executionPlan.OrderBy(g => g.Level))
            {
                _logger.LogWarning($"Планирование группы уровня {group.Level} с {group.Stages.Count} этапами");

                var groupStages = new List<(StageNode Node, Guid BrigadeId, int RequiredEmployees, TimeSpan Duration, bool HasDependencies)>();

                foreach (var stage in group.Stages)
                {
                    var node = stageGraph.FirstOrDefault(n => n.Id == stage.Id);
                    if (node == null) continue;

                    var brigadeId = stageToBrigadeMapping[node.Id];
                    var duration = ParseStandartTimeToTimeSpan(stage.StandartTime);
                    var hasDependencies = node.Parents.Any();

                    groupStages.Add((node, brigadeId, stage.StandartEmployee, duration, hasDependencies));
                }

                var sortedGroupStages = groupStages
                    .OrderBy(s => s.HasDependencies ? 1 : 0)
                    .ThenBy(s => s.Duration)
                    .ToList();

                foreach (var (node, brigadeId, requiredEmployees, duration, hasDependencies) in sortedGroupStages)
                {
                    DateTime searchStartTimeUtc = planningStartTimeUtc;

                    if (hasDependencies)
                    {
                        var parentEndTimes = node.Parents
                            .Where(parentId => stageCompletionTimes.ContainsKey(parentId))
                            .Select(parentId => stageCompletionTimes[parentId])
                            .ToList();

                        if (parentEndTimes.Any())
                        {
                            var maxParentEndTime = parentEndTimes.Max();
                            if (maxParentEndTime > searchStartTimeUtc)
                                searchStartTimeUtc = maxParentEndTime;
                        }
                    }

                    var stageDates = await GetStageDatesForWorkingPeriodSampleAsync(product.Id, node.Id);
                    if (stageDates.Any())
                    {
                        var maxStageDate = stageDates.Max();
                        if (maxStageDate > searchStartTimeUtc)
                        {
                            searchStartTimeUtc = maxStageDate;
                            _logger.LogWarning($"Этап {node.Id} зависит от Stage с датой {maxStageDate:dd.MM.yyyy HH:mm} UTC");
                        }
                    }

                    searchStartTimeUtc = AdjustToWorkHours(searchStartTimeUtc);

                    _logger.LogWarning($"Этап {node.Id}: поиск с {searchStartTimeUtc:HH:mm} UTC, " +
                                       $"длительность {duration.TotalMinutes} мин, " +
                                       $"зависимости от родителей: {hasDependencies}, " +
                                       $"зависимости от Stage: {stageDates.Count}");

                    var plannedTime = await FindAvailableTimeSlotForBrigadeAsync(
                        searchStartTimeUtc,
                        duration,
                        brigadeId,
                        requiredEmployees,
                        _brigadeTimeSlots.GetValueOrDefault(brigadeId, new List<BrigadeTimeSlot>()),
                        factoryId,
                        hasDependencies);

                    if (!plannedTime.HasValue)
                    {
                        _logger.LogError($"Не удалось найти время для этапа {node.Id} на бригаде {brigadeId}");
                        return new List<PlannedStage>();
                    }

                    var stageEndTime = plannedTime.Value.Add(duration);
                    var stageTypeId = await GetStageTypeIdAsync(node.Id);

                    var plannedStage = new PlannedStage
                    {
                        ProductId = product.Id,
                        StageSampleId = node.Id,
                        BrigadeId = brigadeId,
                        StartTime = plannedTime.Value,
                        EndTime = stageEndTime,
                        StageTypeId = stageTypeId,
                        RequiredEmployees = requiredEmployees
                    };

                    plannedStages.Add(plannedStage);
                    stageCompletionTimes[node.Id] = stageEndTime;

                    if (!_brigadeTimeSlots.ContainsKey(brigadeId))
                        _brigadeTimeSlots[brigadeId] = new List<BrigadeTimeSlot>();

                    _brigadeTimeSlots[brigadeId].Add(new BrigadeTimeSlot
                    {
                        Start = plannedStage.StartTime,
                        End = plannedStage.EndTime,
                        ProductId = product.Id,
                        StageId = node.Id,
                        BrigadeId = brigadeId,
                        RequiredEmployees = requiredEmployees
                    });

                    _logger.LogWarning($"Запланирован этап {node.Id}: " +
                        $"{plannedStage.StartTime:dd.MM.yyyy HH:mm} UTC - {plannedStage.EndTime:HH:mm} UTC " +
                        $"на бригаде {brigadeId}");
                }
            }

            return plannedStages;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при планировании продукта {product.Id} с маппингом бригад");
            return new List<PlannedStage>();
        }
    }

    // ========== Поиск временного слота для бригады (полностью в UTC) ==========
    private async Task<DateTime?> FindAvailableTimeSlotForBrigadeAsync(
        DateTime searchStartTimeUtc,
        TimeSpan duration,
        Guid brigadeId,
        int requiredEmployees,
        List<BrigadeTimeSlot> brigadeTimeSlotsUtc,
        Guid factoryId,
        bool hasDependencies)
    {
        try
        {
            _logger.LogWarning($"Поиск слота для бригады {brigadeId}: " +
                             $"поиск с {searchStartTimeUtc:dd.MM.yyyy HH:mm} UTC, " +
                             $"длительность={duration.TotalMinutes} мин, " +
                             $"сотрудников={requiredEmployees}, зависимости={hasDependencies}");

            var brigade = await _brigadeRepository.GetById(brigadeId);
            if (brigade == null)
            {
                _logger.LogError($"Бригада {brigadeId} не найдена");
                return null;
            }

            if (requiredEmployees > brigade.CountEmployee)
            {
                _logger.LogError($"Бригада {brigadeId} имеет {brigade.CountEmployee} сотрудников, требуется {requiredEmployees}");
                return null;
            }

            DateTime currentSearchTimeUtc = AdjustToWorkHours(searchStartTimeUtc);
            int maxDaysToSearch = 30;
            int daysSearched = 0;

            while (daysSearched < maxDaysToSearch)
            {
                // Пропускаем выходные (по UTC, т.к. фабрики в одном поясе и мы работаем в UTC)
                if (currentSearchTimeUtc.DayOfWeek == DayOfWeek.Saturday ||
                    currentSearchTimeUtc.DayOfWeek == DayOfWeek.Sunday)
                {
                    currentSearchTimeUtc = currentSearchTimeUtc.Date.AddDays(1).Add(WORKDAY_START);
                    daysSearched++;
                    _logger.LogWarning($"Пропускаем выходной, переходим к {currentSearchTimeUtc:dd.MM.yyyy HH:mm} UTC");
                    continue;
                }

                DateTime workDayStartUtc = currentSearchTimeUtc.Date.Add(WORKDAY_START);
                DateTime workDayEndUtc = currentSearchTimeUtc.Date.Add(WORKDAY_END);

                var dayBusySlots = brigadeTimeSlotsUtc
                    .Where(s => s.BrigadeId == brigadeId &&
                                s.Start.Date == currentSearchTimeUtc.Date)
                    .OrderBy(s => s.Start)
                    .ToList();

                _logger.LogWarning($"День {currentSearchTimeUtc.Date:yyyy-MM-dd} UTC: {dayBusySlots.Count} занятых слотов");

                if (!dayBusySlots.Any())
                {
                    DateTime candidateStartUtc = currentSearchTimeUtc > workDayStartUtc
                        ? currentSearchTimeUtc
                        : workDayStartUtc;

                    if (candidateStartUtc.Add(duration) <= workDayEndUtc)
                    {
                        _logger.LogWarning($"Найден свободный день: {candidateStartUtc:HH:mm} UTC - {candidateStartUtc.Add(duration):HH:mm} UTC");
                        return candidateStartUtc;
                    }
                }
                else
                {
                    var timePoints = new List<DateTime> { workDayStartUtc };
                    foreach (var slot in dayBusySlots)
                    {
                        timePoints.Add(slot.Start);
                        timePoints.Add(slot.End);
                    }
                    timePoints.Add(workDayEndUtc);
                    timePoints = timePoints.OrderBy(t => t).Distinct().ToList();

                    for (int i = 0; i < timePoints.Count - 1; i++)
                    {
                        DateTime intervalStartUtc = timePoints[i];
                        DateTime intervalEndUtc = timePoints[i + 1];

                        if (intervalEndUtc - intervalStartUtc <= TimeSpan.Zero) continue;

                        DateTime actualStartUtc = intervalStartUtc;

                        if (hasDependencies)
                        {
                            if (intervalEndUtc <= searchStartTimeUtc) continue;
                            if (intervalStartUtc < searchStartTimeUtc)
                                actualStartUtc = searchStartTimeUtc;
                        }
                        else
                        {
                            if (intervalStartUtc < currentSearchTimeUtc)
                            {
                                if (intervalEndUtc <= currentSearchTimeUtc) continue;
                                actualStartUtc = currentSearchTimeUtc;
                            }
                        }

                        if (actualStartUtc + duration <= intervalEndUtc)
                        {
                            bool hasEnoughEmployees = await CheckEmployeesAvailabilityInIntervalAsync(
                                brigadeId, actualStartUtc, duration, requiredEmployees, dayBusySlots);

                            if (hasEnoughEmployees)
                            {
                                _logger.LogWarning($"Найден подходящий промежуток: {actualStartUtc:HH:mm} UTC - {actualStartUtc.Add(duration):HH:mm} UTC");
                                return actualStartUtc;
                            }
                        }
                    }
                }

                daysSearched++;
                currentSearchTimeUtc = currentSearchTimeUtc.Date.AddDays(1).Add(WORKDAY_START);
                _logger.LogWarning($"Переходим к следующему дню: {currentSearchTimeUtc:dd.MM.yyyy HH:mm} UTC");
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

    private async Task<bool> CheckEmployeesAvailabilityInIntervalAsync(
        Guid brigadeId,
        DateTime intervalStartUtc,
        TimeSpan duration,
        int requiredEmployees,
        List<BrigadeTimeSlot> dayBusySlotsUtc)
    {
        try
        {
            var brigade = await _brigadeRepository.GetById(brigadeId);
            if (brigade == null) return false;

            var totalEmployees = brigade.CountEmployee;
            DateTime intervalEndUtc = intervalStartUtc.Add(duration);

            DateTime checkTime = intervalStartUtc;
            int checkPoints = (int)duration.TotalMinutes;

            for (int i = 0; i <= checkPoints; i++)
            {
                int busyEmployees = 0;
                foreach (var slot in dayBusySlotsUtc)
                {
                    if (checkTime >= slot.Start && checkTime < slot.End)
                        busyEmployees += slot.RequiredEmployees;
                }

                if (totalEmployees - busyEmployees < requiredEmployees)
                    return false;

                checkTime = checkTime.AddMinutes(1);
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при проверке доступности сотрудников для бригады {brigadeId}");
            return false;
        }
    }

    // ========== Вспомогательные методы для графа и типов ==========
    private async Task<Dictionary<Guid, Guid>> GetStageTypesForGraphAsync(List<StageNode> stageGraph)
    {
        var stageTypes = new Dictionary<Guid, Guid>();
        foreach (var node in stageGraph)
        {
            var stageTypeId = await GetStageTypeIdAsync(node.Id);
            if (stageTypeId.HasValue)
                stageTypes[node.Id] = stageTypeId.Value;
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

    private double CalculateBrigadeTimeScore(DateTime startTimeUtc, DateTime endTimeUtc, BrigadeEntity brigade)
    {
        double score = (endTimeUtc - startTimeUtc).TotalMinutes * -1;
        score += (10.0 / brigade.CountEmployee) * 100;
        score += (DateTime.UtcNow - startTimeUtc).TotalMinutes * 0.1;
        return score;
    }

    private async Task<List<StageNode>> BuildStageDependencyGraphAsync(
        List<ProductSubTypeWorkingPeriodSampleEntity> stages)
    {
        var nodes = new Dictionary<Guid, StageNode>();

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

        foreach (var stage in stages)
        {
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

    private async Task<List<StageExecutionGroup>> GetStageExecutionPlanAsync(List<StageNode> stageGraph)
    {
        var executionPlan = new List<StageExecutionGroup>();
        var remainingNodes = stageGraph.ToList();
        int level = 0;

        while (remainingNodes.Any())
        {
            level++;

            var readyNodes = remainingNodes
                .Where(node => !node.Parents.Any() ||
                               node.Parents.All(parentId =>
                                   !remainingNodes.Any(n => n.Id == parentId)))
                .ToList();

            if (!readyNodes.Any())
                throw new InvalidOperationException("Обнаружен цикл в зависимостях этапов");

            executionPlan.Add(new StageExecutionGroup
            {
                Stages = readyNodes.Select(n => n.Stage).ToList(),
                CanExecuteInParallel = true,
                Level = level
            });

            foreach (var node in readyNodes)
                remainingNodes.RemoveAll(n => n.Id == node.Id);
        }

        return executionPlan;
    }

    private async Task LoadStageTypeIds()
    {
        try
        {
            _stageTypeIds = new Dictionary<string, Guid>();
            var allStageTypes = await _stageTypeRepository.GetAll();
            foreach (var stageType in allStageTypes)
                _stageTypeIds[stageType.Name] = stageType.Id;
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
                return TimeSpan.Zero;
            if (int.TryParse(standartTime, out int minutes))
                return TimeSpan.FromMinutes(minutes);
            if (TimeSpan.TryParse(standartTime, out TimeSpan timeSpan))
                return timeSpan;
            return TimeSpan.Zero;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при преобразовании StandartTime: '{standartTime}'");
            return TimeSpan.Zero;
        }
    }

    private DateTime AdjustToWorkHours(DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
            utcDateTime = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);

        // Если время уже в рабочем интервале UTC
        if (utcDateTime.TimeOfDay >= WORKDAY_START && utcDateTime.TimeOfDay < WORKDAY_END)
            return utcDateTime;

        // Если до начала рабочего дня
        if (utcDateTime.TimeOfDay < WORKDAY_START)
            return utcDateTime.Date.Add(WORKDAY_START);

        // Если после окончания – следующий рабочий день
        var nextDay = utcDateTime.Date.AddDays(1).Add(WORKDAY_START);
        while (nextDay.DayOfWeek == DayOfWeek.Saturday || nextDay.DayOfWeek == DayOfWeek.Sunday)
            nextDay = nextDay.AddDays(1);
        return nextDay;
    }

    private async Task CreateWorkingPeriodAsync(ProductEntity product, List<PlannedStage> plannedStages)
    {
        if (!plannedStages.Any()) return;

        try
        {
            _logger.LogWarning($"Создание рабочего периода для продукта {product.Id} с {plannedStages.Count} этапами");

            var firstStartTimeUtc = plannedStages.First().StartTime;
            var lastEndTimeUtc = plannedStages.Last().EndTime;

            var workingPeriod = new WorkingPeriodEntity
            {
                Id = Guid.NewGuid(),
                Name = $"Период для продукта {product.Number}",
                Status = "В работе",
                ProductId = product.Id,
                DateFrom = firstStartTimeUtc,
                DateTo = lastEndTimeUtc,
                CreateTime = DateTime.UtcNow,
                UpdateTime = DateTime.UtcNow
            };

            _logger.LogWarning($"Создан WorkingPeriod: {workingPeriod.Id}");
            await _workingPeriodRepository.Add(workingPeriod);
            _allWorkingPeriods.Add(workingPeriod);

            foreach (var plannedStage in plannedStages)
            {
                var workingPeriodStage = new WorkingPeriodStageEntity
                {
                    Id = Guid.NewGuid(),
                    WorkingPeriodId = workingPeriod.Id,
                    DateFrom = plannedStage.StartTime,
                    DateTo = plannedStage.EndTime,
                    Status = "В работе",
                    Recycling = null,
                    ProductSubTypeWorkingPeriodSampleId = plannedStage.StageSampleId,
                    CreateTime = DateTime.UtcNow,
                    UpdateTime = DateTime.UtcNow
                };

                _logger.LogWarning($"Создан WorkingPeriodStage: {workingPeriodStage.Id}");
                await _workingPeriodStageRepository.Add(workingPeriodStage);
                _allWorkingPeriodStages.Add(workingPeriodStage);

                var brigadeRelation = new WorkingPeriodStageBrigadeRelationEntity
                {
                    Id = Guid.NewGuid(),
                    WorkingPeriodStageId = workingPeriodStage.Id,
                    BrigadeId = plannedStage.BrigadeId
                };

                _logger.LogWarning($"Создана связь с бригадой: {brigadeRelation.Id}");
                await _workingPeriodStageBrigadeRelationRepository.Add(brigadeRelation);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при создании рабочего периода для продукта {product.Id}");
            throw;
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

    // ========== Вспомогательные классы (без изменений) ==========
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
        public List<ProductSubTypeWorkingPeriodSampleEntity> Stages { get; set; } = new();
        public bool CanExecuteInParallel { get; set; }
        public DateTime? EarliestStartTime { get; set; }
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
        public List<BrigadeTimeSlot> ScheduledSlots { get; set; } = new();
        public double UtilizationRate { get; set; }
    }
}