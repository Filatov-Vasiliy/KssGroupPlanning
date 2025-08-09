namespace KssGroupPlanning.Entities
{
    public class WorkingPeriodStageEntity
    {
        public int Id { get; set; }
        public int WorkingPeriodId { get; set; }
        public WorkingPeriodEntity? WorkingPeriod { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public TimeOnly? Recycling { get; set; }
        public ProductSubTypeWorkingPeriodsSampleEntity ProductSubTypeWorkingPeriodsSample { get; set; }
        public int ProductSubTypeWorkingPeriodsSampleId { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
