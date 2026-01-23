using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Repositories;
using System.Collections.Specialized;
using System.ComponentModel;

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

            // 1. Сортируем продукты по EndDate
            SortProductsByEndDate();
            _logger.LogWarning($"Отсортировано {_products.Count} продуктов");

            // 2. Планируем каждый продукт
            int plannedCount = 0;
            foreach (var product in _products)
            {
                _logger.LogWarning($"Обработка продукта {product.Id} ({product.Number})");
                var result = await PlanProductAsync(product);
                if (result) plannedCount++;
            }

            _logger.LogWarning($"Успешно запланировано {plannedCount} продуктов из {_products.Count}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при планировании производства");
            throw;
        }
    }

    private async Task TestStandartTimeParsing()
    {
        try
        {
            // Берем несколько этапов для тестирования парсинга
            var sampleStages = await _productSubTypeWorkingPeriodSampleRepository.GetAll();

            if (sampleStages != null && sampleStages.Any())
            {
                _logger.LogWarning("Тестирование преобразования StandartTime:");

                foreach (var stage in sampleStages.Take(5))
                {
                    TimeSpan duration = ParseStandartTimeToTimeSpan(stage.StandartTime);
                    _logger.LogWarning($"  Этап {stage.Id}: '{stage.StandartTime}' -> {duration.TotalMinutes} минут ({duration})");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при тестировании преобразования StandartTime");
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

    private async Task<bool> PlanProductAsync(ProductEntity product)
    {
        try
        {
            _logger.LogWarning($"Начало планирования продукта {product.Id}");

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

            // ВАЖНО: Проверяем, что у нас есть корневые этапы
            var rootStages = stageGraph.Where(n => !n.Parents.Any()).ToList();
            _logger.LogWarning($"Корневых этапов (без родителей): {rootStages.Count}");

            if (rootStages.Count == 0)
            {
                _logger.LogError("Нет корневых этапов! Возможно, цикличные зависимости.");
                return false;
            }

            // Получаем занятое время на фабрике
            var busyTimeSlots = await GetBusyTimeSlotsAsync(product.FactoryId);
            _logger.LogWarning($"Получено {busyTimeSlots.Count} занятых слотов времени");

            // Планируем этапы с учетом зависимостей и параллельности
            var plannedStages = await PlanStagesWithDependenciesAsync(
                product,
                executionPlan,
                busyTimeSlots,
                stageGraph);

            if (plannedStages == null || !plannedStages.Any())
            {
                _logger.LogWarning($"Не удалось запланировать этапы для продукта {product.Id}");
                return false;
            }

            _logger.LogWarning($"Успешно запланировано {plannedStages.Count} этапов для продукта {product.Id}");

            // Анализируем незапланированные этапы
            AnalyzeUnplannedStages(productStages, plannedStages, stageGraph);

            // Создаем рабочий период
            await CreateWorkingPeriodAsync(product, plannedStages);
            _logger.LogInformation($"Создан рабочий период для продукта {product.Id}");

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при планировании продукта {product.Id}");
            return false;
        }
    }

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
        int level = 0;

        while (remainingNodes.Any())
        {
            level++;
            _logger.LogWarning($"Построение уровня {level} выполнения");

            // Находим узлы, у которых все родители уже обработаны
            var readyNodes = remainingNodes
                .Where(node => !node.Parents.Any() ||
                              node.Parents.All(parentId =>
                                  !remainingNodes.Any(n => n.Id == parentId)))
                .ToList();

            if (!readyNodes.Any())
            {
                // Обнаружен цикл в зависимостях
                _logger.LogError("Обнаружен цикл в зависимостях этапов");

                // Выводим оставшиеся узлы и их зависимости для отладки
                foreach (var node in remainingNodes)
                {
                    _logger.LogError($"Узел {node.Id} зависит от: {string.Join(", ", node.Parents)}");
                }

                throw new InvalidOperationException("Обнаружен цикл в зависимостях этапов");
            }

            // Логируем этапы на этом уровне
            _logger.LogWarning($"Уровень {level}: {readyNodes.Count} этапов: " +
                             $"{string.Join(", ", readyNodes.Select(n => n.Id))}");

            // Группа этапов, которые можно выполнять параллельно
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

        _logger.LogWarning($"Построено {executionPlan.Count} уровней выполнения");
        return executionPlan;
    }

    private async Task<List<PlannedStage>> PlanStagesWithDependenciesAsync(
    ProductEntity product,
    List<StageExecutionGroup> executionPlan,
    List<TimeSlot> busyTimeSlots,
    List<StageNode> stageGraph)
    {
        var plannedStages = new List<PlannedStage>();
        var stageCompletionTimes = new Dictionary<Guid, DateTime>();
        var assemblyBrigadeMapping = new Dictionary<Guid, Guid>();

        _logger.LogWarning($"Начало планирования этапов для продукта {product.Id}");

        // Отладка графа
        DebugDependencyGraph(stageGraph, executionPlan);

        // Создаем словарь для быстрого доступа к узлам
        var nodeDict = stageGraph.ToDictionary(n => n.Id);

        // Сначала запланируем ВСЕ этапы уровня 1 (корневые)
        _logger.LogWarning("=== ПЛАНИРОВАНИЕ КОРНЕВЫХ ЭТАПОВ (Уровень 1) ===");

        var level1Group = executionPlan.FirstOrDefault(g => g.Level == 1);
        if (level1Group == null)
        {
            _logger.LogError("Не найден уровень 1 (корневые этапы)");
            return new List<PlannedStage>();
        }

        _logger.LogWarning($"Корневых этапов: {level1Group.Stages.Count}");

        // Планируем все корневые этапы
        foreach (var stage in level1Group.Stages)
        {
            _logger.LogWarning($"Планирование корневого этапа {stage.Id} ({stage.WorkingPeriodName})");

            var node = nodeDict[stage.Id];

            // У корневых этапов не должно быть родителей
            if (node.Parents.Any())
            {
                _logger.LogError($"Корневой этап {stage.Id} имеет родителей: {string.Join(", ", node.Parents)}");
                continue;
            }

            var result = await PlanSingleStageAsync(
                product, stage, node, busyTimeSlots,
                stageCompletionTimes, assemblyBrigadeMapping,
                plannedStages, stageGraph);

            if (!result)
            {
                _logger.LogError($"Не удалось запланировать корневой этап {stage.Id}");
                return new List<PlannedStage>();
            }
        }

        _logger.LogWarning($"Успешно запланировано {plannedStages.Count} корневых этапов");

        // Теперь планируем остальные уровни последовательно
        for (int level = 2; level <= executionPlan.Max(g => g.Level); level++)
        {
            var group = executionPlan.FirstOrDefault(g => g.Level == level);
            if (group == null)
            {
                _logger.LogWarning($"Уровень {level} не найден");
                continue;
            }

            _logger.LogWarning($"=== ПЛАНИРОВАНИЕ УРОВНЯ {level} ===");
            _logger.LogWarning($"Этапов на уровне: {group.Stages.Count}");

            // Проверяем, что все родители этапов этого уровня уже запланированы
            foreach (var stage in group.Stages)
            {
                var node = nodeDict[stage.Id];

                // Находим незапланированных родителей
                var unplannedParents = node.Parents
                    .Where(parentId => !plannedStages.Any(p => p.StageSampleId == parentId))
                    .ToList();

                if (unplannedParents.Any())
                {
                    _logger.LogError($"Этап {stage.Id} имеет незапланированных родителей: {string.Join(", ", unplannedParents)}");

                    // Показываем информацию о родителях
                    foreach (var parentId in unplannedParents)
                    {
                        var parentNode = nodeDict[parentId];
                        _logger.LogError($"  Родитель {parentId}: " +
                                       $"Родители родителя: {string.Join(", ", parentNode.Parents)}, " +
                                       $"Запланирован: {plannedStages.Any(p => p.StageSampleId == parentId)}");
                    }
                }
            }

            // Планируем этапы уровня
            bool levelPlannedSuccessfully = true;

            foreach (var stage in group.Stages)
            {
                var node = nodeDict[stage.Id];

                // Пропускаем этапы, чьи родители еще не запланированы
                var allParentsPlanned = node.Parents.All(parentId =>
                    plannedStages.Any(p => p.StageSampleId == parentId));

                if (!allParentsPlanned)
                {
                    _logger.LogWarning($"Пропускаем этап {stage.Id}: не все родители запланированы");
                    continue;
                }

                _logger.LogWarning($"Планирование этапа {stage.Id} ({stage.WorkingPeriodName})");

                var result = await PlanSingleStageAsync(
                    product, stage, node, busyTimeSlots,
                    stageCompletionTimes, assemblyBrigadeMapping,
                    plannedStages, stageGraph);

                if (!result)
                {
                    _logger.LogError($"Не удалось запланировать этап {stage.Id} уровня {level}");
                    levelPlannedSuccessfully = false;
                    break;
                }
            }

            if (!levelPlannedSuccessfully)
            {
                _logger.LogError($"Не удалось запланировать все этапы уровня {level}");
                return new List<PlannedStage>();
            }
        }

        _logger.LogWarning($"Успешно запланировано {plannedStages.Count} этапов для продукта {product.Id}");

        // Проверяем, что все этапы запланированы
        var allStageIds = stageGraph.Select(n => n.Id).ToList();
        var plannedStageIds = plannedStages.Select(p => p.StageSampleId).ToList();
        var unplannedStages = allStageIds.Except(plannedStageIds).ToList();

        if (unplannedStages.Any())
        {
            _logger.LogError($"Не запланированы этапы: {string.Join(", ", unplannedStages)}");
        }

        // Проверяем все зависимости
        ValidateAllDependencies(plannedStages, stageGraph);

        // Логируем порядок выполнения
        LogExecutionOrder(plannedStages, stageGraph);

        return plannedStages;
    }

    private async Task<bool> PlanSingleStageAsync(
     ProductEntity product,
     ProductSubTypeWorkingPeriodSampleEntity stage,
     StageNode node,
     List<TimeSlot> busyTimeSlots,
     Dictionary<Guid, DateTime> stageCompletionTimes,
     Dictionary<Guid, Guid> assemblyBrigadeMapping,
     List<PlannedStage> plannedStages,
     List<StageNode> stageGraph)
    {
        try
        {
            // Определяем минимальное время начала
            DateTime earliestStartTime = DateTime.Now;

            if (node.Parents.Any())
            {
                // Находим максимальное время завершения всех родителей
                var parentEndTimes = node.Parents
                    .Where(parentId => stageCompletionTimes.ContainsKey(parentId))
                    .Select(parentId => stageCompletionTimes[parentId])
                    .ToList();

                if (parentEndTimes.Any())
                {
                    earliestStartTime = parentEndTimes.Max();
                    _logger.LogWarning($"Этап {stage.Id} зависит от {node.Parents.Count} родителей. " +
                                     $"Максимальное время завершения родителей: {earliestStartTime}");
                }
                else
                {
                    _logger.LogError($"Родители этапа {stage.Id} не запланированы, но должны быть!");
                    return false;
                }
            }

            // Получаем тип этапа
            var stageTypeRelation = await _workingPeriodStageTypeRelationRepository
                .GetByProductSubTypeWorkingPeriodSampleId(stage.Id);
            var stageTypeId = stageTypeRelation?.FirstOrDefault()?.StageTypeId;
            StageTypeEntity stageType = null;

            if (stageTypeId.HasValue)
            {
                stageType = await _stageTypeRepository.GetById(stageTypeId.Value);
            }

            // Получаем требуемую бригаду
            Guid? brigadeId = await GetBrigadeForStageAsync(
                stage,
                stageType,
                product.Id,
                assemblyBrigadeMapping);

            if (!brigadeId.HasValue)
            {
                _logger.LogError($"Не найдена бригада для этапа {stage.Id}");
                return false;
            }

            // Парсим длительность этапа из минут
            TimeSpan duration = ParseStandartTimeToTimeSpan(stage.StandartTime);

            if (duration <= TimeSpan.Zero)
            {
                _logger.LogError($"Некорректная длительность для этапа {stage.Id}: {stage.StandartTime}");
                return false;
            }

            // Ищем свободное время НАЧИНАЯ С earliestStartTime
            var plannedTime = await FindAvailableTimeSlotAsync(
                earliestStartTime,
                duration,
                busyTimeSlots,
                brigadeId.Value,
                product.FactoryId);

            if (!plannedTime.HasValue)
            {
                _logger.LogError($"Не найдено свободное время для этапа {stage.Id}");
                return false;
            }

            // ГАРАНТИРУЕМ, что этап начинается после ВСЕХ родителей
            if (node.Parents.Any())
            {
                bool needsAdjustment = false;
                DateTime latestParentEnd = earliestStartTime;

                foreach (var parentId in node.Parents)
                {
                    if (stageCompletionTimes.TryGetValue(parentId, out var parentEndTime))
                    {
                        if (parentEndTime > latestParentEnd)
                            latestParentEnd = parentEndTime;

                        if (plannedTime.Value < parentEndTime)
                        {
                            needsAdjustment = true;
                            _logger.LogWarning($"НАРУШЕНИЕ: Этап {stage.Id} запланирован на {plannedTime.Value}, " +
                                             $"но родитель {parentId} завершается в {parentEndTime}");
                        }
                    }
                }

                // Если нужно корректировать, ищем время ПОСЛЕ самого позднего родителя
                if (needsAdjustment)
                {
                    _logger.LogWarning($"Ищем время для этапа {stage.Id} ПОСЛЕ всех родителей, самый поздний завершается в {latestParentEnd}");

                    // Ищем начиная СРАЗУ ПОСЛЕ завершения самого позднего родителя
                    var adjustedTime = await FindAvailableTimeSlotAsync(
                        latestParentEnd.AddMinutes(1), // Начинаем сразу после родителя
                        duration,
                        busyTimeSlots,
                        brigadeId.Value,
                        product.FactoryId);

                    if (adjustedTime.HasValue)
                    {
                        // Проверяем, что новое время действительно после всех родителей
                        bool allParentsBeforeAdjusted = true;
                        foreach (var parentId in node.Parents)
                        {
                            if (stageCompletionTimes.TryGetValue(parentId, out var parentEndTime))
                            {
                                if (adjustedTime.Value < parentEndTime)
                                {
                                    allParentsBeforeAdjusted = false;
                                    _logger.LogError($"Новое время {adjustedTime.Value} все еще раньше родителя {parentId}: {parentEndTime}");
                                    break;
                                }
                            }
                        }

                        if (allParentsBeforeAdjusted)
                        {
                            plannedTime = adjustedTime;
                            _logger.LogWarning($"Исправлено: этап {stage.Id} теперь начинается в {plannedTime.Value}");
                        }
                        else
                        {
                            _logger.LogError($"Не удалось найти время для этапа {stage.Id} после всех родителей");
                            return false;
                        }
                    }
                    else
                    {
                        _logger.LogError($"Не удалось найти время для этапа {stage.Id} после {latestParentEnd}");
                        return false;
                    }
                }
            }

            // Создаем запланированный этап
            var plannedStage = new PlannedStage
            {
                ProductId = product.Id,
                StageSampleId = stage.Id,
                BrigadeId = brigadeId.Value,
                StartTime = plannedTime.Value,
                EndTime = plannedTime.Value.Add(duration),
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

            _logger.LogWarning($"Успешно запланирован этап {stage.Id}: " +
                             $"{plannedStage.StartTime:yyyy-MM-dd HH:mm} - {plannedStage.EndTime:HH:mm}");

            // Дополнительная проверка
            if (node.Parents.Any())
            {
                foreach (var parentId in node.Parents)
                {
                    if (stageCompletionTimes.TryGetValue(parentId, out var parentEndTime))
                    {
                        if (plannedStage.StartTime < parentEndTime)
                        {
                            _logger.LogError($"КРИТИЧЕСКАЯ ОШИБКА: Этап {stage.Id} начинается в {plannedStage.StartTime}, " +
                                           $"но родитель {parentId} завершается только в {parentEndTime}!");
                        }
                        else
                        {
                            _logger.LogWarning($"OK: Этап {stage.Id} начинается в {plannedStage.StartTime}, " +
                                             $"после родителя {parentId} (завершился в {parentEndTime})");
                        }
                    }
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при планировании этапа {stage.Id}");
            return false;
        }
    }

    private void ValidateAllDependencies(
    List<PlannedStage> plannedStages,
    List<StageNode> stageGraph)
    {
        _logger.LogWarning("=== ПРОВЕРКА ВСЕХ ЗАВИСИМОСТЕЙ ===");

        var plannedDict = plannedStages.ToDictionary(p => p.StageSampleId);
        var nodeDict = stageGraph.ToDictionary(n => n.Id);

        bool allValid = true;

        foreach (var plannedStage in plannedStages)
        {
            if (nodeDict.TryGetValue(plannedStage.StageSampleId, out var node))
            {
                foreach (var parentId in node.Parents)
                {
                    if (plannedDict.TryGetValue(parentId, out var parentStage))
                    {
                        if (plannedStage.StartTime < parentStage.EndTime)
                        {
                            _logger.LogError($"НАРУШЕНИЕ: Этап {plannedStage.StageSampleId} начинается в {plannedStage.StartTime}, " +
                                           $"но родитель {parentId} завершается в {parentStage.EndTime}");
                            allValid = false;
                        }
                    }
                    else
                    {
                        _logger.LogError($"Родитель {parentId} этапа {plannedStage.StageSampleId} не запланирован!");
                        allValid = false;
                    }
                }
            }
        }

        if (allValid)
        {
            _logger.LogWarning("Все зависимости соблюдены корректно!");
        }
        else
        {
            _logger.LogError("Обнаружены нарушения в зависимостях!");
        }

        _logger.LogWarning("=== КОНЕЦ ПРОВЕРКИ ===");
    }

    private void LogExecutionOrder(List<PlannedStage> plannedStages, List<StageNode> stageGraph)
    {
        _logger.LogWarning("=== ПОРЯДОК ВЫПОЛНЕНИЯ ЭТАПОВ ===");

        foreach (var stage in plannedStages.OrderBy(s => s.StartTime))
        {
            var node = stageGraph.FirstOrDefault(n => n.Id == stage.StageSampleId);
            string dependencies = node?.Parents.Any() == true
                ? $" (зависит от: {string.Join(", ", node.Parents)})"
                : " (без зависимостей)";

            _logger.LogWarning($"Этап {stage.StageSampleId}: " +
                             $"{stage.StartTime:yyyy-MM-dd HH:mm} - {stage.EndTime:HH:mm}" +
                             $"{dependencies}");
        }

        _logger.LogWarning("=== КОНЕЦ ПОРЯДКА ===");
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
                if (currentTime < plannedStage.EndTime &&
                    currentTime.Add(duration) > plannedStage.StartTime)
                {
                    currentTime = plannedStage.EndTime;
                }
            }
        }

        return currentTime;
    }

    private async Task<Guid?> GetBrigadeForStageAsync(
        ProductSubTypeWorkingPeriodSampleEntity stage,
        StageTypeEntity stageType,
        Guid productId,
        Dictionary<Guid, Guid> assemblyBrigadeMapping)
    {
        try
        {
            // Для этапов "Сборка" используем одну и ту же бригаду для всего продукта
            if (stageType?.Name == "Сборка")
            {
                if (assemblyBrigadeMapping.TryGetValue(productId, out var existingBrigadeId))
                {
                    return existingBrigadeId;
                }
            }

            // Получаем связи типа этапа для образца
            var stageTypeRelations = await _workingPeriodStageTypeRelationRepository
                .GetByProductSubTypeWorkingPeriodSampleId(stage.Id);

            if (stageTypeRelations == null || !stageTypeRelations.Any())
            {
                _logger.LogWarning($"Для образца этапа {stage.Id} не найдены связи с типом этапа");
                return null;
            }

            // Берем первый тип этапа
            var stageTypeId = stageTypeRelations.First().StageTypeId;

            // Получаем бригады для этого типа этапа на фабрике продукта
            var product = _products?.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                _logger.LogWarning($"Не найден продукт {productId}");
                return null;
            }

            // Получаем все бригады на фабрике для этого типа этапа
            var factoryBrigades = await _brigadeRepository.GetByFactoryId(product.FactoryId);
            if (factoryBrigades == null || !factoryBrigades.Any())
            {
                _logger.LogWarning($"На фабрике {product.FactoryId} нет бригад");
                return null;
            }

            var brigadeForStageType = factoryBrigades.FirstOrDefault(b => b.StageTypeId == stageTypeId);
            if (brigadeForStageType == null)
            {
                _logger.LogWarning($"На фабрике {product.FactoryId} нет бригад для типа этапа {stageTypeId}");
                return null;
            }

            Guid brigadeId = brigadeForStageType.Id;

            // Запоминаем бригаду для этапов сборки
            if (stageType?.Name == "Сборка")
            {
                assemblyBrigadeMapping[productId] = brigadeId;
            }

            return brigadeId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при получении бригады для этапа {stage.Id}");
            return null;
        }
    }

    private async Task<List<TimeSlot>> GetBusyTimeSlotsAsync(Guid factoryId)
    {
        var busySlots = new List<TimeSlot>();

        try
        {
            // Получаем все рабочие периоды на этой фабрике
            var factoryProducts = _products
                .Where(p => p.FactoryId == factoryId)
                .Select(p => p.Id)
                .ToList();

            var factoryWorkingPeriods = _allWorkingPeriods
                .Where(wp => factoryProducts.Contains(wp.ProductId))
                .ToList();

            _logger.LogDebug($"На фабрике {factoryId}: {factoryWorkingPeriods.Count} рабочих периодов");

            // Для каждого рабочего периода получаем его этапы
            foreach (var workingPeriod in factoryWorkingPeriods)
            {
                var stages = _allWorkingPeriodStages
                    .Where(ws => ws.WorkingPeriodId == workingPeriod.Id)
                    .ToList();

                foreach (var stage in stages)
                {
                    // Получаем бригады для этого этапа
                    var brigadeRelations = await _workingPeriodStageBrigadeRelationRepository
                        .GetByWorkingPeriodStageId(stage.Id);

                    if (brigadeRelations != null && brigadeRelations.Any())
                    {
                        foreach (var relation in brigadeRelations)
                        {
                            // Преобразуем UTC время обратно в локальное для планирования
                            var localDateFrom = FromUtc(stage.DateFrom);
                            var localDateTo = FromUtc(stage.DateTo);

                            busySlots.Add(new TimeSlot
                            {
                                Start = localDateFrom,
                                End = localDateTo,
                                StageId = stage.Id,
                                BrigadeId = relation.BrigadeId
                            });
                        }
                    }
                    else
                    {
                        // Если нет связей с бригадами, все равно добавляем слот
                        var localDateFrom = FromUtc(stage.DateFrom);
                        var localDateTo = FromUtc(stage.DateTo);

                        busySlots.Add(new TimeSlot
                        {
                            Start = localDateFrom,
                            End = localDateTo,
                            StageId = stage.Id,
                            BrigadeId = null
                        });
                        _logger.LogWarning($"Этап {stage.Id} не имеет привязанных бригад");
                    }
                }
            }

            _logger.LogDebug($"Создано {busySlots.Count} занятых слотов для фабрики {factoryId}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при получении занятых слотов для фабрики {factoryId}");
        }

        return busySlots;
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

    private async Task<DateTime?> FindAvailableTimeSlotAsync(
     DateTime startFrom,
     TimeSpan duration,
     List<TimeSlot> busySlots,
     Guid brigadeId,
     Guid factoryId)
    {
        try
        {
            _logger.LogWarning($"Поиск слота: начало с {startFrom}, длительность={duration.TotalMinutes} минут, бригада={brigadeId}");

            // Если длительность больше рабочего дня, этап невозможен
            if (duration > WORKDAY_DURATION)
            {
                _logger.LogWarning($"Длительность этапа {duration.TotalMinutes} минут превышает рабочий день {WORKDAY_DURATION.TotalMinutes} минут");
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
                    _logger.LogDebug($"Пропускаем выходной: {currentTime.DayOfWeek}");
                    currentTime = currentTime.Date.AddDays(1).Add(WORKDAY_START);
                    daysSearched++;
                    continue;
                }

                _logger.LogDebug($"Проверка дня {currentTime.Date:yyyy-MM-dd} ({currentTime.DayOfWeek}) с {currentTime:HH:mm}");

                // Получаем все слоты для этой бригады в этот день
                var dayBusySlots = busySlots
                    .Where(s => s.BrigadeId == brigadeId &&
                               s.Start.Date == currentTime.Date)
                    .OrderBy(s => s.Start)
                    .ToList();

                // Начинаем поиск с currentTime или с начала рабочего дня, если currentTime раньше
                DateTime searchStart = currentTime;
                if (searchStart.TimeOfDay < WORKDAY_START)
                {
                    searchStart = currentTime.Date.Add(WORKDAY_START);
                }

                // Если день полностью свободен
                if (!dayBusySlots.Any())
                {
                    // Проверяем, помещается ли этап в рабочий день, начиная с searchStart
                    DateTime candidateStart = searchStart;
                    DateTime candidateEnd = candidateStart.Add(duration);

                    if (candidateEnd.TimeOfDay <= WORKDAY_END)
                    {
                        if (await CheckBrigadeAvailabilityAsync(brigadeId, candidateStart, duration, factoryId))
                        {
                            _logger.LogWarning($"Найден свободный слот в начале дня: {candidateStart}");
                            return candidateStart;
                        }
                    }
                }
                else
                {
                    // Ищем промежутки между занятыми слотами, начиная с searchStart
                    DateTime lastEnd = searchStart;

                    foreach (var busySlot in dayBusySlots)
                    {
                        // Если busySlot.End <= lastEnd, пропускаем (уже пройденный интервал)
                        if (busySlot.End <= lastEnd)
                            continue;

                        // Если busySlot.Start > lastEnd, проверяем промежуток
                        if (busySlot.Start > lastEnd)
                        {
                            TimeSpan gap = busySlot.Start - lastEnd;

                            if (gap >= duration)
                            {
                                // Промежуток достаточно большой
                                DateTime candidateStart = lastEnd;
                                DateTime candidateEnd = candidateStart.Add(duration);

                                // Проверяем, что этап не выходит за рабочий день
                                if (candidateEnd.TimeOfDay <= WORKDAY_END)
                                {
                                    if (await CheckBrigadeAvailabilityAsync(brigadeId, candidateStart, duration, factoryId))
                                    {
                                        _logger.LogWarning($"Найден слот в промежутке: {candidateStart}");
                                        return candidateStart;
                                    }
                                }
                            }
                        }

                        // Обновляем последнее занятое время
                        if (busySlot.End > lastEnd)
                        {
                            lastEnd = busySlot.End;
                        }
                    }

                    // Проверяем промежуток после последнего занятого слота
                    DateTime workDayEnd = currentTime.Date.Add(WORKDAY_END);
                    if (workDayEnd - lastEnd >= duration)
                    {
                        DateTime candidateStart = lastEnd;
                        DateTime candidateEnd = candidateStart.Add(duration);

                        if (candidateEnd.TimeOfDay <= WORKDAY_END)
                        {
                            if (await CheckBrigadeAvailabilityAsync(brigadeId, candidateStart, duration, factoryId))
                            {
                                _logger.LogWarning($"Найден слот в конце дня: {candidateStart}");
                                return candidateStart;
                            }
                        }
                    }
                }

                // Переходим к следующему рабочему дню
                daysSearched++;
                currentTime = currentTime.Date.AddDays(1).Add(WORKDAY_START);
            }

            _logger.LogWarning($"Не удалось найти свободный слот за {daysSearched} дней");

            // Дополнительная диагностика
            await DebugBusySlotsDetailed(busySlots, brigadeId, duration, startFrom);

            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при поиске свободного слота времени");
            return null;
        }
    }

    private async Task DebugBusySlotsDetailed(
        List<TimeSlot> busySlots,
        Guid brigadeId,
        TimeSpan requiredDuration,
        DateTime searchFrom)
    {
        _logger.LogWarning("=== ПОДРОБНАЯ ДИАГНОСТИКА ПОИСКА ВРЕМЕНИ ===");

        var brigadeSlots = busySlots
            .Where(s => s.BrigadeId == brigadeId)
            .OrderBy(s => s.Start)
            .ToList();

        _logger.LogWarning($"Всего занятых слотов для бригады {brigadeId}: {brigadeSlots.Count}");
        _logger.LogWarning($"Ищем время с: {searchFrom}");
        _logger.LogWarning($"Требуемая длительность: {requiredDuration.TotalMinutes} минут");

        if (!brigadeSlots.Any())
        {
            _logger.LogWarning("Нет занятых слотов - бригада полностью свободна!");
            return;
        }

        // Проверяем ближайшие 5 дней
        DateTime checkDate = searchFrom.Date;
        for (int i = 0; i < 5; i++)
        {
            // Пропускаем выходные
            while (checkDate.DayOfWeek == DayOfWeek.Saturday || checkDate.DayOfWeek == DayOfWeek.Sunday)
            {
                checkDate = checkDate.AddDays(1);
            }

            DateTime workStart = checkDate.Add(WORKDAY_START);
            DateTime workEnd = checkDate.Add(WORKDAY_END);

            var daySlots = brigadeSlots
                .Where(s => s.Start.Date == checkDate)
                .OrderBy(s => s.Start)
                .ToList();

            _logger.LogWarning($"День {checkDate:yyyy-MM-dd} ({checkDate.DayOfWeek}): {workStart:HH:mm} - {workEnd:HH:mm}");

            if (!daySlots.Any())
            {
                _logger.LogWarning($"  День полностью свободен");

                // Проверяем, поместится ли этап с searchFrom
                DateTime testStart = searchFrom > workStart ? searchFrom : workStart;
                if (testStart < searchFrom) testStart = searchFrom;

                DateTime testEnd = testStart.Add(requiredDuration);

                if (testEnd <= workEnd)
                {
                    _logger.LogWarning($"  МОЖНО ЗАПЛАНИРОВАТЬ: {testStart:HH:mm} - {testEnd:HH:mm}");
                }
                else
                {
                    _logger.LogWarning($"  Не помещается: {testStart:HH:mm} - {testEnd:HH:mm} (выходит за {workEnd:HH:mm})");
                }
            }
            else
            {
                // Показываем занятые слоты и свободные промежутки
                DateTime lastEnd = workStart;

                foreach (var slot in daySlots)
                {
                    // Показываем свободный промежуток перед этим занятым слотом
                    if (slot.Start > lastEnd)
                    {
                        TimeSpan gap = slot.Start - lastEnd;
                        _logger.LogWarning($"  Свободно: {lastEnd:HH:mm} - {slot.Start:HH:mm} ({gap.TotalMinutes} минут)");

                        // Проверяем, достаточно ли этого промежутка
                        if (gap >= requiredDuration)
                        {
                            // Проверяем, что начало не раньше searchFrom
                            DateTime potentialStart = lastEnd;
                            if (potentialStart < searchFrom)
                                potentialStart = searchFrom;

                            if (potentialStart < slot.Start)
                            {
                                DateTime potentialEnd = potentialStart.Add(requiredDuration);
                                if (potentialEnd <= slot.Start)
                                {
                                    _logger.LogWarning($"    МОЖНО ЗАПЛАНИРОВАТЬ: {potentialStart:HH:mm} - {potentialEnd:HH:mm}");
                                }
                            }
                        }
                    }

                    _logger.LogWarning($"  Занято: {slot.Start:HH:mm} - {slot.End:HH:mm}");

                    lastEnd = slot.End > lastEnd ? slot.End : lastEnd;
                }

                // Проверяем конец дня
                if (workEnd > lastEnd)
                {
                    TimeSpan endGap = workEnd - lastEnd;
                    _logger.LogWarning($"  Свободно: {lastEnd:HH:mm} - {workEnd:HH:mm} ({endGap.TotalMinutes} минут)");

                    if (endGap >= requiredDuration)
                    {
                        DateTime potentialStart = lastEnd;
                        if (potentialStart < searchFrom)
                            potentialStart = searchFrom;

                        if (potentialStart < workEnd)
                        {
                            DateTime potentialEnd = potentialStart.Add(requiredDuration);
                            if (potentialEnd <= workEnd)
                            {
                                _logger.LogWarning($"    МОЖНО ЗАПЛАНИРОВАТЬ: {potentialStart:HH:mm} - {potentialEnd:HH:mm}");
                            }
                        }
                    }
                }
            }

            checkDate = checkDate.AddDays(1);
        }

        _logger.LogWarning("=== КОНЕЦ ДИАГНОСТИКИ ===");
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

    // Исправленный метод CreateWorkingPeriodAsync
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

            // Обновляем даты продукта (DateOnly не требует преобразования)
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

    private async Task LoadFactoryBrigades()
    {
        try
        {
            _factoryBrigades = new Dictionary<Guid, List<BrigadeEntity>>();
            var allFactories = await _factoryRepository.GetAll();

            _logger.LogWarning($"Загружено {allFactories.Count} фабрик");

            foreach (var factory in allFactories)
            {
                var factoryBrigades = await _brigadeRepository.GetByFactoryId(factory.Id);
                if (factoryBrigades != null)
                {
                    _factoryBrigades[factory.Id] = factoryBrigades;
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
                _logger.LogWarning("StandartTime пуст или null, используется 0 минут");
                return TimeSpan.Zero;
            }

            // Пробуем распарсить как целое число (минуты)
            if (int.TryParse(standartTime, out int minutes))
            {
                _logger.LogDebug($"Преобразование StandartTime: '{standartTime}' -> {minutes} минут");
                return TimeSpan.FromMinutes(minutes);
            }

            // Если не число, пробуем распарсить как TimeSpan (на всякий случай)
            if (TimeSpan.TryParse(standartTime, out TimeSpan timeSpan))
            {
                _logger.LogDebug($"Преобразование StandartTime: '{standartTime}' -> TimeSpan {timeSpan}");
                return timeSpan;
            }

            // Если оба метода не сработали
            _logger.LogError($"Не удалось преобразовать StandartTime: '{standartTime}'");
            return TimeSpan.Zero;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при преобразовании StandartTime: '{standartTime}'");
            return TimeSpan.Zero;
        }
    }

    // Вспомогательные методы для отладки
    public async Task TestPlanning()
    {
        try
        {
            _logger.LogInformation("Тестовый запуск планирования");

            _products = await _productRepository.GetAll();
            _allWorkingPeriods = await _workingPeriodRepository.GetAll();
            _allWorkingPeriodStages = await _workingPeriodStageRepository.GetAll();

            await LoadFactoryBrigades();
            await LoadStageTypeIds();

            // Берём первый продукт
            var product = _products?.FirstOrDefault();
            if (product == null)
            {
                _logger.LogWarning("Нет продуктов для теста");
                return;
            }

            _logger.LogInformation($"Тестирование продукта: {product.Id}, SubTypeId: {product.ProductSubTypeId}");

            // Проверяем этапы
            var stages = await _productSubTypeWorkingPeriodSampleRepository
                .GetByProductSubTypeId(product.ProductSubTypeId);
            _logger.LogInformation($"Найдено этапов: {stages?.Count ?? 0}");

            // Пробуем запланировать
            await PlanProductAsync(product);

            _logger.LogInformation("Тест завершен");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в тестовом планировании");
        }
    }

    // Вспомогательные методы для работы с UTC
    private DateTime ToUtc(DateTime dateTime)
    {
        if (dateTime.Kind == DateTimeKind.Unspecified)
        {
            // Предполагаем, что это локальное время
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Local).ToUniversalTime();
        }
        else if (dateTime.Kind == DateTimeKind.Local)
        {
            return dateTime.ToUniversalTime();
        }
        else
        {
            // Уже UTC
            return dateTime;
        }
    }

    private DateTime FromUtc(DateTime dateTime)
    {
        if (dateTime.Kind == DateTimeKind.Unspecified)
        {
            // Предполагаем, что это UTC время
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc).ToLocalTime();
        }
        else if (dateTime.Kind == DateTimeKind.Utc)
        {
            return dateTime.ToLocalTime();
        }
        else
        {
            // Уже локальное время
            return dateTime;
        }
    }

    private void DebugDependencyGraph(List<StageNode> stageGraph, List<StageExecutionGroup> executionPlan)
    {
        _logger.LogWarning("=== ОТЛАДКА ГРАФА ЗАВИСИМОСТЕЙ ===");

        foreach (var group in executionPlan)
        {
            _logger.LogWarning($"Уровень {group.Level}: {group.Stages.Count} этапов");
            foreach (var stage in group.Stages)
            {
                var node = stageGraph.FirstOrDefault(n => n.Id == stage.Id);
                if (node != null)
                {
                    string parents = node.Parents.Any()
                        ? $"Родители: {string.Join(", ", node.Parents)}"
                        : "Без родителей";
                    string children = node.Children.Any()
                        ? $"Дети: {string.Join(", ", node.Children)}"
                        : "Без детей";

                    _logger.LogWarning($"  Этап {stage.Id} ({stage.WorkingPeriodName}): {parents}, {children}");
                }
            }
        }

        // Проверяем корректность графа
        foreach (var node in stageGraph)
        {
            // Проверяем, что родители существуют в графе
            foreach (var parentId in node.Parents)
            {
                if (!stageGraph.Any(n => n.Id == parentId))
                {
                    _logger.LogError($"Этап {node.Id} ссылается на несуществующего родителя {parentId}");
                }
            }
        }

        _logger.LogWarning("=== КОНЕЦ ОТЛАДКИ ГРАФА ===");
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

                if (unplannedParents.Any())
                {
                    _logger.LogError($"  Не запланированные родители: {string.Join(", ", unplannedParents)}");
                }
            }
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
        public int Level { get; set; } // Добавляем уровень для отладки
    }
}