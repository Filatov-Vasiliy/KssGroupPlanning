namespace KssGroupPlanning.Entities
{
    public class WorkingPeriodStageMaterialEntity
    {
        public Guid Id { get; set; }
        public ProductEntity? Product { get; set; } = null;
        public Guid ProductId { get; set; }
        public Guid GroupMaterialId { get; set; }
        public GroupMaterialEntity? GroupMaterial { get; set; } = null;
        public DateOnly DateDelivery { get; set; }
    }
}
