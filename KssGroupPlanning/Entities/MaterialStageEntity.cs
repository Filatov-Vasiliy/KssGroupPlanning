using KssGroupPlanning.Models;

namespace KssGroupPlanning.Entities
{
    public class MaterialStageEntity
    {
        public Guid Id { get; set; }
        public string StageName { get; set; }
        public Guid? GroupMaterialId { get; set; } = null;
        public GroupMaterialEntity? GroupMaterial { get; set; } = null;
        public List<ProductSubTypeStageSampleEntity>? ProductSubTypeStageSamples { get; set; } = new List<ProductSubTypeStageSampleEntity>();
    }
}
