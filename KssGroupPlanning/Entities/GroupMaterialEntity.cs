namespace KssGroupPlanning.Entities;

public class GroupMaterialEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<WorkingPeriodStageMaterialEntity> workingPeriodStageMaterialEntities { get; set; } = new List<WorkingPeriodStageMaterialEntity>();
}
