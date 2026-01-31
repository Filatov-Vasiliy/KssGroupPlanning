
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.EntityInterfaces;

namespace KssGroupPlanning.Services.EntityServices;

public class WorkingPeriodStageBrigadeRelationService
{
    private readonly IWorkingPeriodStageBrigadeRelationRepository _workingPeriodStageBrigadeRelationRepository;
    public WorkingPeriodStageBrigadeRelationService(IWorkingPeriodStageBrigadeRelationRepository workingPeriodStageBrigadeRelationRepository)
    {
        _workingPeriodStageBrigadeRelationRepository = workingPeriodStageBrigadeRelationRepository;
    }
    public async Task<List<WorkingPeriodStageBrigadeRelationEntity>> GetAll()
    {
        return await _workingPeriodStageBrigadeRelationRepository.GetAll();
    }

    public async Task<WorkingPeriodStageBrigadeRelationEntity?> GetById(Guid id)
    {
        return await _workingPeriodStageBrigadeRelationRepository.GetById(id);
    }
    public async Task<List<WorkingPeriodStageBrigadeRelationEntity?>> GetByWorkingPeriodStageId(Guid workingPeriodStageId)
    {
        return await _workingPeriodStageBrigadeRelationRepository.GetByWorkingPeriodStageId(workingPeriodStageId);
    }
    public async Task<List<WorkingPeriodStageBrigadeRelationEntity?>> GetByBrigadeId(Guid BrigadeId)
    {
        return await _workingPeriodStageBrigadeRelationRepository.GetByBrigadeId(BrigadeId);
    }
    public async Task Add(WorkingPeriodStageBrigadeRelationEntity workingPeriodStageBrigadeRelation)
    {
        var workingPeriodStageBrigadeRelationEntity = new WorkingPeriodStageBrigadeRelationEntity
        {
            Id = Guid.NewGuid(),
            WorkingPeriodStageId = workingPeriodStageBrigadeRelation.WorkingPeriodStageId,
            BrigadeId = workingPeriodStageBrigadeRelation.BrigadeId,
        };
        await _workingPeriodStageBrigadeRelationRepository.Add(workingPeriodStageBrigadeRelationEntity);
    }
    public async Task Update(WorkingPeriodStageBrigadeRelationEntity workingPeriodStageBrigadeRelation)
    {
        await _workingPeriodStageBrigadeRelationRepository.Update(workingPeriodStageBrigadeRelation);
    }
    public async Task Delete(Guid id)
    {
        await _workingPeriodStageBrigadeRelationRepository.Delete(id);
    }

}
