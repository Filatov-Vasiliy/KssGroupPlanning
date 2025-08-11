namespace KssGroupPlanning.Models;

public class MaterialSample
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateOnly? DeliveryDate { get; set; } = null;
}
