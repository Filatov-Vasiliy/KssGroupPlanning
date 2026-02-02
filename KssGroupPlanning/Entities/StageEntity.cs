namespace KssGroupPlanning.Entities
{
    public class StageEntity
    {
        public Guid Id { get; set; }
        
        public Guid ProductSubTypeStageSampleId { get; set; }
        public ProductSubTypeStageSampleEntity? ProductSubTypeStageSample { get; set; } = null;
        public Guid ProductId { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public ProductEntity? Product { get; set; } = null;
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
