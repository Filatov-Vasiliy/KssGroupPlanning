namespace KssGroupPlanning.Entities;

public class ProductEntity
{
    public int Id { get; set; }
    public string? Number { get; set; } = null; // ????
    public int ProductSubTypeId { get; set; }
    public ProductSubTypeEntity? ProductSubTypeEntity { get; set; }
    public int FactoryId { get; set; }
    public FactoryEntity? Factory { get; set; } = null;
    public int OrderId { get; set; }
    public OrderEntity? Order { get; set; } = null;
    public string Status { get; set; }
    public DateOnly? Start_date { get; set; } = null;
    public DateOnly? End_date { get; set; } = null;
    public DateTime CreateTime { get; set; } = DateTime.Now;
    public DateTime UpdateTime { get; set; } = DateTime.Now;
}
