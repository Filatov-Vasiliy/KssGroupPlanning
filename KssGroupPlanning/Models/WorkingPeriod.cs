namespace KssGroupPlanning.Models;
    public class WorkingPeriod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
