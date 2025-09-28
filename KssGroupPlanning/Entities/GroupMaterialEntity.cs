namespace KssGroupPlanning.Entities;

public class GroupMaterialEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<WorkingPeriodStageMaterialEntity>? WorkingPeriodStageMaterials { get; set; } = new List<WorkingPeriodStageMaterialEntity>();
    public List<ProductSubTypeWorkingPeriodSampleEntity>? ProductSubTypeWorkingPeriodSamples { get; set; } = new List<ProductSubTypeWorkingPeriodSampleEntity>();
}
