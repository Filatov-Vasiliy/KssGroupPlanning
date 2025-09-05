namespace KssGroupPlanning.Models
{
    public class WorkingPeriodStageMaterial
    {
        private WorkingPeriodStageMaterial(Guid id, Guid workingPeriodStageId, Guid materialSampleId, DateOnly dateDelivery) 
        { 
            Id = id;
            WorkingPeriodStageId = workingPeriodStageId;
            MaterialSampleId = materialSampleId;
            DateDelivery = dateDelivery;
        }
        public Guid Id { get; set; }
        public WorkingPeriodStage WorkingPeriodStage { get; set; }
        public Guid WorkingPeriodStageId { get; set; }
        public MaterialSample MaterialSample { get; set; }
        public Guid MaterialSampleId { get; set; }
        public DateOnly DateDelivery { get; set; }
        public static WorkingPeriodStageMaterial Create(Guid id, Guid workingPeriodStageId, Guid materialSampleId, DateOnly dateDelivery)
        {
            return new WorkingPeriodStageMaterial(id, workingPeriodStageId, materialSampleId, dateDelivery);
        }
    }
}
