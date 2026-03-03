using KssGroupPlanning.Entities.Src;

namespace KssGroupPlanning.Interfaces.EntityInterfaces.Src;

public interface ISrcMaterialRepository
{
    Task Add(List<SrcMaterialEntity> srcMaterials);

    Task<List<SrcMaterialEntity>> GetSrcMaterials();
    Task RemoveDuplicates();
    Task TruncateTable();
}
