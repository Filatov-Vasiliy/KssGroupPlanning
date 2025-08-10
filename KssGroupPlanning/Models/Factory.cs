namespace KssGroupPlanning.Models;

public class Factory
{ 
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Product>? Products { get; set; } = [];
}
