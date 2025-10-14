using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Interfaces;
using KssGroupPlanning.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Services;

public class NewStageService
{
    private readonly INewStageRepository _StageRepository;
    public NewStageService(INewStageRepository StageRepository)
    {
        _StageRepository = StageRepository;
    }
    public async Task<List<StageEntity>> GetAll()
    {
        return await _StageRepository.GetAll();
    }

    public async Task<StageEntity?> GetById(Guid id)
    {
        return await _StageRepository.GetById(id);
    }
    public async Task<StageEntity?> GetByProductId(Guid productId)
    {
        return await _StageRepository.GetByProductId(productId);
    }
    public async Task Add(StageEntity stage)
    {
        var StageEntity = new StageEntity
        {
            Id = Guid.NewGuid(),
            Name = stage.Name,
            ProductId = stage.ProductId,
            Status = stage.Status,
            CreateTime = DateTime.Now,
            UpdateTime = DateTime.Now,
            Date = stage.Date,
        };
        await _StageRepository.Add(StageEntity);
    }
    public async Task Update(StageEntity Stage)
    {
        await _StageRepository.Update(Stage);
    }
    public async Task Delete(Guid id)
    {
        await _StageRepository.Delete(id);
    }

}
