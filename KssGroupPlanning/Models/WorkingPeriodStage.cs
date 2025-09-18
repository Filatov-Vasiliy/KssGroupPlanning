using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Models
{
    public class WorkingPeriodStage
    {
        private WorkingPeriodStage(Guid id, Guid workingPeriodId, DateTime dateFrom, DateTime dateTo, string status, TimeOnly? recycling, Guid productSubTypeWorkingPeriodSampleId, Guid brigadeId, DateTime createTime, DateTime updateTime)
        { 
            Id = id;
            WorkingPeriodId = workingPeriodId;
            DateFrom = DateFrom;
            DateTo = dateTo;
            Status = status;
            Recycling = recycling;
            ProductSubTypeWorkingPeriodSampleId = productSubTypeWorkingPeriodSampleId;
            BrigadeId = brigadeId;
            CreateTime = createTime;
            UpdateTime = updateTime;
        }
        public Guid Id { get; set; }
        public Guid WorkingPeriodId { get; set; }
        public WorkingPeriod? WorkingPeriod { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Status { get; set; }
        public TimeOnly? Recycling { get; set; }
        public ProductSubTypeWorkingPeriodSample ProductSubTypeWorkingPeriodSample { get; set; }
        public Guid ProductSubTypeWorkingPeriodSampleId { get; set; }

        public Guid BrigadeId { get; set; }
        public Brigade Brigade { get; set; }
        public List<WorkingPeriodStageMaterial> workingPeriodStageMaterials { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        public static WorkingPeriodStage Create(Guid id, Guid workingPeriodId, DateTime dateFrom, DateTime dateTo, string status, TimeOnly? recycling, Guid productSubTypeWorkingPeriodSampleId, Guid brigadeId, DateTime createTime, DateTime updateTime)
        { 
            return new WorkingPeriodStage(id, workingPeriodId, dateFrom, dateTo, status, recycling, productSubTypeWorkingPeriodSampleId,brigadeId, createTime, updateTime);
        }
    }
}
