namespace KssGroupPlanning.Entities
{
    public class WorkingPeriodsStageTypesEntity
    {
        public Guid Id { get; set; }
        public Guid StageTypeId { get; set; }
        public StageTypeEntity? StageType { get; set; } = null;
        public Guid ProductSubTypeWokingPeriodsSampleId { get; set; }
        public ProductSubTypeWorkingPeriodsSampleEntity? ProductSubTypeWokingPeriodsSampleEntity { get; set; } = null;
    }
}
