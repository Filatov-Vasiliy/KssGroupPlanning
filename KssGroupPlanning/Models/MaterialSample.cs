namespace KssGroupPlanning.Models;

public class MaterialSample
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly? DeliveryDate { get; set; } = null;
}
