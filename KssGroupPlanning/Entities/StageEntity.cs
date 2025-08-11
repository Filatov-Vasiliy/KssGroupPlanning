namespace KssGroupPlanning.Entities
{
    public class StageEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProductId { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public ProductEntity? Product { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
