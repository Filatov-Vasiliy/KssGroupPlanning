

namespace KssGroupPlanning.Entities;

public class WorkingPeriodStageBrigadeRelationEntity
{
    public Guid Id { get; set; }
    public Guid WorkingPeriodStageId { get; set; }
    public WorkingPeriodStageEntity? WorkingPeriodStage { get; set; }
    public Guid BrigadeId { get; set; }
    public BrigadeEntity? Brigade { get; set; }

}
