namespace KssGroupPlanning.Entities;

public class ProductEntity
{
    public Guid Id { get; set; }
    public string? Number { get; set; } = null; // ????
    public Guid ProductSubTypeId { get; set; }
    public ProductSubTypeEntity? ProductSubTypeEntity { get; set; }
    public Guid FactoryId { get; set; }
    public FactoryEntity? FactoryEntity { get; set; } = null;
    public Guid OrderId { get; set; }
    public OrderEntity? OrderEntity { get; set; } = null;
    public Guid? ParentProductId { get; set; }
    public ProductEntity? ParentProductEntity { get; set; } 
    public List<ProductEntity>? ChildProductEntities { get; set; } = new List<ProductEntity>();
    public string Status { get; set; }
    public DateOnly? StartDate { get; set; } = null;
    public DateOnly? EndDate { get; set; } = null;
    public DateTime CreateTime { get; set; } = DateTime.Now;
    public DateTime UpdateTime { get; set; } = DateTime.Now;
}
