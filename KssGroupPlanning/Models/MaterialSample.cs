namespace KssGroupPlanning.Models;

public class MaterialSample
{
    private MaterialSample(Guid id, string name, DateOnly deliveryDate) 
    {
        Id = id;
        Name = name;
        DeliveryDate = deliveryDate;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateOnly? DeliveryDate { get; set; } = null;
    public static MaterialSample Create(Guid id, string name, DateOnly deliveryDate) 
    { 
        return new MaterialSample(id, name, deliveryDate);
    }
}
