namespace KssGroupPlanning.Models;

public class ProductSubTypeWorkingPeriodsSample
{
    private ProductSubTypeWorkingPeriodsSample(Guid id, Guid productSubTypeId,int rowNumber, string workingPeriodName, string standartTime, int standartEmployee, Guid stageTypeId)
    { 
        Id = id;
        ProductSubTypeId = productSubTypeId;
        RowNumber = rowNumber;
        WorkingPeriodName = workingPeriodName;
        StandartTime = standartTime;
        StandartEmployee = standartEmployee;
        StageTypeId = stageTypeId;
    }
    public Guid Id { get; set; }
    public Guid ProductSubTypeId { get; set; }
    public ProductSubType? ProductSubType { get; set; } = null;
    public int RowNumber { get; set; }
    public string WorkingPeriodName { get; set; }
    public string StandartTime { get; set; } // string??
    public int StandartEmployee { get; set; }
    public Guid StageTypeId { get; set; }
    public StageType? StageType { get; set; } = null;
    public static ProductSubTypeWorkingPeriodsSample Create(Guid id, Guid productSubTypeId, int rowNumber, string workingPeriodName, string standartTime, int standartEmployee, Guid stageTypeId)
    { 
        return new ProductSubTypeWorkingPeriodsSample(id, productSubTypeId, rowNumber, workingPeriodName, standartTime, standartEmployee, stageTypeId);
    }
}
