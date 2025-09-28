namespace KssGroupPlanning.Entities;

public class WorkingPeriodEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public Guid ProductId { get; set; }
    public ProductEntity? Product { get; set; } = null;
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public DateTime CreateTime { get; set; } = DateTime.Now;
    public DateTime UpdateTime { get; set; } = DateTime.Now;

    public List<WorkingPeriodStageEntity>? WorkingPeriodStages { get; set; } = new List<WorkingPeriodStageEntity>();
}
