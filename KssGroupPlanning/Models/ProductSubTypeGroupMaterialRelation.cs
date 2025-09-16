namespace KssGroupPlanning.Models;

public class ProductSubTypeGroupMaterialRelation
{
    private ProductSubTypeGroupMaterialRelation(Guid id, Guid groupMaterialId, Guid productSubTypeWorkingPeriodSampleId)
    {
        Id = id;
        GroupMaterialId = groupMaterialId;
        ProductSubTypeWorkingPeriodSampleId = productSubTypeWorkingPeriodSampleId;
    }
    public Guid Id { get; set; }
    public Guid GroupMaterialId { get; set; }
    public GroupMaterial? GroupMaterial { get; set; }
    public Guid ProductSubTypeWorkingPeriodSampleId { get; set; }
    public ProductSubTypeWorkingPeriodsSample? ProductSubTypeWorkingPeriodsSample { get; set; }

    public static ProductSubTypeGroupMaterialRelation Create(Guid id, Guid groupMaterialId, Guid productSubTypeWorkingPeriodSampleId)
    {
        return new ProductSubTypeGroupMaterialRelation(id, groupMaterialId, productSubTypeWorkingPeriodSampleId);
    }
}
