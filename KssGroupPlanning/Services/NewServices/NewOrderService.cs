using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Interfaces;
using KssGroupPlanning.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Services;

public class NewOrderService
{
    private readonly INewOrderRepository _orderRepository;
    public NewOrderService(INewOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<List<OrderEntity>> GetAll()
    {
        return await _orderRepository.GetAll();
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
            Id = Guid.NewGuid(),
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
