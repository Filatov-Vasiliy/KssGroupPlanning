using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Interfaces.EntityInterfaces
{
    public interface IOrderRepository
    {
        Task<List<OrderEntity>> GetAll();
        Task<List<OrderEntity>> GetAllDetailed();

        Task<OrderEntity?> GetById(Guid id);
        Task<OrderEntity?> GetByNumber(string number);
        Task Add(OrderEntity order);
        Task Update(OrderEntity order);
        Task Delete(Guid id);
    }
}
