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
        private readonly GroupMaterialService _groupMaterialService;
        private readonly ILogger<IntegrationService> _logger;
        public IntegrationService(ILogger<IntegrationService> logger, SrcOrderService srcOrderService, SrcMaterialService srcMaterialService, SrcProductService srcProductService, FactoryService factoryService, ProductSubTypeService productSubTypeService,OrderService orderService,ProductService productService, WorkingPeriodStageMaterialService workingPeriodStageMaterialService,ProductTypeService productTypeService,GroupMaterialService groupMaterialService)
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
            _groupMaterialService = groupMaterialService;
        }
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
            Dictionary<string, ProductEntity> productTrueDict = new Dictionary<string, ProductEntity>(); // либо реализовать метод накидываения родителя
            Dictionary<(Guid, Guid), DateOnly> preparedMaterials2 = new Dictionary<(Guid, Guid), DateOnly>();
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
                var orderOld = await _orderService.GetByNumber(entity.Number);
                if (orderOld != null)
                {
                    entity.Id = orderOld.Id;
                    await _orderService.Update(entity);
                }
                else
                {
                    await _orderService.Add(entity);
                }
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

                var productOld =  await _productService.GetByNumber(entity.Number);
                if (productOld != null)
                {
                    entity.Id = productOld.Id;
                    await _productService.Update(entity);
                }
                else
                {
                    await _productService.Add(entity);
                }
                productDict.Add(entity.Number, product);
                productTrueDict.Add(entity.Number, entity);
             }
            List<WorkingPeriodStageMaterialEntity> materialsFinish   = new List<WorkingPeriodStageMaterialEntity>();
            foreach (var material in materials) 
            {
                WorkingPeriodStageMaterialEntity entity = new WorkingPeriodStageMaterialEntity();

                // Разделить по productOrderNameChild 
                // 1. Если не нулл то убрать из обработки, но найти в словаре продуктов и добавить ссылку на родителя
                // 2. Если нулл, то оставить в списке и определить материал групп, сгруппировать по материал груп и найти максимальную дату доставки. вставить запись в хуятину
                if (material.ProductOrderNameChild != null) // Реализация нахождения родительского изделия
                {
                    var parentProductId = productTrueDict[material.ProductOrderNameChild].Id;
                    var product = productTrueDict[material.ProductOrderName];
                    product.ParentProductId = parentProductId;
                    await _productService.Update(product);
                    continue;
                }
                entity.Id = Guid.NewGuid();
                entity.ProductId = productDict[material.ProductOrderName].Id;
                entity.DateDelivery = material.PostedDate ?? material.ForAdmissionDate ?? DateOnly.Parse("01.01.2049");
                // Определение группы материала
                // Черный металл
                // Нержавейка 
                // Метизы
                // УПМ
                // Насосы
                // Электрика
                // Арматура?
                // Оборудование
                // Шкаф управления
                // Другое
                var groupMaterial = await _groupMaterialService.GetAll();
                Dictionary<string, Guid> groupsNew = new Dictionary<string, Guid>();
                foreach (var group in groupMaterial)
                {
                    List<string> strings = new List<string>();
                    switch (group.Name)
                    {
                        case "Черный металл":
                            groupsNew.Add("Черн", group.Id);
                            break;
                        case "Нержавейка":
                            groupsNew.Add("Нержав", group.Id);
                            break;
                        case "Метизы":
                            groupsNew.Add("Метиз", group.Id);
                            groupsNew.Add("Крепеж", group.Id);
                            break;
                        case "УПМ":
                            groupsNew.Add("УПМ", group.Id);
                            break;
                        case "Насосы":
                            groupsNew.Add("Насос", group.Id);
                            break;
                        case "Электрика":
                            groupsNew.Add("Электро", group.Id);
                            groupsNew.Add("Датчики", group.Id);
                            break;
                        case "ШУ":
                            groupsNew.Add("ШУ", group.Id);
                            groupsNew.Add("Шкафы управления", group.Id);
                            break;
                        case "Оборудование":
                            groupsNew.Add("Расходомеры, манометры", group.Id);
                            groupsNew.Add("Инструмент", group.Id);
                            groupsNew.Add("Дробилки", group.Id);
                            break;
                        case "Арматура":
                            groupsNew.Add("Арматур", group.Id);
                            break;
                        default:
                            groupsNew.Add("Другое", group.Id);
                            break;
                    }
                }
                var keys = groupsNew.Keys;
                if (keys != null)
                {
                    foreach (var key in keys)
                    {
                        if (material.MaterialGroup.Contains(key))
                        {
                            entity.GroupMaterialId = groupsNew[key];
                        }
                        else
                        {
                            entity.GroupMaterialId = groupsNew["Другое"];
                        }
                    }
                }
                else 
                {
                    var group = await _groupMaterialService.GetByName("Другое");
                    entity.GroupMaterialId = group.Id;
                }

                // Определение даты доставки
                (Guid, Guid) complexKey = (entity.ProductId, entity.GroupMaterialId); // Составной ключ для словаря
                if (preparedMaterials2.ContainsKey(complexKey))
                {
                    if (preparedMaterials2[complexKey] < entity.DateDelivery)
                    {
                        preparedMaterials2[complexKey] = entity.DateDelivery;
                    }
                }
                else 
                { 
                    preparedMaterials2.Add(complexKey, entity.DateDelivery);
                }
            }
            foreach (var material in preparedMaterials2)
            {
                WorkingPeriodStageMaterialEntity entity = new WorkingPeriodStageMaterialEntity();
                entity.ProductId = material.Key.Item1;
                entity.GroupMaterialId = material.Key.Item2;
                entity.DateDelivery = material.Value;
                entity.Id = Guid.NewGuid();
                var materialOld = await _workingPeriodStageMaterialService.GetByComplexKey(entity.ProductId, entity.GroupMaterialId);
                if (materialOld != null)
                {
                    entity.Id = materialOld.Id;
                    await _workingPeriodStageMaterialService.Update(entity);
                }
                else
                {
                    await _workingPeriodStageMaterialService.Add(entity);
                }
            }
        }
    }
}
