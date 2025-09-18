namespace KssGroupPlanning.Entities
{
    public class WorkingPeriodRelationEntity
    {
        public Guid Id { get; set; }
        public Guid ParentProductSubTypeWokingPeriodsSampleId { get; set; }
        public ProductSubTypeWorkingPeriodSampleEntity? ParentProductSubTypeWorkingPeriodSampleEntity { get; set; } = null;
        public Guid ChildProductSubTypeWokingPeriodsSampleId { get; set; }
        public ProductSubTypeWorkingPeriodSampleEntity? ChildProductSubTypeWorkingPeriodSampleEntity { get; set; } = null;
    }
}
