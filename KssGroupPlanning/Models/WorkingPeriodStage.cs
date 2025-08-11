namespace KssGroupPlanning.Models
{
    public class WorkingPeriodStage
    {
        private WorkingPeriodStage(Guid id, Guid workingPeriodId, DateTime startDate, DateTime endDate, string status, TimeOnly? recycling, Guid productSubTypeWorkingPeriodsSampleId, DateTime createTime, DateTime updateTime)
        { 
            Id = id;
            WorkingPeriodId = workingPeriodId;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            Recycling = recycling;
            ProductSubTypeWorkingPeriodsSampleId = productSubTypeWorkingPeriodsSampleId;
            CreateTime = createTime;
            UpdateTime = updateTime;
        }
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
        public static WorkingPeriodStage Create(Guid id, Guid workingPeriodId, DateTime startDate, DateTime endDate, string status, TimeOnly? recycling, Guid productSubTypeWorkingPeriodsSampleId, DateTime createTime, DateTime updateTime)
        { 
            return new WorkingPeriodStage(id, workingPeriodId, startDate, endDate, status, recycling, productSubTypeWorkingPeriodsSampleId, createTime, updateTime);
        }
    }
}
