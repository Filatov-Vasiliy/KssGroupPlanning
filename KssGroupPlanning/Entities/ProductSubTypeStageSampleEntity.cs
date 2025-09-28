namespace KssGroupPlanning.Entities;

public class ProductSubTypeStageSampleEntity
{
    public Guid Id { get; set; }
    public Guid ProductSubTypeId { get; set; }
    public ProductSubTypeEntity? ProductSubType { get; set; } = null;
    public int RowNumber { get; set; }
    public string StageName { get; set; }
    public string StandartTime { get; set; } // string??
}
