using KssGroupPlanning.Entities;


namespace KssGroupPlanning.Interfaces.EntityInterfaces
{
    public interface ISrcOrderRepository
    {
        Task Add(List<SrcOrderEntity> srcOrders);

        Task<List<SrcOrderEntity>> GetSrcOrders();

    }
}
