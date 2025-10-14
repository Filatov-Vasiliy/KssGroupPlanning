using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface INewGroupMaterialRepository
    {
        Task<List<GroupMaterialEntity>> GetAll();
        Task<GroupMaterialEntity?> GetById(Guid id);
        Task<GroupMaterialEntity?> GetByName(string name);
        Task Add(GroupMaterialEntity groupMaterial);
        Task Update(GroupMaterialEntity groupMaterial);
        Task Delete(Guid id);
    }
}
