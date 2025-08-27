namespace KssGroupPlanning.Models;

public class WorkingPeriodsRelations
{
    private WorkingPeriodsRelations(Guid id, Guid parentProductSubTypeWokingPeriodsSampleId, Guid childProductSubTypeWokingPeriodsSampleId)
    {
        Id = id;
        ParentProductSubTypeWokingPeriodsSampleId = parentProductSubTypeWokingPeriodsSampleId;
        ChildProductSubTypeWokingPeriodsSampleId = childProductSubTypeWokingPeriodsSampleId;
    }
    public Guid Id { get; set; }
    public Guid ParentProductSubTypeWokingPeriodsSampleId { get; set; }
    public ProductSubTypeWorkingPeriodsSample? ParentProductSubTypeWokingPeriodsSample { get; set; } = null;
    public Guid ChildProductSubTypeWokingPeriodsSampleId { get; set; }
    public ProductSubTypeWorkingPeriodsSample? ChildProductSubTypeWokingPeriodsSample { get; set; } = null;
    public static WorkingPeriodsRelations Create (Guid id, Guid parentProductSubTypeWokingPeriodsSampleId, Guid childProductSubTypeWokingPeriodsSampleId)
    {
        return new WorkingPeriodsRelations(id, parentProductSubTypeWokingPeriodsSampleId,childProductSubTypeWokingPeriodsSampleId);
    }
}
