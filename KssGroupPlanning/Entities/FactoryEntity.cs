namespace KssGroupPlanning.Entities;

public class FactoryEntity
{ 
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<ProductEntity>? Products { get; set; } = [];
}
