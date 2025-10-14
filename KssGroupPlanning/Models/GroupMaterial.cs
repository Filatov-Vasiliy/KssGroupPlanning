using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Models;

public class GroupMaterial
{
    private GroupMaterial(Guid id, string name) 
    { 
        Id = id;
        Name = name;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<WorkingPeriodStageMaterial>? WorkingPeriodStageMaterials { get; set; } = new List<WorkingPeriodStageMaterial>();
    public List<ProductSubTypeWorkingPeriodSample>? ProductSubTypeWorkingPeriodSamples { get; set; } = new List<ProductSubTypeWorkingPeriodSample>();
    public List<ProductSubTypeGroupMaterialRelation> ProductSubTypeGroupMaterialRelations { get; set; } = new List<ProductSubTypeGroupMaterialRelation>();
    public MaterialStage? MaterialStage { get; set; } = null;
    public static GroupMaterial Create(Guid id, string name)
    {
        return new GroupMaterial(id, name);
    }
}
