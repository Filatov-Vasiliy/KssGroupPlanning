using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.EntityInterfaces;
using KssGroupPlanning.Services.EntityServices;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
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
            foreach (var type in types) {
                if (productName.Contains(type.Name))
                {
                    var seektype = await _productSubTypeService.GetByProductTypeId(type.Id);
                    return seektype[0].Id;
                }
            }
            return Guid.Parse("0ea947d9-3af7-40dd-83ae-35cc07c5a068");
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
                var orderNew = await _orderService.GetByNumber(entity.Number);
                order.Id = orderNew.Id;
                orderDict.Add(order.OrderNumber ?? "Пусто", order);

            }
            _logger.LogWarning($"Началась обработка Продуктов");

            foreach (var product in products)
            {
                if (product.OrderNumber == "")
                {
                    _logger.LogWarning($" ГОВНИЩЕ {JsonSerializer.Serialize(product)}");
                    continue;
                }
                var productOrder = orderDict[product.OrderNumber];

                ProductEntity entity = new ProductEntity();
                entity.Id = Guid.NewGuid();
                entity.Status = product.Status ?? "Пусто";
                var factory = await _factoryService.GetByName(product.Factory.Trim().Split(" ")[0]);
                Guid factoryId = Guid.Parse("019c765b-e572-7a27-84ca-277461509d9f");
                if (factory != null)
                {
                    factoryId = factory.Id;
                }
                entity.FactoryId = factoryId; // возможно добавить ToLower
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
                    _logger.LogWarning($"Апдейтим Текущий Продукт: {JsonSerializer.Serialize(entity)}");
                    _logger.LogWarning($"Апдейтим Текущий Продукт: {JsonSerializer.Serialize(entity)}");
                    await _productService.Update(entity);
                }
                else
                {
                    _logger.LogWarning($"Инсертим Текущий Продукт: {JsonSerializer.Serialize(entity)}");
                    _logger.LogWarning($"Апдейтим Текущий ордер: {JsonSerializer.Serialize(orderDict[product.OrderNumber])}");

                    await _productService.Add(entity);
                }
                
                try
                {
                    productDict.Add(product.ProductOrderName, product);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Ключ {entity.Number} уже существует.");
                    continue;
                }
                try
                {
                    productTrueDict.Add(product.ProductOrderName, entity);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Ключ {entity.Number} уже существует.");
                    continue;
                }
             }
            List<WorkingPeriodStageMaterialEntity> materialsFinish   = new List<WorkingPeriodStageMaterialEntity>();
            _logger.LogWarning($"Началась обработка материалов");
            _logger.LogWarning($"ne true {productDict["ПКНФ-001204"]}");
            _logger.LogWarning($"true {productTrueDict["ПКНФ-001204"]}");

            foreach (var material in materials) 
            {
                WorkingPeriodStageMaterialEntity entity = new WorkingPeriodStageMaterialEntity();

                // Разделить по productOrderNameChild 
                // 1. Если не нулл то убрать из обработки, но найти в словаре продуктов и добавить ссылку на родителя
                // 2. Если нулл, то оставить в списке и определить материал групп, сгруппировать по материал груп и найти максимальную дату доставки. вставить запись в хуятину
                if (material.ProductOrderNameChild != "") // Реализация нахождения родительского изделия
                {
                    Guid parentProductId;
                    try
                    {
                        parentProductId = productTrueDict[material.ProductOrderNameChild].Id;
                    }
                    catch (KeyNotFoundException ex)
                    {
                        Console.WriteLine($"Ключ {material.ProductOrderNameChild} уже существует.");
                        continue;
                    }
                    ProductEntity product = new ProductEntity();
                    try
                    {
                        product = productTrueDict[material.ProductOrderName];
                    }
                    catch (KeyNotFoundException ex)
                    {
                        Console.WriteLine($"Ключ {material.ProductOrderName} уже существует.");
                        continue;
                    }
                    product.ParentProductId = parentProductId;
                    await _productService.Update(product);
                    continue;
                }
                entity.Id = Guid.NewGuid();
                entity.ProductId = productTrueDict[material.ProductOrderName].Id;
                entity.DateDelivery = material.PostedDate ?? material.ForAdmissionDate ?? DateOnly.Parse("01.01.2000");
                var groupMaterial = await _groupMaterialService.GetAll();
                Dictionary<string, Guid> groupsNew = new Dictionary<string, Guid>();
                foreach (var group in groupMaterial)
                {
                    List<string> strings = new List<string>();
                    switch (group.Name.ToLower())
                    {
                        case "черный металл":
                            groupsNew.Add("Черн", group.Id);
                            break;
                        case "нержавейка":
                            groupsNew.Add("Нержав", group.Id);
                            break;
                        case "метизы":
                            groupsNew.Add("Метиз", group.Id);
                            groupsNew.Add("Крепеж", group.Id);
                            break;
                        case "упм":
                            groupsNew.Add("УПМ", group.Id);
                            break;
                        case "насосы":
                            groupsNew.Add("Насос", group.Id);
                            break;
                        case "электрика":
                            groupsNew.Add("Электро", group.Id);
                            groupsNew.Add("Датчики", group.Id);
                            break;
                        case "шу":
                            groupsNew.Add("ШУ", group.Id);
                            groupsNew.Add("Шкафы управления", group.Id);
                            break;
                        case "оборудование":
                            groupsNew.Add("Расходомеры, манометры", group.Id);
                            groupsNew.Add("Инструмент", group.Id);
                            groupsNew.Add("Дробилки", group.Id);
                            break;
                        case "арматура":
                            groupsNew.Add("Арматур", group.Id);
                            break;
                        default:
                            groupsNew.Add("Другое", group.Id);
                            break;
                    }
                }
                _logger.LogWarning($"Группы {JsonSerializer.Serialize(groupsNew)}");
                var keys = groupsNew.Keys;
                if (keys != null)
                {
                    foreach (var key in keys)
                    {
                        if (material.MaterialGroup.ToLower().Contains(key.ToLower()))
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
                    // Если текущая дата = 2049 и новая дата 2049 или вообще нет, то оставляем 2049
                    // Если тек дата = 2049, а приход новый, то ставим новую дату
                    // Если тек дата меньше новой даты, то ставим новую дату
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
