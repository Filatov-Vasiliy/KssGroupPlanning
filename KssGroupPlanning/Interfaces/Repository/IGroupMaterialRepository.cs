using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface IGroupMaterialRepository
    {
        Task<List<GroupMaterial>> GetAll();
        Task<GroupMaterial?> GetById(Guid id);
        Task<GroupMaterial?> GetByName(string name);
        Task Add(GroupMaterial groupMaterial);
        Task Update(GroupMaterial groupMaterial);
        Task Delete(Guid id);
    }
}
