using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface INewWorkingPeriodRepository
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
