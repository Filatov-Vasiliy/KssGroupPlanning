using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.EntityInterfaces;
using KssGroupPlanning.Services.EntityServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace KssGroupPlanning.Services.IntergrationServices
{
    public class IntegrationService
    {
        private readonly SrcOrderService _srcOrderService;
        private readonly SrcProductService _srcProductService;
        private readonly SrcMaterialService _srcMaterialService;
        private readonly OrderService _orderService;
        private readonly ProductService _productService;
        private readonly WorkingPeriodStageMaterialService _workingPeriodStageMaterialService;
        private readonly FactoryService _factoryService;
        private readonly ProductTypeService _productTypeService;
        private readonly ProductSubTypeService _productSubTypeService;
        private readonly ILogger<IntegrationService> _logger;
        public IntegrationService(ILogger<IntegrationService> logger, SrcOrderService srcOrderService, SrcMaterialService srcMaterialService, SrcProductService srcProductService, FactoryService factoryService, ProductSubTypeService productSubTypeService,OrderService orderService,ProductService productService, WorkingPeriodStageMaterialService workingPeriodStageMaterialService,ProductTypeService productTypeService)
        {
            _logger = logger;
            _srcOrderService = srcOrderService;
            _srcProductService = srcProductService;
            _srcMaterialService = srcMaterialService;
            _factoryService = factoryService;
            _productSubTypeService = productSubTypeService;
            _productTypeService = productTypeService;
            _orderService = orderService;
            _productService = productService;
            _workingPeriodStageMaterialService = workingPeriodStageMaterialService;
        }
        /*
        public async Task <List<SrcOrderEntity>> GetAll()
        {

            return 
        }
        */
        public List<SrcOrderEntity> ClearOrderNumber (List<SrcOrderEntity> orders) 
        {
            foreach (var order in orders) 
            {
                if ((order.OrderName != null) || (!order.OrderName[1].ToString().Any(char.IsLetter)))
                {
                    order.OrderName = order.OrderName.Trim().Split(" ")[0];
                    order.OrderName.Replace("-", "");
                }
                else 
                {
                    orders.Remove(order);
                }
            }
            return orders;
        }
        public string ClearProductNumber(string str)
        {
            if ((str != null) || (!str[1].ToString().Any(char.IsLetter)))
            {
                str = str.Trim().Split(" ")[0];
                str.Replace("-", "");
            }
            else 
            {
                str = "Failed";
            }
            return str;
        }
        public async Task<Guid> ProductSubTypeIdForProduct(string productName) 
        {
            var types = await _productTypeService.GetAll();
            ProductTypeEntity currentType = new ProductTypeEntity();
            foreach (var type in types) {
                if (productName.Contains(type.Name))
                {
                    currentType = type;
                }
            }
            var a =  await _productSubTypeService.GetByProductTypeId(currentType.Id);
            return a[0].Id;
        }
        public async Task LoadSrcToMain()
        { 
            var orders = await _srcOrderService.GetAll();
            orders = ClearOrderNumber(orders);
            Dictionary<string, SrcOrderEntity> orderDict = new Dictionary<string, SrcOrderEntity>();
            Dictionary<string, SrcProductEntity> productDict = new Dictionary<string, SrcProductEntity>();
            var products = await _srcProductService.GetAll();
            var materials = await _srcMaterialService.GetAll();
            foreach (var order in orders)
            {
                OrderEntity entity = new OrderEntity();
                entity.Id = Guid.NewGuid();
                entity.Status = order.Status ?? "Пусто";
                entity.Manager = order.Manager ?? "Пусто";
                entity.Contragent = order.Contragent ?? "Пусто";
                entity.PaymentCurrent = order.PaymentCurrent ?? 0;
                entity.PaymentAmount = order.PaymentAmount ?? 0;
                entity.Number = order.OrderName ?? "Пусто";
                orderDict.Add(order.OrderNumber?? "Пусто", order);
                await _orderService.Add(entity);
            }
            foreach (var product in products)
            {
                var productOrder = orderDict[product.OrderNumber];
                ProductEntity entity = new ProductEntity();
                entity.Id = Guid.NewGuid();
                entity.Status = product.Status ?? "Пусто";
                entity.FactoryId = _factoryService.GetByName(product.Factory.Trim().Split(" ")[0]).Result.Id; // возможно добавить ToLower
                entity.OrderId = productOrder.Id;
                entity.ParentProductId = null;
                entity.StartDate = productOrder.SchemeDate;
                entity.EndDate = productOrder.LogisticDate;
                entity.Number =  ClearProductNumber(product.Comment);
                entity.ProductSubTypeId = await ProductSubTypeIdForProduct(product.Comment ?? "КНС");
                await _productService.Add(entity);
             }
            foreach (var material in materials) 
            {
                WorkingPeriodStageMaterialEntity entity = new WorkingPeriodStageMaterialEntity();
                entity.Id = Guid.NewGuid();
                entity.ProductId = productDict[material.ProductOrderName].Id;
                entity.GroupMaterialId = Guid.NewGuid(); // TODO НЕПОНЯТНО КАК СДЕЛАТЬ ССЫЛКУ
                entity.DateDelivery = material.PostedDate ?? material.ForAdmissionDate ?? DateOnly.Parse("01.01.2049");
            }

        }
    }
}
