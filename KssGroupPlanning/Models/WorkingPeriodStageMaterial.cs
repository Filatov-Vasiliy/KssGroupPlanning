namespace KssGroupPlanning.Models
{
    public class WorkingPeriodStageMaterial
    {
        private WorkingPeriodStageMaterial(Guid id, Guid workingPeriodStageId, Guid groupMaterialId, DateOnly dateDelivery) 
        { 
            Id = id;
            WorkingPeriodStageId = workingPeriodStageId;
            GroupMaterialId = groupMaterialId;
            DateDelivery = dateDelivery;
        }
        public Guid Id { get; set; }
        public WorkingPeriodStage WorkingPeriodStage { get; set; }
        public Guid WorkingPeriodStageId { get; set; }
        public Guid GroupMaterialId { get; set; }
        public GroupMaterial GroupMaterials { get; set; }
        public DateOnly DateDelivery { get; set; }
        public static WorkingPeriodStageMaterial Create(Guid id, Guid workingPeriodStageId, Guid groupMaterialId, DateOnly dateDelivery)
        {
            return new WorkingPeriodStageMaterial(id, workingPeriodStageId, groupMaterialId, dateDelivery);
        }
    }
}
