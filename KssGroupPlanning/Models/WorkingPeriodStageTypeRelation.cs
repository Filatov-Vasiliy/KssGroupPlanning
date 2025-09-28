namespace KssGroupPlanning.Models;

public class WorkingPeriodStageTypeRelation
{
    private WorkingPeriodStageTypeRelation(Guid id, Guid stageTypeId, Guid productSubTypeWorkingPeriodSampleId) 
    {
        Id = id;
        StageTypeId = stageTypeId;
        ProductSubTypeWorkingPeriodSampleId = productSubTypeWorkingPeriodSampleId;
    }
    public Guid Id { get; set; }
    public Guid StageTypeId { get; set; }
    public StageType? StageType { get; set; } = null;
    public Guid ProductSubTypeWorkingPeriodSampleId { get; set; }
    public ProductSubTypeWorkingPeriodSample? ProductSubTypeWorkingPeriodSample { get; set; } = null;
    public static WorkingPeriodStageTypeRelation Create(Guid id, Guid stageTypeId, Guid productSubTypeWorkingPeriodSampleId) 
    {
        return new WorkingPeriodStageTypeRelation(id, stageTypeId, productSubTypeWorkingPeriodSampleId);
    }
}
