using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Models
{
    public class WorkingPeriodStage
    {
        private WorkingPeriodStage(Guid id, Guid workingPeriodId, DateTime startDate, DateTime endDate, string status, TimeOnly? recycling, Guid productSubTypeWorkingPeriodsSampleId, Guid brigadeId, DateTime createTime, DateTime updateTime)
        { 
            Id = id;
            WorkingPeriodId = workingPeriodId;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            Recycling = recycling;
            ProductSubTypeWorkingPeriodsSampleId = productSubTypeWorkingPeriodsSampleId;
            BrigadeId = brigadeId;
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

        public Guid BrigadeId { get; set; }
        public Brigade Brigade { get; set; }
        public List<WorkingPeriodStageMaterial> workingPeriodStageMaterials { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        public static WorkingPeriodStage Create(Guid id, Guid workingPeriodId, DateTime startDate, DateTime endDate, string status, TimeOnly? recycling, Guid productSubTypeWorkingPeriodsSampleId, Guid brigadeId, DateTime createTime, DateTime updateTime)
        { 
            return new WorkingPeriodStage(id, workingPeriodId, startDate, endDate, status, recycling, productSubTypeWorkingPeriodsSampleId,brigadeId, createTime, updateTime);
        }
    }
}
