using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Models;
    public class WorkingPeriod
    {
    private WorkingPeriod(Guid id, string name, string status, Guid productId, DateTime dateFrom, DateTime dateTo, DateTime createTime, DateTime updateTime)
    {
        Id = id;
        Name = name;
        Status = status;
        ProductId = productId;
        DateFrom = dateFrom;
        DateTo = dateTo;
        CreateTime = createTime;
        UpdateTime = updateTime;
    }

    public Guid Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        public List<WorkingPeriodStage>? WorkingPeriodStages { get; set; } = new List<WorkingPeriodStage>();

    public static WorkingPeriod Create(Guid id, string name, string status, Guid productId, DateTime dateFrom, DateTime dateTo, DateTime createTime, DateTime updateTime)
        { 
            return new WorkingPeriod(id, name, status, productId, dateFrom, dateTo, createTime, updateTime);
        }
    }
