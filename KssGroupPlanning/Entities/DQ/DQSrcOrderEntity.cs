using CsvHelper.Configuration.Attributes;
using KssGroupPlanning.Entities.Src;
using Microsoft.AspNetCore.WebUtilities;

namespace KssGroupPlanning.Entities.DQ;

public class DQSrcOrderEntity
{
    public Guid Id { get; set; }
    public string? OrderName { get; set; }
    public string? Status { get; set; }
    public string? Contragent { get; set; }
    public string? Dogovor { get; set; }
    public string? Manager { get; set; }
    public string? OrderNumber { get; set; }
    public DateOnly? OrderDate { get; set; }
    public DateOnly? SchemeDate { get; set; }
    public DateOnly? LogisticDate { get; set; }
    public DateOnly? CreateDate { get; set; }
    public decimal? PaymentAmount { get; set; }
    public decimal? PaymentCurrent { get; set; }
    public int? Qty { get; set; }
    public bool LoadStatus { get; set; }
    public string? Reason { get; set; }
    public Guid? OrderId { get; set; } = Guid.Empty;
    public DateTime? CreateTime { get; set; } = DateTime.Now;
    public DQSrcOrderEntity SrcToDQ(SrcOrderEntity order, string reason, bool loadStatus, Guid? orderId)
    {
        DQSrcOrderEntity newEntity = new DQSrcOrderEntity();
        newEntity.Id = Guid.NewGuid();
        newEntity.OrderName = order.OrderName;
        newEntity.Status = order.Status;
        newEntity.Contragent = order.Contragent;
        newEntity.Dogovor = order.Dogovor;
        newEntity.Manager = order.Manager;
        newEntity.OrderNumber = order.OrderNumber;
        newEntity.OrderDate = order.OrderDate;
        newEntity.SchemeDate = order.SchemeDate;
        newEntity.LogisticDate = order.LogisticDate;
        newEntity.CreateDate = order.CreateDate;
        newEntity.PaymentAmount = order.PaymentAmount;
        newEntity.PaymentCurrent = order.PaymentCurrent;
        newEntity.Qty = order.Qty;
        newEntity.LoadStatus = loadStatus;
        newEntity.Reason = reason;
        newEntity.OrderId = orderId;
        return newEntity;
    }
}