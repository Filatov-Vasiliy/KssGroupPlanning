using CsvHelper.Configuration.Attributes;
using KssGroupPlanning.Models;
namespace KssGroupPlanning.Entities;

public class SrcOrderEntity
{
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
    
}
