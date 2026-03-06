using KssGroupPlanning.Entities.Src;


namespace KssGroupPlanning.Interfaces.EntityInterfaces.Src;

public interface ISrcProductRepository
{
    Task Add(List<SrcProductEntity> srcProducts);

    Task<List<SrcProductEntity>> GetSrcProducts();

    Task RemoveDuplicates();
    Task TruncateTable();

}