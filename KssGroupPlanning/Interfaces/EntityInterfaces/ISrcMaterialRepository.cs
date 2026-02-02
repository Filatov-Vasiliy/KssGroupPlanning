using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Interfaces.EntityInterfaces;

    public interface ISrcMaterialRepository
    {
        Task Add(List<SrcMaterialEntity> srcMaterials);

        Task<List<SrcMaterialEntity>> GetSrcMaterials();

    }
