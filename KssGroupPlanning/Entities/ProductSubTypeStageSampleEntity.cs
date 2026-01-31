

namespace KssGroupPlanning.Entities;

public class ProductSubTypeStageSampleEntity
{
    public Guid Id { get; set; }
    public Guid ProductSubTypeId { get; set; }
    public ProductSubTypeEntity? ProductSubType { get; set; } = null;
    public int RowNumber { get; set; }
    public string StageName { get; set; }
    public Guid MaterialStageId { get; set; }
    public MaterialStageEntity? MaterialStage { get; set; } = null;
    public string StandartTime { get; set; } // string??
}
