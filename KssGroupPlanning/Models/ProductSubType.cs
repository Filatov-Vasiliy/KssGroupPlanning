namespace KssGroupPlanning.Models;

public class ProductSubType
{
    private ProductSubType(Guid id, string name, Guid productTypeId)
    { 
        Id = id;
        Name = name;
        ProductTypeId = productTypeId;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ProductTypeId { get; set; }
    public ProductType? ProductType { get; set; } = null;

    public static ProductSubType Create(Guid id, string name, Guid productTypeId)
    { 
        return new ProductSubType(id, name, productTypeId);
    }
}
