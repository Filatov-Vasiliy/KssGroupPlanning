namespace KssGroupPlanning.Entities;

public class BrigadeEntity
{
    public Guid Id { get; set; }
    public StageTypeEntity? StageType { get; set; }
    public Guid StageTypeId { get; set; }
    public FactoryEntity? Factory { get; set; }
    public Guid FactoryId { get; set; }
    public int CountEmployee { get; set; }
    public List<WorkingPeriodStageEntity>? WorkingPeriodStages { get; set; } = new List<WorkingPeriodStageEntity>();
    public List<WorkingPeriodStageBrigadeRelationEntity> WorkingPeriodStageBrigadeRelations { get; set; } = new List<WorkingPeriodStageBrigadeRelationEntity>();
}
