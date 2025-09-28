using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Models;

public class Product
{
    private Product(Guid id, string? number, Guid productSubTypeId, Guid factoryId, Guid orderId,Guid parentProductId, string status, DateOnly? start_date, DateOnly? end_date, DateTime createTime, DateTime updateTime)
    {
        Id = id;
        Number = number;
        ProductSubTypeId = productSubTypeId;
        FactoryId = factoryId;
        OrderId = orderId;
        ParentProductId = parentProductId;
        Status = status;
        Start_date = start_date;
        End_date = end_date;
        CreateTime = createTime;
        UpdateTime = updateTime;
    }

    public Guid Id { get; set; }
    public string? Number { get; set; } = null; // ????
    public Guid ProductSubTypeId { get; set; }
    public ProductSubType? ProductSubType { get; set; }
    public Guid FactoryId { get; set; }
    public Factory? Factory { get; set; } = null;
    public Guid OrderId { get; set; }
    public Order? Order { get; set; } = null;
    public Guid? ParentProductId { get; set; }
    public Product? ParentProduct { get; set; } = null;
    public List<Product>? ChildProducts { get; set; } = new List<Product>();
    public List<Stage> Stages { get; set; } = new List<Stage>();
    public string Status { get; set; }
    public List<WorkingPeriod>? WorkingPeriods { get; set; } = new List<WorkingPeriod>();
    public DateOnly? Start_date { get; set; } = null;
    public DateOnly? End_date { get; set; } = null;
    public DateTime CreateTime { get; set; } = DateTime.Now;
    public DateTime UpdateTime { get; set; } = DateTime.Now;

    public static Product Create(Guid id, string? number, Guid productSubTypeId, Guid factoryId, Guid orderId,Guid parentProductId, string status, DateOnly? start_date, DateOnly? end_date, DateTime createTime, DateTime updateTime)
    { 
        return new Product(id,number, productSubTypeId,factoryId,orderId,parentProductId, status,start_date,end_date,createTime,updateTime);
    }
}
