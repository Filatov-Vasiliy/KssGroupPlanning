using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Models;

public class WorkingPeriodRelation
{
    private WorkingPeriodRelation(Guid id, Guid parentProductSubTypeWorkingPeriodSampleId, Guid childProductSubTypeWorkingPeriodSampleId)
    {
        Id = id;
        ParentProductSubTypeWorkingPeriodSampleId = parentProductSubTypeWorkingPeriodSampleId;
        ChildProductSubTypeWorkingPeriodSampleId = childProductSubTypeWorkingPeriodSampleId;
    }
    public Guid Id { get; set; }
    public Guid ParentProductSubTypeWorkingPeriodSampleId { get; set; }
    public ProductSubTypeWorkingPeriodSample? ParentProductSubTypeWorkingPeriodSample { get; set; } = null;
    public Guid ChildProductSubTypeWorkingPeriodSampleId { get; set; }
    public ProductSubTypeWorkingPeriodSample? ChildProductSubTypeWorkingPeriodSample { get; set; } = null;
    public List<WorkingPeriodRelation> ChildWorkingPeriodRelations { get; set; } = new List<WorkingPeriodRelation>();
    public List<WorkingPeriodRelation> ParentWorkingPeriodRelations { get; set; } = new List<WorkingPeriodRelation>();

    public static WorkingPeriodRelation Create (Guid id, Guid parentProductSubTypeWorkingPeriodSampleId, Guid childProductSubTypeWorkingPeriodSampleId)
    {
        return new WorkingPeriodRelation(id, parentProductSubTypeWorkingPeriodSampleId,childProductSubTypeWorkingPeriodSampleId);
    }
}
