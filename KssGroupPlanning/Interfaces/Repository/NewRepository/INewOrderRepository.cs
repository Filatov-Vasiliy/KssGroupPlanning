using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface INewOrderRepository
    {
        Task<List<OrderEntity>> GetAll();
        Task<OrderEntity?> GetById(Guid id);
        Task<OrderEntity?> GetByNumber(string number);
        Task Add(OrderEntity order);
        Task Update(OrderEntity order);
        Task Delete(Guid id);
    }
}
