using KssGroupPlanning.Models;

namespace KssGroupPlanning.Entities;

public class WorkingPeriodStageBrigadeRelationEntity
{
    public Guid Id { get; set; }
    public Guid WorkingPeriodStageId { get; set; }
    public WorkingPeriodStageEntity? WorkingPeriodStageEntity { get; set; }
    public Guid BrigadeId { get; set; }
    public BrigadeEntity? BrigadeEntity { get; set; }
}
