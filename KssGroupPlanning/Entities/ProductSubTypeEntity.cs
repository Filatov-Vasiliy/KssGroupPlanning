namespace KssGroupPlanning.Entities;

public class ProductSubTypeEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ProductTypeId { get; set; }
    public ProductTypeEntity? ProductTypeEntity { get; set; } = null;
}
