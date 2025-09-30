using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;

namespace KssGroupPlanning.Models;

public class WorkingPeriodStageBrigadeRelation
{
    private WorkingPeriodStageBrigadeRelation(Guid id, Guid workingPeriodStageId, Guid brigadeId) 
    { 
        Id = id;
        WorkingPeriodStageId = workingPeriodStageId;
        BrigadeId = brigadeId;
    }
    public Guid Id { get; set; }
    public Guid WorkingPeriodStageId { get; set; }
    public WorkingPeriodStage? WorkingPeriodStage { get; set; }
    public Guid BrigadeId { get; set; }
    public Brigade? Brigade { get; set; }

    public static WorkingPeriodStageBrigadeRelation Create(Guid id, Guid workingPeriodStageId, Guid brigadeId) {
        return new WorkingPeriodStageBrigadeRelation(id, workingPeriodStageId, brigadeId);
    }
}
