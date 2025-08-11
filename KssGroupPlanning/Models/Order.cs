namespace KssGroupPlanning.Models;

public class Order
{
    private Order(Guid id, string number, string manager, string contragent, decimal paymentAmount, decimal paymentCurrent, string status, DateTime createTime, DateTime updateTime )
    {
        Id = id;
        Number = number;
        Manager = manager;
        Contragent = contragent;
        PaymentAmount = paymentAmount;
        PaymentCurrent = paymentCurrent;
        Status = status;
        CreateTime = createTime;
        UpdateTime = updateTime;
    }

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

    public static Order Create(Guid id, string number, string manager, string contragent, decimal paymentAmount, decimal paymentCurrent, string status, DateTime createTime, DateTime updateTime)
    { 
        return new Order(id, number, manager, contragent, paymentAmount, paymentCurrent, status, createTime, updateTime);
    }
}
