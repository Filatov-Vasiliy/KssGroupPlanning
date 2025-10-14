using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Interfaces;
using KssGroupPlanning.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Services;

public class NewWorkingPeriodRelationService
{
    private readonly INewWorkingPeriodRelationRepository _workingPeriodRelationRepository;
    public NewWorkingPeriodRelationService(INewWorkingPeriodRelationRepository workingPeriodRelationRepository)
    {
        _workingPeriodRelationRepository = workingPeriodRelationRepository;
    }
    public async Task<List<WorkingPeriodRelationEntity>> GetAll()
    {
        return await _workingPeriodRelationRepository.GetAll();
    }

    public async Task<WorkingPeriodRelationEntity> GetById(Guid id)
    {
        return await _workingPeriodRelationRepository.GetById(id);
    }
    public async Task<List<WorkingPeriodRelationEntity?>> GetByParentProductSubTypeWorkingPeriodSampleId(Guid parentProductSubTypeWorkingPeriodSampleId)
    {
        return await _workingPeriodRelationRepository.GetByParentProductSubTypeWorkingPeriodSampleId(parentProductSubTypeWorkingPeriodSampleId);
    }
    public async Task<List<WorkingPeriodRelationEntity?>> GetByChildProductSubTypeWorkingPeriodSampleId(Guid childProductSubTypeWorkingPeriodSampleId)
    {
        return await _workingPeriodRelationRepository.GetByChildProductSubTypeWorkingPeriodSampleId(childProductSubTypeWorkingPeriodSampleId);
    }
    public async Task Add(WorkingPeriodRelationEntity workingPeriodRelation)
    {
        var workingPeriodRelationEntity = new WorkingPeriodRelationEntity
        {
            Id = Guid.NewGuid(),
            ChildProductSubTypeWorkingPeriodSampleId = workingPeriodRelation.ChildProductSubTypeWorkingPeriodSampleId,
            ParentProductSubTypeWorkingPeriodSampleId = workingPeriodRelation.ParentProductSubTypeWorkingPeriodSampleId,
        };
        await _workingPeriodRelationRepository.Add(workingPeriodRelationEntity);
    }
    public async Task Update(WorkingPeriodRelationEntity workingPeriodRelation)
    {
        await _workingPeriodRelationRepository.Update(workingPeriodRelation);
    }
    public async Task Delete(Guid id)
    {
        await _workingPeriodRelationRepository.Delete(id);
    }

}
