using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface INewSrcOrderRepository
    {
        Task Add(List<SrcOrderEntity> srcOrders);

        Task<List<SrcOrderEntity>> GetSrcOrders();

    }
}
