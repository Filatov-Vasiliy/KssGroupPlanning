namespace KssGroupPlanning.Models;

public class ProductSubTypeStagesSample
{
    public int Id { get; set; }
    public int ProductSubTypeId { get; set; }
    public ProductSubType? ProductSubType { get; set; } = null;
    public int RowNumber { get; set; }
    public string StageName { get; set; }
    public string StandartTime { get; set; } // string??
}
