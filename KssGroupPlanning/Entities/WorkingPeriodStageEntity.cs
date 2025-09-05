namespace KssGroupPlanning.Entities
{
    public class WorkingPeriodStageEntity
    {
        public Guid Id { get; set; }
        public Guid WorkingPeriodId { get; set; }
        public WorkingPeriodEntity? WorkingPeriod { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public TimeOnly? Recycling { get; set; }
        public ProductSubTypeWorkingPeriodsSampleEntity ProductSubTypeWorkingPeriodsSample { get; set; }
        public Guid ProductSubTypeWorkingPeriodsSampleId { get; set; }
        public Guid BrigadeId { get; set; }
        public BrigadeEntity Brigade { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        public List<WorkingPeriodStageMaterialEntity> workingPeriodStageMaterialEntities { get; set; }
    }
}
