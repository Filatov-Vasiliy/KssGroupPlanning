namespace KssGroupPlanning.Models;

public class ProductType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<ProductSubType> ProductSubTypes { get; set; } = [];
}
