namespace KssGroupPlanning.Entities;

public class ProductSubTypeWorkingPeriodsSampleEntity
{
    public Guid Id { get; set; }
    public Guid ProductSubTypeId { get; set; }
    public ProductSubTypeEntity? ProductSubTypeEntity { get; set; } = null;
    public int RowNumber { get; set; }
    public string WorkingPeriodName { get; set; }
    public string StandartTime { get; set; } // string??
    public int StandartEmployee { get; set; }
    //public Guid StageTypeId { get; set; } -- удалено
    //public StageTypeEntity? StageTypeEntity { get; set; } = null;  -- удалено
}
