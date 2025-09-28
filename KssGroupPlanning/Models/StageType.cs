using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;
using KssGroupPlanning.Interfaces.Repository;

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
    public List<Brigade>? Brigades { get; set; } = new List<Brigade>();
    public List<ProductSubTypeWorkingPeriodSample>? ProductSubTypeWorkingPeriodSamples { get; set; } = new List<ProductSubTypeWorkingPeriodSample>();
    public static StageType Create(Guid id, string name)
    { 
        return new StageType(id, name); 
    }
}

