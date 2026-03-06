using KssGroupPlanning.Entities.Src;


namespace KssGroupPlanning.Interfaces.EntityInterfaces.Src;

public interface ISrcOrderRepository
{
    Task Add(List<SrcOrderEntity> srcOrders);

    Task<List<SrcOrderEntity>> GetSrcOrders();
    Task RemoveDuplicates();
    Task TruncateTable();

}
