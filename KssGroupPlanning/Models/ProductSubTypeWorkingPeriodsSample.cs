namespace KssGroupPlanning.Models;

public class ProductSubTypeWorkingPeriodsSample
{
    public int Id { get; set; }
    public int ProductSubTypeId { get; set; }
    public ProductSubType? ProductSubType { get; set; } = null;
    public int RowNumber { get; set; }
    public string WorkingPeriodName { get; set; }
    public string StandartTime { get; set; } // string??
    public int StandartEmployee { get; set; }
    public int StageTypeId { get; set; }
    public StageType? StageType { get; set; } = null;
}
