namespace KssGroupPlanning.Models;

public class WorkingPeriodsStageTypes
{
    private WorkingPeriodsStageTypes(Guid id, Guid stageTypeId, Guid productSubTypeWokingPeriodsSampleId) 
    {
        Id = id;
        StageTypeId = stageTypeId;
        ProductSubTypeWokingPeriodsSampleId = productSubTypeWokingPeriodsSampleId;
    }
    public Guid Id { get; set; }
    public Guid StageTypeId { get; set; }
    public StageType? StageType { get; set; } = null;
    public Guid ProductSubTypeWokingPeriodsSampleId { get; set; }
    public ProductSubTypeWorkingPeriodsSample? ProductSubTypeWokingPeriodsSample { get; set; } = null;
    public static WorkingPeriodsStageTypes Create(Guid id, Guid stageTypeId, Guid productSubTypeWokingPeriodsSampleId) 
    {
        return new WorkingPeriodsStageTypes(id, stageTypeId, productSubTypeWokingPeriodsSampleId);
    }
}
