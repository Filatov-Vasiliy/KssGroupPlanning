namespace KssGroupPlanning.Entities
{
    public class WorkingPeriodStageMaterialEntity
    {
        public Guid Id { get; set; }
        public WorkingPeriodStageEntity WorkingPeriodStageEntity { get; set; }
        public Guid WorkingPeriodStageId { get; set; }
        public MaterialSampleEntity MaterialSampleEntity { get; set; }
        public Guid MaterialSampleId {get; set; }
        public DateOnly DateDelivery { get; set; }
    }
}
