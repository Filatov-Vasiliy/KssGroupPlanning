namespace KssGroupPlanning.Entities;

public class StageTypeEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public List<BrigadeEntity> Brigades { get; set; } = new List<BrigadeEntity>();
    public List<ProductSubTypeWorkingPeriodSampleEntity> ProductSubTypeWorkingPeriodSamples { get; set; } = new List<ProductSubTypeWorkingPeriodSampleEntity>();
    public List<WorkingPeriodStageTypeRelationEntity> WorkingPeriodStageTypeRelations { get; set; } = new List<WorkingPeriodStageTypeRelationEntity>();
}
