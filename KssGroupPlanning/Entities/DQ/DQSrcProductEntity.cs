using CsvHelper.Configuration.Attributes;
using KssGroupPlanning.Entities.Src;

namespace KssGroupPlanning.Entities.DQ;

public class DQSrcProductEntity
{
    public Guid Id { get; set; }
    public string? ProductOrderName { get; set; }
    public DateOnly? ProductOrderDate { get; set; }
    public string? Comment { get; set; }
    public string? Factory { get; set; }
    public string? Status { get; set; }
    public DateOnly? CreateDate { get; set; }
    public string? OrderNumber { get; set; }
    public DateOnly? OrderDate { get; set; }
    public string? ProductName { get; set; }
    public int? Qty { get; set; }
    public bool LoadStatus { get; set; }
    public string? Reason { get; set; }
    public Guid? ProductId { get; set; } = Guid.Empty;
    public DateTime? CreateTime { get; set; } = DateTime.Now;
    public DQSrcProductEntity SrcToDQ(SrcProductEntity product, string reason, bool loadStatus, Guid? productId)
    {
        DQSrcProductEntity newEntity = new DQSrcProductEntity();
        newEntity.Id = product.Id;
        newEntity.ProductOrderName = product.ProductOrderName;
        newEntity.ProductOrderDate = product.ProductOrderDate;
        newEntity.Comment = product.Comment;
        newEntity.Factory = product.Factory;
        newEntity.Status = product.Status;
        newEntity.CreateDate = product.CreateDate;
        newEntity.OrderNumber = product.OrderNumber;
        newEntity.OrderDate = product.OrderDate;
        newEntity.ProductName = product.ProductName;
        newEntity.Qty = product.Qty;
        newEntity.LoadStatus = loadStatus;
        newEntity.Reason = reason;
        newEntity.ProductId = productId;
        return newEntity;
    }
}
