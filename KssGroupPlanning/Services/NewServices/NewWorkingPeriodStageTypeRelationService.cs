using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Interfaces;
using KssGroupPlanning.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Services;

public class NewWorkingPeriodStageTypeRelationService
{
    private readonly INewWorkingPeriodStageTypeRelationRepository _workingPeriodStageTypeRelationRepository;
    public NewWorkingPeriodStageTypeRelationService(INewWorkingPeriodStageTypeRelationRepository workingPeriodStageTypeRelationRepository)
    {
        _workingPeriodStageTypeRelationRepository = workingPeriodStageTypeRelationRepository;
    }
    public async Task<List<WorkingPeriodStageTypeRelationEntity>> GetAll()
    {
        return await _workingPeriodStageTypeRelationRepository.GetAll();
    }

    public async Task<WorkingPeriodStageTypeRelationEntity> GetById(Guid id)
    {
        return await _workingPeriodStageTypeRelationRepository.GetById(id);
    }
    public async Task<List<WorkingPeriodStageTypeRelationEntity?>> GetByProductSubTypeWorkingPeriodSampleId(Guid productSubTypeWorkingPeriodSampleId)
    {
        return await _workingPeriodStageTypeRelationRepository.GetByProductSubTypeWorkingPeriodSampleId(productSubTypeWorkingPeriodSampleId);
    }
    public async Task<List<WorkingPeriodStageTypeRelationEntity?>> GetByStageTypeId(Guid stageTypeId)
    {
        return await _workingPeriodStageTypeRelationRepository.GetByStageTypeId(stageTypeId);
    }
    public async Task Add(WorkingPeriodStageTypeRelationEntity workingPeriodStageTypeRelation)
    {
        var workingPeriodStageTypeRelationEntity = new WorkingPeriodStageTypeRelationEntity
        {
            Id = Guid.NewGuid(),
            StageTypeId = workingPeriodStageTypeRelation.StageTypeId,
            ProductSubTypeWorkingPeriodSampleId = workingPeriodStageTypeRelation.ProductSubTypeWorkingPeriodSampleId,
        };
        await _workingPeriodStageTypeRelationRepository.Add(workingPeriodStageTypeRelationEntity);
    }
    public async Task Update(WorkingPeriodStageTypeRelationEntity workingPeriodStageTypeRelation)
    {
        await _workingPeriodStageTypeRelationRepository.Update(workingPeriodStageTypeRelation);
    }
    public async Task Delete(Guid id)
    {
        await _workingPeriodStageTypeRelationRepository.Delete(id);
    }

}
