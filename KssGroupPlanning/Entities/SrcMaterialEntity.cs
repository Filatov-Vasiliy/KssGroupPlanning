using CsvHelper.Configuration.Attributes;

namespace KssGroupPlanning.Entities
{
    public class SrcMaterialEntity
    {
        public Guid Id { get; set; }
        [Name("Product order name")]
        public string ProductOrderName { get; set; }
        [Name("Product_order_date")]
        public DateOnly ProductOrderDate { get; set; }
        [Name("Material Name")]
        public string MaterialName { get; set; }
        [Name("Material group")]
        public string MaterialGroup { get; set; }
        [Name("qty")]
        public decimal Qty { get; set; }
        [Name("Current qty")]
        public decimal CurrentQty { get; set; }
        [Name("Posted Date")]
        public DateOnly PostedDate { get; set; }
        [Name("For admission date")]
        public DateOnly ForAdmissionDate { get; set; }
        [Name("Create Date")]
        public DateOnly CreateDate { get; set; }
        [Name("Product order name child")]
        public string ProductOrderNameChild { get; set; }
    }
}
