namespace KssGroupPlanning.Entities
{
    public class WorkingPeriodStageMaterialEntity
    {
        public Guid Id { get; set; }
        public WorkingPeriodStageEntity WorkingPeriodStageEntity { get; set; }
        public Guid WorkingPeriodStageId { get; set; }
        public Guid GroupMaterialId { get; set; }
        public GroupMaterialEntity GroupMaterialEntities { get; set; }
        public DateOnly DateDelivery { get; set; }
    }
}
