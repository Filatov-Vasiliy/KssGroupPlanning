namespace KssGroupPlanning.Entities;

public class ProductSubTypeWorkingPeriodsSampleEntity
{
    public int Id { get; set; }
    public int ProductSubTypeId { get; set; }
    public ProductSubTypeEntity? ProductSubTypeEntity { get; set; } = null;
    public int RowNumber { get; set; }
    public string WorkingPeriodName { get; set; }
    public string StandartTime { get; set; } // string??
    public int StandartEmployee { get; set; }
    public int StageTypeId { get; set; }
    public StageTypeEntity? StageTypeEntity { get; set; } = null;
}
