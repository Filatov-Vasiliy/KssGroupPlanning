using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Interfaces.EntityInterfaces
{
    public interface IBrigadeRepository
    {
        Task<List<BrigadeEntity>> GetAll();
        Task<BrigadeEntity?> GetById(Guid id);
        Task <List<BrigadeEntity?>> GetByFactoryId(Guid factoryId);
        Task<List<BrigadeEntity?>> GetByName(string Name);
        Task<List<BrigadeEntity?>> GetByStageTypeId(Guid StageTypeId);
        Task Add(BrigadeEntity brigade);
        Task Update(BrigadeEntity brigade);
        Task Delete(Guid id);
    }
}
