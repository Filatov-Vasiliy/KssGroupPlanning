namespace KssGroupPlanning.Models
{
    public class WorkingPeriodStage
    {
        public int Id { get; set; }
        public int WorkingPeriodId { get; set; }
        public WorkingPeriod? WorkingPeriod { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public TimeOnly? Recycling { get; set; }
        public ProductSubTypeWorkingPeriodsSample ProductSubTypeWorkingPeriodsSample { get; set; }
        public int ProductSubTypeWorkingPeriodsSampleId { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
