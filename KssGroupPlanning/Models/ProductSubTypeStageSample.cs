namespace KssGroupPlanning.Models;

public class ProductSubTypeStageSample
{
    private ProductSubTypeStageSample(Guid id, Guid productSubTypeId, int rowNumber,Guid materialStageId, string standartTime)
    { 
        Id = id;
        ProductSubTypeId = productSubTypeId;
        RowNumber = rowNumber;
        MaterialStageId = materialStageId;
        StandartTime = standartTime;
    }
    public Guid Id { get; set; }
    public Guid ProductSubTypeId { get; set; }
    public ProductSubType? ProductSubType { get; set; } = null;
    public int RowNumber { get; set; }
    public string StandartTime { get; set; } // string??
    public Guid MaterialStageId { get; set; }
    public MaterialStage? MaterialStage { get; set; } = null;
    public static ProductSubTypeStageSample Create(Guid id, Guid productSubTypeId, int rowNumber, Guid materialStageId, string standartTime)
    { 
        return new ProductSubTypeStageSample(id, productSubTypeId, rowNumber, materialStageId, standartTime);
    }
}
