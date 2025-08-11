namespace KssGroupPlanning.Models;

public class StageType
{
    private StageType(Guid id, string name) 
    { 
      Id = id;
      Name = name;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public static StageType Create(Guid id, string name)
    { 
        return new StageType(id, name); 
    }
}

