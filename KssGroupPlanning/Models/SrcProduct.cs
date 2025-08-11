using CsvHelper.Configuration.Attributes;
namespace KssGroupPlanning.Models;

public class SrcProduct
{
    private SrcProduct(string productOrderName, DateOnly productOrderDate, string comment, string factory, string status, DateOnly createDate, string orderNumber, DateOnly orderDate, string productName, int qty)
    {
        ProductOrderName = productOrderName;
        ProductOrderDate = productOrderDate;
        Comment = comment;
        Factory = factory;
        Status = status;
        CreateDate = createDate;
        OrderNumber = orderNumber;
        OrderDate = orderDate;
        ProductName = productName;
        this.qty = qty;
    }

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
    public int qty { get; set; }
    //public DateTime? UploadTime { get; set; } = DateTime.Now;
    public static SrcProduct Create(string productOrderName, DateOnly productOrderDate, string comment, string factory, string status, DateOnly createDate, string orderNumber, DateOnly orderDate, string productName, int qty)
    { 
        return new SrcProduct(productOrderName, productOrderDate, comment, factory, status, createDate, orderNumber, orderDate, productName, qty);
    }
}
