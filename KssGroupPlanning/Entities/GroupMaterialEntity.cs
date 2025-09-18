namespace KssGroupPlanning.Entities;

public class GroupMaterialEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<WorkingPeriodStageMaterialEntity> WorkingPeriodStageMaterialEntities { get; set; } = new List<WorkingPeriodStageMaterialEntity>();
}
