using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Interfaces;
using KssGroupPlanning.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Services;

public class NewWorkingPeriodStageService
{
    private readonly INewWorkingPeriodStageRepository _workingPeriodStageRepository;
    public NewWorkingPeriodStageService(INewWorkingPeriodStageRepository workingPeriodStageRepository)
    {
        _workingPeriodStageRepository = workingPeriodStageRepository;
    }
    public async Task<List<WorkingPeriodStageEntity>> GetAll()
    {
        return await _workingPeriodStageRepository.GetAll();
    }

    public async Task<WorkingPeriodStageEntity?> GetById(Guid id)
    {
        return await _workingPeriodStageRepository.GetById(id);
    }
    public async Task<List<WorkingPeriodStageEntity?>> GetByWorkingPeriodId(Guid workingPeriodId)
    {
        return await _workingPeriodStageRepository.GetByWorkingPeriodId(workingPeriodId);
    }
    public async Task<List<WorkingPeriodStageEntity?>> GetByProductSubTypeWorkingPeriodSampleId(Guid productSubTypeWorkingPeriodSampleId)
    {
        return await _workingPeriodStageRepository.GetByProductSubTypeWorkingPeriodSampleId(productSubTypeWorkingPeriodSampleId);
    }
    public async Task Add(WorkingPeriodStageEntity workingPeriodStage)
    {
        var workingPeriodStageEntity = new WorkingPeriodStageEntity
        {
            Id = Guid.NewGuid(),
            WorkingPeriodId = workingPeriodStage.Id,
            DateFrom = workingPeriodStage.DateFrom,
            DateTo = workingPeriodStage.DateTo,
            Status = workingPeriodStage.Status,
            Recycling = workingPeriodStage.Recycling,
            ProductSubTypeWorkingPeriodSampleId = workingPeriodStage.ProductSubTypeWorkingPeriodSampleId,
            CreateTime = DateTime.Now,
            UpdateTime = DateTime.Now,
        };
        await _workingPeriodStageRepository.Add(workingPeriodStageEntity);
    }
    public async Task Update(WorkingPeriodStageEntity workingPeriodStage)
    {
        await _workingPeriodStageRepository.Update(workingPeriodStage);
    }
    public async Task Delete(Guid id)
    {
        await _workingPeriodStageRepository.Delete(id);
    }

}
