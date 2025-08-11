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
    public string Status { get; set; }
    public DateOnly? Start_date { get; set; } = null;
    public DateOnly? End_date { get; set; } = null;
    public DateTime CreateTime { get; set; } = DateTime.Now;
    public DateTime UpdateTime { get; set; } = DateTime.Now;
}
