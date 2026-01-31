
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.EntityInterfaces;

namespace KssGroupPlanning.Services.EntityServices;

public class WorkingPeriodRelationService
{
    private readonly IWorkingPeriodRelationRepository _workingPeriodRelationRepository;
    public WorkingPeriodRelationService(IWorkingPeriodRelationRepository workingPeriodRelationRepository)
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
