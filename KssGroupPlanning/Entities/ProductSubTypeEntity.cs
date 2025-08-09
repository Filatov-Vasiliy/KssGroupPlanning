namespace KssGroupPlanning.Entities;

public class ProductSubTypeEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ProductTypeId { get; set; }
    public ProductTypeEntity? ProductTypeEntity { get; set; } = null;
}
