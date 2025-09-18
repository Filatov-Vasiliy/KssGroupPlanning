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
    public static GroupMaterial Create(Guid id, string name)
    {
        return new GroupMaterial(id, name);
    }
}
