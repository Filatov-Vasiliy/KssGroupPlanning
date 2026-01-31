using KssGroupPlanning.Entities;


namespace KssGroupPlanning.Interfaces.EntityInterfaces
{
    public interface IGroupMaterialRepository
    {
        Task<List<GroupMaterialEntity>> GetAll();
        Task<GroupMaterialEntity?> GetById(Guid id);
        Task<GroupMaterialEntity?> GetByName(string name);
        Task Add(GroupMaterialEntity groupMaterial);
        Task Update(GroupMaterialEntity groupMaterial);
        Task Delete(Guid id);
    }
}
