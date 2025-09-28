namespace KssGroupPlanning.Models;

public class ProductType
{
    private ProductType(Guid id, string name)
    { 
        Id = id;
        Name = name;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<ProductSubType>? ProductSubTypes { get; set; } = new List<ProductSubType>();
    public static ProductType Create(Guid id, string name) 
    { 
        return new ProductType(id, name);
    }
}


