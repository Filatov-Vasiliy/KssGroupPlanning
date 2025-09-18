namespace KssGroupPlanning.Entities
{
    public class WorkingPeriodStageEntity
    {
        public Guid Id { get; set; }
        public Guid WorkingPeriodId { get; set; }
        public WorkingPeriodEntity? WorkingPeriod { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Status { get; set; }
        public TimeOnly? Recycling { get; set; }
        public ProductSubTypeWorkingPeriodSampleEntity? ProductSubTypeWorkingPeriodSample { get; set; }
        public Guid ProductSubTypeWorkingPeriodSampleId { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        public List<WorkingPeriodStageMaterialEntity>? workingPeriodStageMaterialEntities { get; set; }
    }
}
