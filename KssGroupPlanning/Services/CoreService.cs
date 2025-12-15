using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Services
{
    public class CoreService
    {
        private readonly IProductTypeRepository _productTypeRepository;
        public CoreService(IProductTypeRepository productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }
        public async Task<List<ProductType>> GetAll()
        {
            return await _productTypeRepository.GetAll();
        }
    }
}
