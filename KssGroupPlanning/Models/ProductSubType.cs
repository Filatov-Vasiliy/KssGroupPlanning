namespace KssGroupPlanning.Models;

public class ProductSubType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ProductTypeId { get; set; }
    public ProductType? ProductType { get; set; } = null;
}
