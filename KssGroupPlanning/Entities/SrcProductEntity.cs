using CsvHelper.Configuration.Attributes;
namespace KssGroupPlanning.Entities;

public class SrcProductEntity
{
    public Guid Id { get; set; }
    [Name("product_order_name")]
    public string ProductOrderName { get; set; }
    [Name("productorderdate")]
    [Format("dd.mm.yyyy")]
    public DateOnly ProductOrderDate { get; set; }
    [Name("comment")]
    public string Comment { get; set; }
    [Name("factory")]
    public string Factory { get; set; }
    [Name("status")]
    public string Status { get; set; }
    [Name("create_date")]
    [Format("dd.mm.yyyy")]
    public DateOnly CreateDate { get; set; }
    [Name("order_number")]
    public string OrderNumber { get; set; }
    [Name("order_date")]
    [Format("dd.mm.yyyy")]
    public DateOnly OrderDate { get; set; }
    [Name("product_name")]
    public string ProductName { get; set; }
    [Name("qty")]
    public int Qty { get; set; }
//public DateTime? UploadTime { get; set; } = DateTime.Now;
}
