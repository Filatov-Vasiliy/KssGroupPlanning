namespace KssGroupPlanning.Entities;

public class ProductSubTypeStagesSampleEntity
{
    public Guid Id { get; set; }
    public Guid ProductSubTypeId { get; set; }
    public ProductSubTypeEntity? ProductSubTypeEntity { get; set; } = null;
    public int RowNumber { get; set; }
    public string StageName { get; set; }
    public string StandartTime { get; set; } // string??
}
