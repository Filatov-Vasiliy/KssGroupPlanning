using KssGroupPlanning.Entities;


namespace KssGroupPlanning.Interfaces.EntityInterfaces
{
    public interface IWorkingPeriodRepository
    {
        Task<List<WorkingPeriodEntity>> GetAll();
        Task<WorkingPeriodEntity?> GetById(Guid id);
        Task<WorkingPeriodEntity?> GetByProductId(Guid productId);
        Task<WorkingPeriodEntity?> GetByName(string name);
        Task Add(WorkingPeriodEntity workingPeriod);
        Task Update(WorkingPeriodEntity workingPeriod);
        Task Delete(Guid id);
    }
}
