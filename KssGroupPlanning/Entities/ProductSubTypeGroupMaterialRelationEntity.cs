namespace KssGroupPlanning.Entities;

public class ProductSubTypeGroupMaterialRelationEntity
{
    public Guid Id { get; set; }
    public Guid GroupMaterialId { get; set; }
    public GroupMaterialEntity? GroupMaterial { get; set; }
    public Guid ProductSubTypeWorkingPeriodSampleId { get; set; }
    public ProductSubTypeWorkingPeriodSampleEntity? ProductSubTypeWorkingPeriodSample { get; set; }
}
