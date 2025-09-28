namespace KssGroupPlanning.Entities
{
    public class WorkingPeriodRelationEntity
    {
        public Guid Id { get; set; }
        public Guid ParentProductSubTypeWorkingPeriodSampleId { get; set; }
        public ProductSubTypeWorkingPeriodSampleEntity? ParentProductSubTypeWorkingPeriodSampleEntity { get; set; } = null;
        public Guid ChildProductSubTypeWorkingPeriodSampleId { get; set; }
        public ProductSubTypeWorkingPeriodSampleEntity? ChildProductSubTypeWorkingPeriodSampleEntity { get; set; } = null;
    }
}
