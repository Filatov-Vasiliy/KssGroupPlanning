using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Repositories;
using System.Collections.Specialized;
using System.ComponentModel;

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

    private List<Product>? _products;
    private List<WorkingPeriod> _allWorkingPeriods;
    private List<WorkingPeriodStage> _allWorkingPeriodStages;
    private Dictionary<Guid, List<Brigade>> _factoryBrigades;
    private Dictionary<string, Guid> _stageTypeIds;

    // Константы для рабочего времени
    private readonly TimeSpan WORKDAY_START = new TimeSpan(8, 0, 0);
    private readonly TimeSpan WORKDAY_END = new TimeSpan(20, 0, 0);
    private readonly TimeSpan WORKDAY_DURATION = new TimeSpan(12, 0, 0);

    public CoreService
        (
        IProductRepository productRepository,
        IWorkingPeriodRepository workingPeriodRepository,
        IWorkingPeriodStageRepository workingPeriodStageRepository,
        IProductSubTypeWorkingPeriodSampleRepository productSubTypeWorkingPeriodSampleRepository,
        IFactoryRepository factoryRepository,
        IBrigadeRepository brigadeRepository,
        IWorkingPeriodStageBrigadeRelationRepository workingPeriodStageBrigadeRelationRepository,
        IWorkingPeriodRelationRepository workingPeriodRelationRepository,
        IWorkingPeriodStageTypeRelationRepository workingPeriodStageTypeRelationRepository,
        IStageTypeRepository stageTypeRepository
        )
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
    }

    /*public async Task InitializeAsync()
    {
        _products = await _productRepository.GetAll();
        _allWorkingPeriods = await _workingPeriodRepository.GetAll();
        _allWorkingPeriodStages = await _workingPeriodStageRepository.GetAll();

        // Загружаем бригады по фабрикам
        await LoadFactoryBrigades();

        // Загружаем ID типов этапов
        await LoadStageTypeIds();
    }*/

    public async Task PlanProductionAsync()
    {
        _products = await _productRepository.GetAll();
        _allWorkingPeriods = await _workingPeriodRepository.GetAll();
        _allWorkingPeriodStages = await _workingPeriodStageRepository.GetAll();

        // Загружаем бригады по фабрикам
        await LoadFactoryBrigades();

        // Загружаем ID типов этапов
        await LoadStageTypeIds();


        if (_products == null) return;

        // 1. Сортируем продукты по EndDate
        SortProductsByEndDate();

        // 2. Планируем каждый продукт
        foreach (var product in _products)
        {
            await PlanProductAsync(product);
        }
    }

    private void SortProductsByEndDate()
    {
        _products?.Sort((p1, p2) =>
        {
            if (p1.EndDate == p2.EndDate) return 0;
            if (p1.EndDate == null) return 1;     // null в конец
            if (p2.EndDate == null) return -1;    // не-null в начало
            return p1.EndDate.Value.CompareTo(p2.EndDate.Value);
        });
    }

    private async Task PlanProductAsync(Product product)
    {
        // Проверяем, есть ли уже рабочий период
        var existingWorkingPeriod = _allWorkingPeriods
            .FirstOrDefault(wp => wp.ProductId == product.Id);

        if (existingWorkingPeriod != null) return;

        // Получаем этапы сборки для продукта
        var productStages = await _productSubTypeWorkingPeriodSampleRepository
            .GetByProductSubTypeId(product.ProductSubTypeId);

        if (productStages == null || !productStages.Any()) return;

        // Строим граф зависимостей этапов
        var stageGraph = await BuildStageDependencyGraphAsync(productStages);

        // Получаем план выполнения с учетом зависимостей
        var executionPlan = await GetStageExecutionPlanAsync(stageGraph);

        // Получаем занятое время на фабрике
        var busyTimeSlots = await GetBusyTimeSlotsAsync(product.FactoryId);

        // Планируем этапы с учетом зависимостей и параллельности
        var plannedStages = await PlanStagesWithDependenciesAsync(
            product,
            executionPlan,
            busyTimeSlots,
            stageGraph);

        if (plannedStages.Any())
        {
            // Создаем рабочий период
            await CreateWorkingPeriodAsync(product, plannedStages);
        }
    }

    private async Task<List<StageNode>> BuildStageDependencyGraphAsync(
     List<ProductSubTypeWorkingPeriodSample> stages)
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
            // Получаем зависимости этого этапа (какие этапы должны быть выполнены до него)
            var parentRelations = await _workingPeriodRelationRepository
                .GetByChildProductSubTypeWorkingPeriodSampleId(stage.Id);

            if (parentRelations != null)
            {
                foreach (var relation in parentRelations)
                {
                    if (nodes.ContainsKey(relation.ParentProductSubTypeWorkingPeriodSampleId))
                    {
                        // Этап зависит от родительского
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

        // Пока есть необработанные узлы
        while (remainingNodes.Any())
        {
            // Находим узлы, у которых все зависимости удовлетворены
            var readyNodes = remainingNodes
                .Where(node =>
                    !node.Parents.Any() || // Корневые узлы
                    node.Parents.All(parentId =>
                        !remainingNodes.Any(n => n.Id == parentId)) // Все родители уже обработаны
                )
                .ToList();

            if (!readyNodes.Any())
            {
                // Обнаружен цикл в зависимостях
                throw new InvalidOperationException("Обнаружен цикл в зависимостях этапов");
            }

            // Группа этапов, которые можно выполнять параллельно
            var executionGroup = new StageExecutionGroup
            {
                Stages = readyNodes.Select(n => n.Stage).ToList(),
                CanExecuteInParallel = true // Эти этапы могут выполняться параллельно
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

    private async Task<List<PlannedStage>> PlanStagesWithDependenciesAsync(
        Product product,
        List<StageExecutionGroup> executionPlan,
        List<TimeSlot> busyTimeSlots,
        List<StageNode> stageGraph)
    {
        var plannedStages = new List<PlannedStage>();
        var stageCompletionTimes = new Dictionary<Guid, DateTime>();
        var assemblyBrigadeMapping = new Dictionary<Guid, Guid>();

        foreach (var group in executionPlan)
        {
            var groupStartTime = DateTime.MinValue;

            // Для этапов в группе определяем время начала
            // Если этап зависит от других, его начало - после завершения всех зависимостей
            foreach (var stage in group.Stages)
            {
                var node = stageGraph.First(n => n.Id == stage.Id);
                DateTime stageStartTime = DateTime.Now;

                // Находим максимальное время завершения всех зависимостей
                if (node.Parents.Any())
                {
                    var parentEndTimes = node.Parents
                        .Where(parentId => stageCompletionTimes.ContainsKey(parentId))
                        .Select(parentId => stageCompletionTimes[parentId])
                        .ToList();

                    if (parentEndTimes.Any())
                    {
                        stageStartTime = parentEndTimes.Max();
                    }
                }

                // Получаем тип этапа
                var stageTypeRelation = await _workingPeriodStageTypeRelationRepository
                    .GetByProductSubTypeWorkingPeriodSampleId(stage.Id);
                var stageTypeId = stageTypeRelation?.FirstOrDefault()?.StageTypeId;
                var stageType = stageTypeId.HasValue ?
                    await _stageTypeRepository.GetById(stageTypeId.Value) : null;

                // Получаем требуемую бригаду
                Guid? brigadeId = await GetBrigadeForStageAsync(
                    stage,
                    stageType,
                    product.Id,
                    assemblyBrigadeMapping);

                if (!brigadeId.HasValue)
                {
                    return new List<PlannedStage>(); // Не смогли найти бригаду
                }

                // Для параллельных этапов в группе корректируем время начала
                // чтобы избежать одновременного использования одной бригады
                if (group.CanExecuteInParallel)
                {
                    // Проверяем, не занята ли бригада в это время другими этапами группы
                    var adjustedStartTime = await AdjustStartTimeForParallelStagesAsync(
                        stageStartTime,
                        TimeSpan.Parse(stage.StandartTime),
                        brigadeId.Value,
                        plannedStages.Where(p => group.Stages.Any(g => g.Id == p.StageSampleId)).ToList(),
                        product.FactoryId);

                    stageStartTime = adjustedStartTime;
                }

                // Ищем свободное время с учетом бригады
                var plannedTime = await FindAvailableTimeSlotAsync(
                    stageStartTime,
                    TimeSpan.Parse(stage.StandartTime),
                    busyTimeSlots,
                    brigadeId.Value,
                    product.FactoryId);

                if (!plannedTime.HasValue)
                {
                    return new List<PlannedStage>();
                }

                // Создаем запланированный этап
                var plannedStage = new PlannedStage
                {
                    ProductId = product.Id,
                    StageSampleId = stage.Id,
                    BrigadeId = brigadeId.Value,
                    StartTime = plannedTime.Value,
                    EndTime = plannedTime.Value.Add(TimeSpan.Parse(stage.StandartTime)),
                    StageTypeId = stageTypeId
                };

                plannedStages.Add(plannedStage);
                stageCompletionTimes[stage.Id] = plannedStage.EndTime;

                // Обновляем занятые слоты
                busyTimeSlots.Add(new TimeSlot
                {
                    Start = plannedStage.StartTime,
                    End = plannedStage.EndTime,
                    StageId = stage.Id,
                    BrigadeId = brigadeId.Value
                });

                // Обновляем время начала для следующих этапов группы
                if (plannedStage.StartTime > groupStartTime)
                {
                    groupStartTime = plannedStage.StartTime;
                }
            }
        }

        return plannedStages;
    }

    private async Task<DateTime> AdjustStartTimeForParallelStagesAsync(
    DateTime proposedStartTime,
    TimeSpan duration,
    Guid brigadeId,
    List<PlannedStage> plannedStagesInGroup,
    Guid factoryId)
    {
        DateTime currentTime = proposedStartTime;

        // Проверяем, не занята ли бригада другими этапами этой же группы
        foreach (var plannedStage in plannedStagesInGroup)
        {
            if (plannedStage.BrigadeId == brigadeId)
            {
                // Если этап уже запланирован на эту бригаду, проверяем пересечение
                if (currentTime < plannedStage.EndTime &&
                    currentTime.Add(duration) > plannedStage.StartTime)
                {
                    // Найдено пересечение, сдвигаем время
                    currentTime = plannedStage.EndTime;
                }
            }
        }

        return currentTime;
    }

    private async Task<Guid?> GetBrigadeForStageAsync(
    ProductSubTypeWorkingPeriodSample stage,
    StageType stageType,
    Guid productId,
    Dictionary<Guid, Guid> assemblyBrigadeMapping)
    {
        // Для этапов "Сборка" используем одну и ту же бригаду для всего продукта
        if (stageType?.Name == "Сборка")
        {
            if (assemblyBrigadeMapping.TryGetValue(productId, out var existingBrigadeId))
            {
                return existingBrigadeId;
            }
        }

        // Получаем доступные бригады для этого этапа
        var brigadeRelations = await _workingPeriodStageBrigadeRelationRepository
            .GetByWorkingPeriodStageId(stage.Id);

        if (brigadeRelations == null || !brigadeRelations.Any())
        {
            return null;
        }

        var brigadeId = brigadeRelations.First().BrigadeId;

        // Запоминаем бригаду для этапов сборки
        if (stageType?.Name == "Сборка")
        {
            assemblyBrigadeMapping[productId] = brigadeId;
        }

        return brigadeId;
    }

    private async Task TraverseStageTreeAsync(
        ProductSubTypeWorkingPeriodSample currentStage,
        Dictionary<Guid, ProductSubTypeWorkingPeriodSample> stageDict,
        List<ProductSubTypeWorkingPeriodSample> result)
    {
        result.Add(currentStage);

        // Находим дочерние этапы
        var childRelations = await _workingPeriodRelationRepository
            .GetByParentProductSubTypeWorkingPeriodSampleId(currentStage.Id);

        if (childRelations != null && childRelations.Any())
        {
            foreach (var relation in childRelations)
            {
                if (stageDict.TryGetValue(relation.ChildProductSubTypeWorkingPeriodSampleId, out var childStage))
                {
                    await TraverseStageTreeAsync(childStage, stageDict, result);
                }
            }
        }
    }

    private async Task<List<TimeSlot>> GetBusyTimeSlotsAsync(Guid factoryId)
    {
        var busySlots = new List<TimeSlot>();

        // Получаем все рабочие периоды на этой фабрике
        var factoryProducts = _products
            .Where(p => p.FactoryId == factoryId)
            .Select(p => p.Id)
            .ToList();

        var factoryWorkingPeriods = _allWorkingPeriods
            .Where(wp => factoryProducts.Contains(wp.ProductId))
            .ToList();

        // Для каждого рабочего периода получаем его этапы
        foreach (var workingPeriod in factoryWorkingPeriods)
        {
            var stages = _allWorkingPeriodStages
                .Where(ws => ws.WorkingPeriodId == workingPeriod.Id)
                .ToList();

            foreach (var stage in stages)
            {
                busySlots.Add(new TimeSlot
                {
                    Start = stage.DateFrom,
                    End = stage.DateTo,
                    StageId = stage.Id
                });
            }
        }

        return busySlots;
    }

   

    private async Task<bool> CheckBrigadeAvailabilityAsync(
        Guid brigadeId,
        DateTime startTime,
        TimeSpan duration,
        Guid factoryId)
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

        // Проверяем пересечения по времени
        foreach (var stage in brigadeStages)
        {
            if (startTime < stage.DateTo && endTime > stage.DateFrom)
            {
                return false; // Найдено пересечение
            }
        }

        return true;
    }

    private async Task<Guid?> FindAvailableBrigadeAsync(
        ProductSubTypeWorkingPeriodSample stageSample,
        DateTime startTime,
        Guid factoryId)
    {
        // Получаем все бригады на фабрике
        var factoryBrigades = await _brigadeRepository.GetByFactoryId(factoryId);

        if (factoryBrigades == null) return null;

        // Получаем требуемые бригады для этого типа этапа
        var requiredBrigadeRelations = await _workingPeriodStageBrigadeRelationRepository
            .GetByWorkingPeriodStageId(stageSample.Id);

        var requiredBrigadeIds = requiredBrigadeRelations?
            .Select(r => r.BrigadeId)
            .ToList() ?? new List<Guid>();

        // Проверяем каждую подходящую бригаду на доступность
        foreach (var brigade in factoryBrigades)
        {
            if (requiredBrigadeIds.Contains(brigade.Id))
            {
                var isAvailable = await CheckBrigadeAvailabilityAsync(
                    brigade.Id,
                    startTime,
                    TimeSpan.Parse(stageSample.StandartTime),
                    factoryId);

                if (isAvailable)
                {
                    return brigade.Id;
                }
            }
        }

        return null;
    }

    private async Task<DateTime?> FindAvailableTimeSlotAsync(
        DateTime startFrom,
        TimeSpan duration,
        List<TimeSlot> busySlots,
        Guid brigadeId,
        Guid factoryId)
    {
        DateTime currentTime = AdjustToWorkHours(startFrom);
        DateTime maxSearchTime = currentTime.AddDays(240); // Ограничиваем поиск 240 днями

        while (currentTime < maxSearchTime)
        {
            DateTime slotEnd = currentTime.Add(duration);

            // Проверяем, что слот в рабочее время
            if (!IsWithinWorkHours(currentTime, slotEnd))
            {
                currentTime = MoveToNextWorkPeriod(currentTime);
                continue;
            }

            // Проверяем пересечение с занятыми слотами
            bool hasConflict = false;
            foreach (var busySlot in busySlots)
            {
                // Проверяем пересечение по времени
                if (currentTime < busySlot.End && slotEnd > busySlot.Start)
                {
                    // Если это та же бригада, точно конфликт
                    if (busySlot.BrigadeId == brigadeId)
                    {
                        hasConflict = true;
                        break;
                    }
                }
            }

            if (!hasConflict)
            {
                // Дополнительно проверяем доступность бригады
                var isBrigadeAvailable = await CheckBrigadeAvailabilityAsync(
                    brigadeId, currentTime, duration, factoryId);

                if (isBrigadeAvailable)
                {
                    return currentTime;
                }
            }

            // Переходим к следующему периоду
            currentTime = currentTime.AddHours(1); // Можно оптимизировать
        }

        return null;
    }

    private DateTime AdjustToWorkHours(DateTime dateTime)
    {
        // Если время вне рабочего дня, перемещаем на начало следующего рабочего дня
        if (dateTime.TimeOfDay < WORKDAY_START || dateTime.TimeOfDay >= WORKDAY_END)
        {
            var nextDay = dateTime.Date.AddDays(1);
            return nextDay.Add(WORKDAY_START);
        }

        return dateTime;
    }

    private bool IsWithinWorkHours(DateTime start, DateTime end)
    {
        // Проверяем, что оба времени в пределах рабочего дня
        // и что этап не переходит через границу рабочего дня
        return start.TimeOfDay >= WORKDAY_START &&
               end.TimeOfDay <= WORKDAY_END &&
               start.Date == end.Date;
    }

    private DateTime MoveToNextWorkPeriod(DateTime dateTime)
    {
        // Перемещаем на начало следующего рабочего дня
        var nextDay = dateTime.Date.AddDays(1);
        return nextDay.Add(WORKDAY_START);
    }

    private async Task CreateWorkingPeriodAsync(Product product, List<PlannedStage> plannedStages)
    {
        if (!plannedStages.Any()) return;

        // Создаем рабочий период
        var workingPeriod = WorkingPeriod.Create(
            Guid.NewGuid(),
            $"Период для продукта {product.Number}",
            "В работе",
            product.Id,
            plannedStages.First().StartTime,
            plannedStages.Last().EndTime,
            DateTime.Now,
            DateTime.Now);

        await _workingPeriodRepository.Add(workingPeriod);
        _allWorkingPeriods.Add(workingPeriod);

        // Создаем этапы рабочего периода
        foreach (var plannedStage in plannedStages)
        {
            var workingPeriodStage = WorkingPeriodStage.Create(
                Guid.NewGuid(),
                workingPeriod.Id,
                plannedStage.StartTime,
                plannedStage.EndTime,
                "В работе",
                null, // recycling
                plannedStage.StageSampleId,
                DateTime.Now,
                DateTime.Now);

            await _workingPeriodStageRepository.Add(workingPeriodStage);
            _allWorkingPeriodStages.Add(workingPeriodStage);

            // Создаем связь с бригадой
            var brigadeRelation = WorkingPeriodStageBrigadeRelation.Create(
                Guid.NewGuid(),
                workingPeriodStage.Id,
                plannedStage.BrigadeId);

            await _workingPeriodStageBrigadeRelationRepository.Add(brigadeRelation);
        }

        // Обновляем даты продукта
        product.StartDate = DateOnly.FromDateTime(plannedStages.First().StartTime);
        product.EndDate = DateOnly.FromDateTime(plannedStages.Last().EndTime);
        await _productRepository.Update(product);
    }

    private async Task LoadFactoryBrigades()
    {
        _factoryBrigades = new Dictionary<Guid, List<Brigade>>();
        var allFactories = await _factoryRepository.GetAll();

        foreach (var factory in allFactories)
        {
            var factoryBrigades = await _brigadeRepository.GetByFactoryId(factory.Id);
            if (factoryBrigades != null)
            {
                _factoryBrigades[factory.Id] = factoryBrigades;
            }
        }
    }

    private async Task LoadStageTypeIds()
    {
        _stageTypeIds = new Dictionary<string, Guid>();
        var allStageTypes = await _stageTypeRepository.GetAll();

        foreach (var stageType in allStageTypes)
        {
            _stageTypeIds[stageType.Name] = stageType.Id;
        }
    }

    private class TimeSlot
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Guid StageId { get; set; }
        public Guid? BrigadeId { get; set; }
    }

    private class PlannedStage
    {
        public Guid ProductId { get; set; }
        public Guid StageSampleId { get; set; }
        public Guid BrigadeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid? StageTypeId { get; set; }
    }

    private class StageNode
    {
        public Guid Id { get; set; }
        public ProductSubTypeWorkingPeriodSample Stage { get; set; }
        public List<Guid> Parents { get; set; } // Этапы, которые должны быть выполнены до этого
        public List<Guid> Children { get; set; } // Этапы, которые зависят от этого
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
        public List<ProductSubTypeWorkingPeriodSample> Stages { get; set; }
        public bool CanExecuteInParallel { get; set; }
        public DateTime? EarliestStartTime { get; set; }
    }
}