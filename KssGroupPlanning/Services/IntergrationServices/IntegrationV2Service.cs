using KssGroupPlanning.Entities;
using KssGroupPlanning.Entities.Src;
using KssGroupPlanning.Entities.DQ;
using KssGroupPlanning.Interfaces.EntityInterfaces;
using KssGroupPlanning.Services.EntityServices;
using KssGroupPlanning.Services.DQServices;

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace KssGroupPlanning.Services.IntergrationServices;

public class IntegrationV2Service
{
    private readonly SrcOrderService _srcOrderService;
    private readonly SrcProductService _srcProductService;
    private readonly SrcMaterialService _srcMaterialService;

    private readonly DQSrcOrderService _dqSrcOrderService;
    private readonly DQSrcProductService _dqSrcProductService;
    private readonly DQSrcMaterialService _dqSrcMaterialService;

    private readonly OrderService _orderService;
    private readonly ProductService _productService;
    private readonly WorkingPeriodStageMaterialService _workingPeriodStageMaterialService;
    private readonly FactoryService _factoryService;
    private readonly ProductTypeService _productTypeService;
    private readonly ProductSubTypeService _productSubTypeService;
    private readonly GroupMaterialService _groupMaterialService;

    private readonly ILogger<IntegrationService> _logger;
    public IntegrationV2Service(ILogger<IntegrationService> logger, SrcOrderService srcOrderService, SrcMaterialService srcMaterialService, SrcProductService srcProductService, FactoryService factoryService, ProductSubTypeService productSubTypeService, OrderService orderService, ProductService productService, WorkingPeriodStageMaterialService workingPeriodStageMaterialService, ProductTypeService productTypeService, GroupMaterialService groupMaterialService, DQSrcMaterialService dqSrcMaterialService, DQSrcProductService dqSrcProductService, DQSrcOrderService dqSrcOrderService)
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
        _dqSrcMaterialService = dqSrcMaterialService;
        _dqSrcOrderService = dqSrcOrderService;
        _dqSrcProductService = dqSrcProductService;
    }

    public string ClearNumber(string str)
    {
        if ((str != null) && (!str[1].ToString().Any(char.IsLetter)))
        {
            str = str.Trim().Split(" ")[0];
            str = str.Split("-")[0];
        }
        else
        {
            str = "Failed";
        }
        return str;
    }
    // Функция поиска подходящего ProductType
    public async Task<Guid> ProductSubTypeIdForProduct1(string productName)
    {
        var types = await _productTypeService.GetAll();
        foreach (var type in types)
        {
            if (productName.Contains(type.Name))
            {
                var seektype = await _productSubTypeService.GetByProductTypeId(type.Id);
                return seektype[0].Id;
            }
        }
        return Guid.Parse("0ea947d9-3af7-40dd-83ae-35cc07c5a068");
    }
    public async Task<Guid> ProductSubTypeIdForProduct(List<ProductTypeEntity> types, string productName)
    {
        foreach (var type in types)
        {
            if (productName.Contains(type.Name))
            {
                var seektype = type.ProductSubTypes[0].Id;
                return seektype;
            }
        }
        return Guid.Parse("0ea947d9-3af7-40dd-83ae-35cc07c5a068");
    }
    public async Task LoadSrcToMain()
    {
        var srcOrders = await _srcOrderService.GetAll();
        var srcProducts = await _srcProductService.GetAll();
        var srcMaterials = await _srcMaterialService.GetAll();
        Dictionary<string, SrcOrderEntity> orderDict = new Dictionary<string, SrcOrderEntity>();
        Dictionary<string, SrcProductEntity> productDict = new Dictionary<string, SrcProductEntity>();

        Dictionary<string, (SrcProductEntity,SrcMaterialEntity)> productMaterialDict = new Dictionary<string, (SrcProductEntity, SrcMaterialEntity)>();
        Dictionary<string, SrcMaterialEntity> materialDict = new Dictionary<string, SrcMaterialEntity>();

        Dictionary<string, ProductEntity> productTrueDict = new Dictionary<string, ProductEntity>();

        Dictionary<(Guid, Guid), DateOnly> preparedMaterials = new Dictionary<(Guid, Guid), DateOnly>();

        foreach (var order in srcOrders)
        {
            //DQSrcOrderEntity dQSrcOrderEntity = new DQSrcOrderEntity();

            OrderEntity entity = new OrderEntity();
            var orderName = ClearNumber(order.OrderName);
            if (orderName == "Failed")
            {
                //DQ
                //dQSrcOrderEntity = dQSrcOrderEntity.SrcToDQ(order, "Failed by OrderName", false, null);
                //await _dqSrcOrderService.Add(dQSrcOrderEntity);
                //
                continue;
            }
            entity.Id = Guid.NewGuid();
            entity.Status = order.Status ?? "Пусто";
            entity.Manager = order.Manager ?? "Пусто";
            entity.Contragent = order.Contragent ?? "Пусто";
            entity.PaymentCurrent = order.PaymentCurrent ?? 0;
            entity.PaymentAmount = order.PaymentAmount ?? 0;
            entity.Number = orderName;
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
            // DQ
            //dQSrcOrderEntity = dQSrcOrderEntity.SrcToDQ(order, "", true, order.Id);
            //await _dqSrcOrderService.Add(dQSrcOrderEntity);
            // DQ
            orderDict.Add(order.OrderNumber ?? "Пусто", order);

        }
        var types = await _productTypeService.GetAllDetailed();

        // тестим джоин
        var joinedList = from srcProduct in srcProducts
                         from srcMaterial in srcMaterials
                         where (srcProduct.ProductOrderName == srcMaterial.ProductOrderNameChild || srcProduct.ProductName == srcMaterial.MaterialName)
                         select new { SrcProduct = srcProduct, SrcMaterial = srcMaterial };

        var leftJoinedList = from srcProduct in srcProducts
                             join srcMaterial in srcMaterials
                             on 1 equals 1
                             into bGroup
                             from b in bGroup.Where(x => srcProduct.ProductOrderName == x.ProductOrderNameChild || srcProduct.ProductName == x.MaterialName).DefaultIfEmpty()
                             select new { SrcProduct = srcProduct, SrcMaterial = b };

        foreach (var item in leftJoinedList)
        {
            {
                if (item.SrcMaterial != null)
                {
                    try
                    {
                        productMaterialDict.Add(item.SrcProduct.ProductOrderName, (item.SrcProduct, item.SrcMaterial));
                    }
                    catch { continue; }
                    // Есть дубли в джоине
                }
                else
                {
                    productDict.Add(item.SrcProduct.ProductOrderName, item.SrcProduct);
                }
                srcMaterials.Remove(item.SrcMaterial);
            }
        }

            foreach (var product in productDict.Values)
            {
                ProductEntity productEntity = new ProductEntity();
                productEntity.Id = Guid.NewGuid();
                try
                {
                    productEntity.OrderId = orderDict[product.OrderNumber].Id;
                }
                catch
                {
                    _logger.LogWarning($"Продукт {product.ProductOrderName} не нашел себе ордер");
                    continue;
                }
                bool isSHYP = false; // ШКАФ УПРАВЛЕНИЯ


                // Обработка Дерьмого номера
                if (product.OrderNumber == "")
                {
                    //DQ
                    //dQSrcProductEntity = dQSrcProductEntity.SrcToDQ(product, "Failed by OrderNumber", false, null);
                    // await _dqSrcProductService.Add(dQSrcProductEntity);
                    //DQ
                    continue;
                }
                var productOrder = orderDict[product.OrderNumber];
                var productNumber = ClearNumber(product.Comment);
                if ((productNumber.Contains("ШУ")) || (productNumber.Contains("Ш/У")))
                {
                    isSHYP = true;
                }
                if (productNumber == "Failed")
                {
                    //DQ
                    //dQSrcProductEntity = dQSrcProductEntity.SrcToDQ(product, "Failed by ProductOrderName", false, null);
                    //await _dqSrcProductService.Add(dQSrcProductEntity);
                    //DQ
                    continue;
                }
                productEntity.Status = product.Status ?? "Пусто";
                var factory = await _factoryService.GetByName(product.Factory.Trim().Split(" ")[0]);
                // Если нет подходящего производства накидывается неопределено
                Guid factoryId = Guid.Parse("019c765b-e572-7a27-84ca-277461509d9f");
                if (factory != null)
                {
                    factoryId = factory.Id;
                }
                productEntity.FactoryId = factoryId;
                productEntity.ParentProductId = null;
                if (isSHYP) // обработку если ШУ
                {
                    ProductEntity? parentProduct = await _productService.GetByNumber(productNumber.Split("Ш")[0]);
                    if (parentProduct is null || parentProduct.Id == Guid.Empty)
                    {
                        productEntity.ParentProduct = null;
                    }
                    else
                    {
                        productEntity.ParentProductId = parentProduct.Id;
                    }
                }
                productEntity.StartDate = productOrder.SchemeDate;
                productEntity.EndDate = productOrder.LogisticDate;
                productEntity.Number = productNumber;
                if (isSHYP) // Обработка если ШУ
                {
                    productEntity.ProductSubTypeId = await ProductSubTypeIdForProduct(types, "Ш/У");
                }
                else
                {
                    productEntity.ProductSubTypeId = await ProductSubTypeIdForProduct(types, product.Comment ?? "КНС");
                }
                if (isSHYP && product.Qty > 1)
                {
                    // Добавить обработку накидывания всех шкафов управления
                }
                var productOld = await _productService.GetByNumber(productEntity.Number);
                // В случае если изделие с таким номером уже существует - мы его обновляем, в случае если нет такого номера мы инсертим строку
                if (productOld != null)
                {
                    productEntity.Id = productOld.Id;
                    await _productService.Update(productEntity);
                }
                else
                {
                    await _productService.Add(productEntity);
                }

                try
                {
                    productTrueDict.Add(product.ProductOrderName, productEntity);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Ключ {productEntity.Number} уже существует.");
                    continue;
                }
            // DQ
            //dQSrcProductEntity = dQSrcProductEntity.SrcToDQ(product, "", true, entity.Id);
            //await _dqSrcProductService.Add(dQSrcProductEntity);
            // DQ
            // Формируем мапки с изделиями для мержа с материалами

        }
            // теперь те которые наджоинились на материалы
            foreach (var productMaterial in productMaterialDict.Values)
            {
                var product = productMaterial.Item1;
                var material = productMaterial.Item2;
                ProductEntity productEntity = new ProductEntity();
                productEntity.Id = Guid.NewGuid();
                try
                {
                    productEntity.OrderId = orderDict[product.OrderNumber].Id;
                }
                catch
                {
                    _logger.LogWarning($"Продукт {product.ProductOrderName} не нашел себе ордер");
                    continue;
                }
                bool isSHYP = false; // ШКАФ УПРАВЛЕНИЯ


                // Обработка Дерьмого номера
                if (product.OrderNumber == "")
                {
                    //DQ
                    //dQSrcProductEntity = dQSrcProductEntity.SrcToDQ(product, "Failed by OrderNumber", false, null);
                    // await _dqSrcProductService.Add(dQSrcProductEntity);
                    //DQ
                    continue;
                }
                var productOrder = orderDict[product.OrderNumber];
                var productNumber = ClearNumber(product.Comment);
                if ((productNumber.Contains("ШУ")) || (productNumber.Contains("Ш/У")))
                {
                    isSHYP = true;
                }
                if (productNumber == "Failed")
                {
                    //DQ
                    //dQSrcProductEntity = dQSrcProductEntity.SrcToDQ(product, "Failed by ProductOrderName", false, null);
                    //await _dqSrcProductService.Add(dQSrcProductEntity);
                    //DQ
                    continue;
                }
                productEntity.Status = product.Status ?? "Пусто";
                var factory = await _factoryService.GetByName(product.Factory.Trim().Split(" ")[0]);
                // Если нет подходящего производства накидывается неопределено
                Guid factoryId = Guid.Parse("019c765b-e572-7a27-84ca-277461509d9f");
                if (factory != null)
                {
                    factoryId = factory.Id;
                }
                productEntity.FactoryId = factoryId;
                productEntity.ParentProductId = null;
                if (isSHYP) // обработку если ШУ
                {
                    ProductEntity? parentProduct = await _productService.GetByNumber(productNumber.Split("Ш")[0]);
                    if (parentProduct is null || parentProduct.Id == Guid.Empty)
                    {
                        productEntity.ParentProduct = null;
                    }
                    else
                    {
                        productEntity.ParentProductId = parentProduct.Id;
                    }
                }
                if (productEntity.ParentProduct is null) 
                {
                _logger.LogCritical("тут");
                SrcProductEntity parentProduct = new SrcProductEntity();
                    try 
                    { 
                        parentProduct = productDict[material.ProductOrderName];
                }
                    catch 
                    {
                        try
                        {
                            var preparentProduct = productMaterialDict[material.ProductOrderName];
                             parentProduct = productDict[preparentProduct.Item2.ProductOrderName];
                    }
                        catch 
                        {
                        }
                    }
                    if (parentProduct is not null) 
                    { 
                        var parentId =  await _productService.GetByNumber(ClearNumber(parentProduct.Comment));
                        if (parentId is not null) 
                        { 
                            productEntity.ParentProductId = parentId.Id;
                        }
                    }
                    
                }
                productEntity.StartDate = productOrder.SchemeDate;
                productEntity.EndDate = productOrder.LogisticDate;
                var f = false;
                var i = 1;
                while (f is false) 
                {
                productEntity.Number = productNumber + "."+i.ToString();
                var entity = await _productService.GetByNumber(productEntity.Number);
                if (entity is not null)
                {
                    i++;
                    continue;
                }

                f = true;
                }
                if (isSHYP) // Обработка если ШУ
                {
                    productEntity.ProductSubTypeId = await ProductSubTypeIdForProduct(types, "Ш/У");
                }
                else
                {
                    productEntity.ProductSubTypeId = await ProductSubTypeIdForProduct(types, product.Comment ?? "КНС");
                }
                if (isSHYP && product.Qty > 1)
                {
                    // Добавить обработку накидывания всех шкафов управления
                }
                var productOld = await _productService.GetByNumber(productEntity.Number);
                // В случае если изделие с таким номером уже существует - мы его обновляем, в случае если нет такого номера мы инсертим строку
                if (productOld != null)
                {
                    productEntity.Id = productOld.Id;
                    await _productService.Update(productEntity);
                }
                else
                {
                    await _productService.Add(productEntity);
                }
                try
                {
                    productTrueDict.Add(product.ProductOrderName, productEntity);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Ключ {productEntity.Number} уже существует.");
                    continue;
                }
            // DQ
            //dQSrcProductEntity = dQSrcProductEntity.SrcToDQ(product, "", true, entity.Id);
            //await _dqSrcProductService.Add(dQSrcProductEntity);
            // DQ
            // Формируем мапки с изделиями для мержа с материалами

        }
        //materials

        // Загрузка материалов

        List<WorkingPeriodStageMaterialEntity> materialsFinish = new List<WorkingPeriodStageMaterialEntity>();

        // Сборка груп материалов по алиасам для последующего определения материала и группировки
        var groupMaterial = await _groupMaterialService.GetAll();
        Dictionary<string, Guid> groupsNew = new Dictionary<string, Guid>();

        foreach (var group in groupMaterial)
        {
            List<string> strings = new List<string>();
            switch (group.Name.ToLower())
            {
                case "металл":
                    groupsNew.Add("Черн", group.Id);
                    break;
                case "нержа":
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
                case "станция": //Сделать для этой группы
                    groupsNew.Add("Туду", group.Id);
                    break;
                case "расходные материалы": //Сделать для этой группы
                    groupsNew.Add("Тудуду", group.Id);
                    break;
                default:
                    groupsNew.Add("Другое", group.Id);
                    break;
            }
        }
        // Обработка всех материалов


        foreach (var material in srcMaterials)
        {
            //DQSrcMaterialEntity dQSrcMaterialEntity = new DQSrcMaterialEntity();
            WorkingPeriodStageMaterialEntity entity = new WorkingPeriodStageMaterialEntity();
            entity.Id = Guid.NewGuid();
            var productId = Guid.Empty;

            entity.ProductId = productTrueDict[material.ProductOrderName].Id;
            if (material.Qty <= material.CurrentQty)
            {
                entity.DateDelivery = material.CreateDate ?? DateOnly.Parse("01.01.2000");
            }
            else
            {
                entity.DateDelivery = material.PostedDate ?? material.ForAdmissionDate ?? DateOnly.Parse("01.01.2000");
            }
            var keys = groupsNew.Keys;
            if (keys != null)
            {
                foreach (var key in keys)
                {
                    if (material.MaterialGroup.ToLower().Contains(key.ToLower()))
                    {
                        entity.GroupMaterialId = groupsNew[key];
                        break;
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
            //DQ
            //dQSrcMaterialEntity = dQSrcMaterialEntity.SrcToDQ(material, "", true, entity.GroupMaterialId, entity.ProductId);
            //await _dqSrcMaterialService.Add(dQSrcMaterialEntity);
            //DQ
            // Определение даты доставки
            (Guid, Guid) complexKey = (entity.ProductId, entity.GroupMaterialId); // Составной ключ для словаря
            if (preparedMaterials.ContainsKey(complexKey))
            {
                if (preparedMaterials[complexKey] < entity.DateDelivery)
                {
                    preparedMaterials[complexKey] = entity.DateDelivery;
                }
            }
            else
            {
                preparedMaterials.Add(complexKey, entity.DateDelivery);
            }
        }
        foreach (var material in preparedMaterials)
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
