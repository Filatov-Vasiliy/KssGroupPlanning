using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAll();
        Task<Order?> GetById(Guid id);
        Task<Order?> GetByNumber(string number);
        Task Add(Order order);
        Task Update(Order order);
        Task Delete(Guid id);
    }
}
