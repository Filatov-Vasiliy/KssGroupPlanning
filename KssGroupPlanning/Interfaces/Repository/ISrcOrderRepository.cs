using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface ISrcOrderRepository
    {
        Task Add(List<SrcOrder> srcOrders);

        Task<List<SrcOrder>> GetSrcOrders();

    }
}
