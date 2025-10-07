namespace KssGroupPlanning.Models
{
    public class MaterialStage
    {
        private MaterialStage(Guid id, string stageName, Guid? groupMaterialId) 
        { 
            Id =id;
            StageName =stageName;
            GroupMaterialId =groupMaterialId;
        }
        public Guid Id { get; set; }
        public string StageName { get; set; }
        public Guid? GroupMaterialId { get; set; }
        public GroupMaterial? GroupMaterial { get; set; }
        public List<ProductSubTypeStageSample>? ProductSubTypeStageSample { get; set; } = new List<ProductSubTypeStageSample>();
        public static MaterialStage Create(Guid id, string stageName, Guid? groupMaterialId)
        {
            return new MaterialStage(id, stageName, (Guid)groupMaterialId);
        }

    }
}
