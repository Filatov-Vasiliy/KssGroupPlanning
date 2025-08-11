namespace KssGroupPlanning.Models;

public class Order
{
    public Guid Id { get; set; }
    public string Number { get; set; }
    public string Manager { get; set; }
    public string Contragent { get; set; }
    public decimal PaymentAmount { get; set; }
    public decimal PaymentCurrent { get; set; }
    public string Status { get; set; }
    public List<Product> Products { get; set; } = [];
    public DateTime CreateTime { get; set; } = DateTime.Now;
    public DateTime UpdateTime { get; set; } = DateTime.Now;
}
