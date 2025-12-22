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

    public async Task InitializeAsync()
    {
        _products = await _productRepository.GetAll();
        _allWorkingPeriods = await _workingPeriodRepository.GetAll();
        _allWorkingPeriodStages = await _workingPeriodStageRepository.GetAll();

        // Загружаем бригады по фабрикам
        await LoadFactoryBrigades();

        // Загружаем ID типов этапов
        await LoadStageTypeIds();
    }

    public async Task PlanProductionAsync()
    {
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

        // Получаем последовательность этапов
        var stageSequence = await GetStageSequenceAsync(productStages);

        // Получаем занятое время на фабрике
        var busyTimeSlots = await GetBusyTimeSlotsAsync(product.FactoryId);

        // Планируем этапы
        var plannedStages = await PlanStagesAsync(
            product,
            stageSequence,
            busyTimeSlots,
            productStages);

        if (plannedStages.Any())
        {
            // Создаем рабочий период
            await CreateWorkingPeriodAsync(product, plannedStages);
        }
    }

    private async Task<List<ProductSubTypeWorkingPeriodSample>> GetStageSequenceAsync(
        List<ProductSubTypeWorkingPeriodSample> stages)
    {
        var orderedStages = new List<ProductSubTypeWorkingPeriodSample>();
        var stageDict = stages.ToDictionary(s => s.Id);

        // Находим корневые этапы (те, у которых нет родителей)
        var rootStages = new List<ProductSubTypeWorkingPeriodSample>();

        foreach (var stage in stages)
        {
            var parentRelations = await _workingPeriodRelationRepository
                .GetByChildProductSubTypeWorkingPeriodSampleId(stage.Id);

            if (parentRelations == null || !parentRelations.Any())
            {
                rootStages.Add(stage);
            }
        }

        // Рекурсивно строим последовательность
        foreach (var root in rootStages)
        {
            await TraverseStageTreeAsync(root, stageDict, orderedStages);
        }

        return orderedStages;
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

    private async Task<List<PlannedStage>> PlanStagesAsync(
        Product product,
        List<ProductSubTypeWorkingPeriodSample> stageSequence,
        List<TimeSlot> busyTimeSlots,
        List<ProductSubTypeWorkingPeriodSample> allStages)
    {
        var plannedStages = new List<PlannedStage>();
        DateTime currentTime = DateTime.Now;

        // Словарь для отслеживания бригад для этапов "Сборка"
        var assemblyBrigadeMapping = new Dictionary<Guid, Guid>();

        foreach (var stageSample in stageSequence)
        {
            // Получаем тип этапа
            var stageTypeRelation = await _workingPeriodStageTypeRelationRepository
                .GetByProductSubTypeWorkingPeriodSampleId(stageSample.Id);

            var stageTypeId = stageTypeRelation?.FirstOrDefault()?.StageTypeId;
            var stageType = stageTypeId.HasValue ?
                await _stageTypeRepository.GetById(stageTypeId.Value) : null;

            // Получаем требуемую бригаду
            var brigadeRelations = await _workingPeriodStageBrigadeRelationRepository
                .GetByWorkingPeriodStageId(stageSample.Id); // Note: здесь нужен репозиторий по sampleId

            // Для этапов "Сборка" проверяем, есть ли уже назначенная бригада
            Guid? brigadeId = null;
            if (stageType?.Name == "Сборка" && assemblyBrigadeMapping.ContainsKey(product.Id))
            {
                brigadeId = assemblyBrigadeMapping[product.Id];
            }
            else if (brigadeRelations != null && brigadeRelations.Any())
            {
                brigadeId = brigadeRelations.First().BrigadeId;

                // Запоминаем бригаду для этапов сборки этого продукта
                if (stageType?.Name == "Сборка")
                {
                    assemblyBrigadeMapping[product.Id] = brigadeId.Value;
                }
            }

            // Проверяем доступность бригады
            if (brigadeId.HasValue)
            {
                var isBrigadeAvailable = await CheckBrigadeAvailabilityAsync(
                    brigadeId.Value,
                    currentTime,
                    TimeSpan.Parse(stageSample.StandartTime), // предполагаем формат "hh:mm:ss"
                    product.FactoryId);

                if (!isBrigadeAvailable)
                {
                    // Ищем другую доступную бригаду
                    brigadeId = await FindAvailableBrigadeAsync(
                        stageSample,
                        currentTime,
                        product.FactoryId);
                }
            }

            if (!brigadeId.HasValue)
            {
                // Не смогли найти бригаду - пропускаем продукт
                return new List<PlannedStage>();
            }

            // Ищем свободное время с учетом бригады
            var plannedTime = await FindAvailableTimeSlotAsync(
                currentTime,
                TimeSpan.Parse(stageSample.StandartTime),
                busyTimeSlots,
                brigadeId.Value,
                product.FactoryId);

            if (!plannedTime.HasValue)
            {
                // Не смогли найти время - пропускаем продукт
                return new List<PlannedStage>();
            }

            // Создаем запланированный этап
            var plannedStage = new PlannedStage
            {
                ProductId = product.Id,
                StageSampleId = stageSample.Id,
                BrigadeId = brigadeId.Value,
                StartTime = plannedTime.Value,
                EndTime = plannedTime.Value.Add(TimeSpan.Parse(stageSample.StandartTime)),
                StageTypeId = stageTypeId
            };

            plannedStages.Add(plannedStage);

            // Обновляем текущее время и добавляем слот как занятый
            currentTime = plannedStage.EndTime;
            busyTimeSlots.Add(new TimeSlot
            {
                Start = plannedStage.StartTime,
                End = plannedStage.EndTime,
                StageId = stageSample.Id,
                BrigadeId = brigadeId.Value
            });
        }

        return plannedStages;
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
        DateTime maxSearchTime = currentTime.AddDays(30); // Ограничиваем поиск 30 днями

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

    // Вспомогательные классы
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
}