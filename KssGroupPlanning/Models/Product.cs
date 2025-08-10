namespace KssGroupPlanning.Models;

public class Product
{
    public int Id { get; set; }
    public string? Number { get; set; } = null; // ????
    public int ProductSubTypeId { get; set; }
    public ProductSubType? ProductSubType { get; set; }
    public int FactoryId { get; set; }
    public Factory? Factory { get; set; } = null;
    public int OrderId { get; set; }
    public Order? Order { get; set; } = null;
    public string Status { get; set; }
    public DateOnly? Start_date { get; set; } = null;
    public DateOnly? End_date { get; set; } = null;
    public DateTime CreateTime { get; set; } = DateTime.Now;
    public DateTime UpdateTime { get; set; } = DateTime.Now;
}
