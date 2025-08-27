namespace KssGroupPlanning.Entities
{
    public class WorkingPeriodsRelationsEntity
    {
        public Guid Id { get; set; }
        public Guid ParentProductSubTypeWokingPeriodsSampleId { get; set; }
        public ProductSubTypeWorkingPeriodsSampleEntity? ParentProductSubTypeWokingPeriodsSampleEntity { get; set; } = null;
        public Guid ChildProductSubTypeWokingPeriodsSampleId { get; set; }
        public ProductSubTypeWorkingPeriodsSampleEntity? ChildProductSubTypeWokingPeriodsSampleEntity { get; set; } = null;
    }
}
