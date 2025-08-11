namespace KssGroupPlanning.Models
{
    public class WorkingPeriodStage
    {
        public Guid Id { get; set; }
        public Guid WorkingPeriodId { get; set; }
        public WorkingPeriod? WorkingPeriod { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public TimeOnly? Recycling { get; set; }
        public ProductSubTypeWorkingPeriodsSample ProductSubTypeWorkingPeriodsSample { get; set; }
        public Guid ProductSubTypeWorkingPeriodsSampleId { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
