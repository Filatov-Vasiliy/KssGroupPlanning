namespace KssGroupPlanning.Models;

public class Factory
{ 
    public  Guid Id { get; set; }
    public string Name { get; set; }
    public List<Product>? Products { get; set; } = [];
}
