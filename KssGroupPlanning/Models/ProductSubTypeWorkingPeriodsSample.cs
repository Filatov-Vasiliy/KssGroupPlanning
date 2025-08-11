namespace KssGroupPlanning.Models;

public class ProductSubTypeWorkingPeriodsSample
{
    public Guid Id { get; set; }
    public Guid ProductSubTypeId { get; set; }
    public ProductSubType? ProductSubType { get; set; } = null;
    public int RowNumber { get; set; }
    public string WorkingPeriodName { get; set; }
    public string StandartTime { get; set; } // string??
    public int StandartEmployee { get; set; }
    public Guid StageTypeId { get; set; }
    public StageType? StageType { get; set; } = null;
}
