namespace KssGroupPlanning.Models
{
    public class Stage
    {
        private Stage(Guid id, string name, Guid productId, string status, DateTime date, DateTime createTime, DateTime updateTime)
        {
            Id = id;
            Name = name;
            ProductId = productId;
            Status = status;
            Date = date;
            CreateTime = createTime;
            UpdateTime = updateTime;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProductId { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public Product? Product { get; set; } = null;
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        public static Stage Create(Guid id, string name, Guid productId, string status, DateTime date, DateTime createTime, DateTime updateTime)
        { 
            return new Stage(id, name, productId, status, date, createTime, updateTime);
        }
    }
}
