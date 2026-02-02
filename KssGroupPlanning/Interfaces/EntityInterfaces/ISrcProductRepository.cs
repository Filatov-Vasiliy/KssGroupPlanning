using KssGroupPlanning.Entities;


namespace KssGroupPlanning.Interfaces.EntityInterfaces
{
    public interface ISrcProductRepository
    {
        Task Add(List<SrcProductEntity> srcProducts);

        Task<List<SrcProductEntity>> GetSrcProducts();

    }
}