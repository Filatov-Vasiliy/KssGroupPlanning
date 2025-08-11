using CsvHelper.Configuration.Attributes;
using Microsoft.AspNetCore.Identity;
namespace KssGroupPlanning.Models;

public class SrcOrder
{
    private SrcOrder(string orderName, string status, string contragent, string dogovor, string manager, string orderNumber, DateOnly orderDate, DateOnly schemeDate, DateOnly logisticDate, DateOnly createDate, decimal paymentAmount, decimal paymentCurrent, int qty)
    {
        OrderName = orderName;
        Status = status;
        Contragent = contragent;
        Dogovor = dogovor;
        Manager = manager;
        OrderNumber = orderNumber;
        OrderDate = orderDate;
        SchemeDate = schemeDate;
        LogisticDate = logisticDate;
        CreateDate = createDate;
        PaymentAmount = paymentAmount;
        PaymentCurrent = paymentCurrent;
        Qty = qty;
    }
    [Name("order_name")]
    public string OrderName { get; set; }
    [Name("status")]
    public string Status { get; set; }
    [Name("contragent")]
    public string Contragent { get; set; }
    [Name("dogovor")]
    public string Dogovor { get; set; }
    [Name("manager")]
    public string Manager { get; set; }
    [Name("order_number")]
    public string OrderNumber { get; set; }
    [Name("order_date")]
    [Format("dd.mm.yyyy")]
    public DateOnly OrderDate { get; set; }
    [Name("scheme_date")]
    [Format("dd.mm.yyyy")]
    public DateOnly SchemeDate { get; set; }
    [Name("logistic_date")]
    [Format("dd.mm.yyyy")]
    public DateOnly LogisticDate { get; set; }
    [Name("create_date")]
    [Format("dd.mm.yyyy")]
    public DateOnly CreateDate { get; set; }
    [Name("payment_amount")]
    public decimal PaymentAmount { get; set; }
    [Name("payment_current")]
    public decimal PaymentCurrent { get; set; }
    [Name("qty")]
    public int Qty { get; set; }
    //public DateTime? UploadTime { get; set; } = DateTime.Now;
    public static SrcOrder Create(string orderName, string status, string contragent, string dogovor, string manager, string orderNumber, DateOnly orderDate, DateOnly schemeDate, DateOnly logisticDate, DateOnly createDate, decimal paymentAmount, decimal paymentCurrent, int qty)
    {
        return new SrcOrder(orderName, status, contragent, dogovor, manager, orderNumber, orderDate, schemeDate, logisticDate, createDate, paymentAmount, paymentCurrent, qty);
    }
}
