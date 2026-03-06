
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.EntityInterfaces;
using System.Text.Json;

namespace KssGroupPlanning.Services.EntityServices;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<OrderService> _logger;
    public OrderService(IOrderRepository orderRepository, ILogger<OrderService> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }
    public async Task<List<OrderEntity>> GetAll()
    {
        return await _orderRepository.GetAll();
    }
    public async Task<List<OrderEntity>> GetAllDetailed()
    {
        return await _orderRepository.GetAllDetailed();
    }


    public async Task<OrderEntity?> GetById(Guid id)
    {
        return await _orderRepository.GetById(id);
    }
    public async Task<OrderEntity?> GetByNumber(string number)
    {
        return await _orderRepository.GetByNumber(number);
    }
    public async Task Add(OrderEntity order)
    {
        var orderEntity = new OrderEntity
        {
            //Id = Guid.NewGuid(),
            Id  = order.Id,
            Number = order.Number,
            Manager = order.Manager,
            Contragent = order.Contragent,
            PaymentAmount = order.PaymentAmount,
            PaymentCurrent = order.PaymentCurrent,
            Status = order.Status,
            CreateTime = DateTime.Now,
            UpdateTime = DateTime.Now,
        };
        await _orderRepository.Add(orderEntity);
    }
    public async Task Update(OrderEntity order)
    {
        await _orderRepository.Update(order);
    }
    public async Task Delete(Guid id)
    {
        await _orderRepository.Delete(id);
    }

}
