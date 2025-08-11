namespace KssGroupPlanning.Models;

public class ProductSubTypeStagesSample
{
    private ProductSubTypeStagesSample(Guid id, Guid productSubTypeId, int rowNumber, string stageName, string standartTime)
    { 
        Id = id;
        ProductSubTypeId = productSubTypeId;
        RowNumber = rowNumber;
        StageName = stageName;
        StandartTime = standartTime;
    }
    public Guid Id { get; set; }
    public Guid ProductSubTypeId { get; set; }
    public ProductSubType? ProductSubType { get; set; } = null;
    public int RowNumber { get; set; }
    public string StageName { get; set; }
    public string StandartTime { get; set; } // string??
    public static ProductSubTypeStagesSample Create(Guid id, Guid productSubTypeId, int rowNumber, string stageName, string standartTime)
    { 
        return new ProductSubTypeStagesSample(id, productSubTypeId, rowNumber, stageName, standartTime);
    }
}
