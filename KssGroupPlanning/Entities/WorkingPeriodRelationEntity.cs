namespace KssGroupPlanning.Entities
{
    public class WorkingPeriodRelationEntity
    {
        public Guid Id { get; set; }
        public Guid ParentProductSubTypeWorkingPeriodSampleId { get; set; }
        public ProductSubTypeWorkingPeriodSampleEntity? ParentProductSubTypeWorkingPeriodSample { get; set; } = null;
        public Guid ChildProductSubTypeWorkingPeriodSampleId { get; set; }
        public ProductSubTypeWorkingPeriodSampleEntity? ChildProductSubTypeWorkingPeriodSample { get; set; } = null;
    }
}
