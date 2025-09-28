using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface IWorkingPeriodRepository
    {
        Task<List<WorkingPeriod>> GetAll();
        Task<WorkingPeriod?> GetById(Guid id);
        Task<WorkingPeriod?> GetByProductId(Guid productId);
        Task<WorkingPeriod?> GetByName(string name);
        Task Add(WorkingPeriod workingPeriod);
        Task Update(WorkingPeriod workingPeriod);
        Task Delete(Guid id);
    }
}
