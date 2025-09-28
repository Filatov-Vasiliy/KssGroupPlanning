namespace KssGroupPlanning.Entities;

public class ProductSubTypeEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ProductTypeId { get; set; }
    public ProductTypeEntity? ProductType { get; set; } = null;
    public List<ProductEntity>? Products { get; set; } = new List<ProductEntity>();
    public List<ProductSubTypeWorkingPeriodSampleEntity>? ProductSubTypeWorkingPeriodSamples { get; set; } = new List<ProductSubTypeWorkingPeriodSampleEntity>();
    public List<ProductSubTypeStageSampleEntity>? ProductSubTypeStageSamples { get; set; } = new List<ProductSubTypeStageSampleEntity>();
}
