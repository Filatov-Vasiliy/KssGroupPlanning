namespace KssGroupPlanning.Entities;

public class MaterialSampleEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateOnly? DeliveryDate { get; set; } = null;
}
