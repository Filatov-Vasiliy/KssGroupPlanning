using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Models;

public class Factory
{ 
    private Factory(Guid id, string name) 
    {
        Id = id;
        Name = name;
    }
    public  Guid Id { get; set; }
    public string Name { get; set; }
    public List<Product>? Products { get; set; } = new List<Product>();
    public List<Brigade>? Brigades { get; set; } = new List<Brigade>();

    public static Factory Create(Guid id, string name)
    {
        return new Factory(id, name);
    }
}
