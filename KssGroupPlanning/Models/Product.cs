using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Models;

public class Product
{
    private Product(Guid id, string? number, Guid productSubTypeId, Guid factoryId, Guid orderId,Guid? parentProductId, string status, DateOnly? startDate, DateOnly? endDate, DateTime createTime, DateTime updateTime)
    {
        Id = id;
        Number = number;
        ProductSubTypeId = productSubTypeId;
        FactoryId = factoryId;
        OrderId = orderId;
        ParentProductId = parentProductId;
        Status = status;
        StartDate = startDate;
        EndDate = endDate;
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
    public DateOnly? StartDate { get; set; } = null;
    public DateOnly? EndDate { get; set; } = null;
    public DateTime CreateTime { get; set; } = DateTime.Now;
    public DateTime UpdateTime { get; set; } = DateTime.Now;

    public static Product Create(Guid id, string? number, Guid productSubTypeId, Guid factoryId, Guid orderId,Guid? parentProductId, string status, DateOnly? startDate, DateOnly? endDate, DateTime createTime, DateTime updateTime)
    {
        return new Product(id, number, productSubTypeId, factoryId, orderId, parentProductId: (Guid)parentProductId, status, startDate, endDate, createTime, updateTime);
    }
}
