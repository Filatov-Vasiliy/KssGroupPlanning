
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.EntityInterfaces;

namespace KssGroupPlanning.Services.EntityServices;

public class WorkingPeriodService
{
    private readonly IWorkingPeriodRepository _workingPeriodRepository;
    public WorkingPeriodService(IWorkingPeriodRepository workingPeriodRepository)
    {
        _workingPeriodRepository = workingPeriodRepository;
    }
    public async Task<List<WorkingPeriodEntity>> GetAll()
    {
        return await _workingPeriodRepository.GetAll();
    }

    public async Task<WorkingPeriodEntity?> GetById(Guid id)
    {
        return await _workingPeriodRepository.GetById(id);
    }
    public async Task<WorkingPeriodEntity?> GetByName(string name)
    {
        return await _workingPeriodRepository.GetByName(name);
    }
    public async Task<WorkingPeriodEntity?> GetByProductId(Guid productId)
    {
        return await _workingPeriodRepository.GetByProductId(productId);
    }
    public async Task Add(WorkingPeriodEntity workingPeriod)
    {
        var workingPeriodEntity = new WorkingPeriodEntity
        {
            Id = Guid.NewGuid(),
            Name = workingPeriod.Name,
            Status = workingPeriod.Status,
            ProductId = workingPeriod.ProductId,
            DateFrom = workingPeriod.DateFrom,
            DateTo = workingPeriod.DateTo,
            CreateTime = DateTime.Now,
            UpdateTime = DateTime.Now,
        };
        await _workingPeriodRepository.Add(workingPeriodEntity);
    }
    public async Task Update(WorkingPeriodEntity workingPeriod)
    {
        await _workingPeriodRepository.Update(workingPeriod);
    }
    public async Task Delete(Guid id)
    {
        await _workingPeriodRepository.Delete(id);
    }

}
