namespace KssGroupPlanning.Entities;

public class ProductEntity
{
    public Guid Id { get; set; }
    public string? Number { get; set; } = null; // ????
    public Guid ProductSubTypeId { get; set; }
    public ProductSubTypeEntity? ProductSubType { get; set; }
    public Guid FactoryId { get; set; }
    public FactoryEntity? Factory { get; set; } = null;
    public Guid OrderId { get; set; }
    public OrderEntity? Order { get; set; } = null;
    public Guid? ParentProductId { get; set; } = null;
    public ProductEntity? ParentProduct { get; set; } = null;
    public List<ProductEntity>? ChildProducts { get; set; } = new List<ProductEntity>();
    public string Status { get; set; }
    public List<StageEntity>? Stages { get; set; } =  new List<StageEntity>();
    public List<WorkingPeriodEntity>? WorkingPeriods { get; set; } = new List<WorkingPeriodEntity>();
    public List<WorkingPeriodStageMaterialEntity>? WorkingPeriodStageMaterials { get; set; } = new List<WorkingPeriodStageMaterialEntity>();

    public DateOnly? StartDate { get; set; } = null;
    public DateOnly? EndDate { get; set; } = null;
    public DateTime CreateTime { get; set; } = DateTime.Now;
    public DateTime UpdateTime { get; set; } = DateTime.Now;
}
