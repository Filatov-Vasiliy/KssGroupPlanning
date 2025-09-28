namespace KssGroupPlanning.Entities
{
    public class WorkingPeriodStageMaterialEntity
    {
        public Guid Id { get; set; }
        public WorkingPeriodStageEntity? WorkingPeriodStage { get; set; } = null;
        public Guid WorkingPeriodStageId { get; set; }
        public Guid GroupMaterialId { get; set; }
        public GroupMaterialEntity? GroupMaterial { get; set; } = null;
        public DateOnly DateDelivery { get; set; }
    }
}
