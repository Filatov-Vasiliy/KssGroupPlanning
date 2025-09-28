namespace KssGroupPlanning.Entities
{
    public class WorkingPeriodStageTypeRelationEntity
    {
        public Guid Id { get; set; }
        public Guid StageTypeId { get; set; }
        public StageTypeEntity? StageType { get; set; } = null;
        public Guid ProductSubTypeWorkingPeriodSampleId { get; set; }
        public ProductSubTypeWorkingPeriodSampleEntity? ProductSubTypeWorkingPeriodSampleEntity { get; set; } = null;
    }
}
