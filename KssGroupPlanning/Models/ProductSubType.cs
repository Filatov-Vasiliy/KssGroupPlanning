namespace KssGroupPlanning.Models;

public class ProductSubType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ProductTypeId { get; set; }
    public ProductType? ProductType { get; set; } = null;
}
