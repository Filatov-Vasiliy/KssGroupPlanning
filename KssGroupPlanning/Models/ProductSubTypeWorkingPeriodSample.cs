namespace KssGroupPlanning.Models;

public class ProductSubTypeWorkingPeriodSample
{
    private ProductSubTypeWorkingPeriodSample(Guid id, Guid productSubTypeId,int rowNumber, string workingPeriodName, string standartTime, int standartEmployee)
    { 
        Id = id;
        ProductSubTypeId = productSubTypeId;
        RowNumber = rowNumber;
        WorkingPeriodName = workingPeriodName;
        StandartTime = standartTime;
        StandartEmployee = standartEmployee;
    }
    public Guid Id { get; set; }
    public Guid ProductSubTypeId { get; set; }
    public ProductSubType? ProductSubType { get; set; } = null;
    public int RowNumber { get; set; }
    public string WorkingPeriodName { get; set; }
    public string StandartTime { get; set; } // string??
    public int StandartEmployee { get; set; }
    public static ProductSubTypeWorkingPeriodSample Create(Guid id, Guid productSubTypeId, int rowNumber, string workingPeriodName, string standartTime, int standartEmployee)
    { 
        return new ProductSubTypeWorkingPeriodSample(id, productSubTypeId, rowNumber, workingPeriodName, standartTime, standartEmployee);
    }
}
