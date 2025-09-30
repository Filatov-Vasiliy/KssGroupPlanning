using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Models
{
    public class WorkingPeriodStage
    {
        private WorkingPeriodStage(Guid id, Guid workingPeriodId, DateTime dateFrom, DateTime dateTo, string status, TimeOnly? recycling, Guid productSubTypeWorkingPeriodSampleId, DateTime createTime, DateTime updateTime)
        { 
            Id = id;
            WorkingPeriodId = workingPeriodId;
            DateFrom = DateFrom;
            DateTo = dateTo;
            Status = status;
            Recycling = recycling;
            ProductSubTypeWorkingPeriodSampleId = productSubTypeWorkingPeriodSampleId;
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
        public ProductSubTypeWorkingPeriodSample? ProductSubTypeWorkingPeriodSample { get; set; } = null;
        public Guid ProductSubTypeWorkingPeriodSampleId { get; set; }
        public List<WorkingPeriodStageMaterial>? workingPeriodStageMaterials { get; set; } = new List<WorkingPeriodStageMaterial>();
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        public List<Brigade>? Brigades { get; set; } = new List<Brigade>();
        public List<WorkingPeriodStageBrigadeRelation> WorkingPeriodStageBrigadeRelations { get; set; } = new List<WorkingPeriodStageBrigadeRelation>();

        public static WorkingPeriodStage Create(Guid id, Guid workingPeriodId, DateTime dateFrom, DateTime dateTo, string status, TimeOnly? recycling, Guid productSubTypeWorkingPeriodSampleId, DateTime createTime, DateTime updateTime)
        { 
            return new WorkingPeriodStage(id, workingPeriodId, dateFrom, dateTo, status, recycling, productSubTypeWorkingPeriodSampleId, createTime, updateTime);
        }
    }
}
