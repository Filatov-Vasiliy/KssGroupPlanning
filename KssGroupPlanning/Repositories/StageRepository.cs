using KssGroupPlanning.Entities;
using KssGroupPlanning.Infrastuction.Db;
using KssGroupPlanning.Interfaces.EntityInterfaces;


using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Repositories;

public class StageRepository : IStageRepository
{
	private readonly ProjectDbContext _dbcontext;

	public StageRepository(ProjectDbContext context)
	{
		_dbcontext = context;
	}
	public async Task<List<StageEntity>> GetAll()
	{
        return await _dbcontext.Stage.AsNoTracking().OrderBy(c => c.CreateTime).ToListAsync();
	}

	public async Task<StageEntity?> GetById(Guid id)
	{

        return await _dbcontext.Stage.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
	}
	public async Task<List<StageEntity?>> GetByProductId(Guid productId)
	{
        return await _dbcontext.Stage.AsNoTracking().Where(c => c.ProductId == productId).ToListAsync();
	}
    public async Task<List<StageEntity?>> GetByProductSubTypeStageSampleId(Guid productSubTypeStageSampleId)
    {
        return await _dbcontext.Stage.AsNoTracking().Where(c => c.ProductSubTypeStageSampleId == productSubTypeStageSampleId).ToListAsync();
    }

    public async Task Add(StageEntity stage)
	{
		await _dbcontext.AddAsync(stage);
		await _dbcontext.SaveChangesAsync();
	}
	public async Task Update(StageEntity stage)
	{
		var StageEntity = await _dbcontext.Stage.FirstOrDefaultAsync(s => s.Id == stage.Id)
			?? throw new Exception();

		StageEntity.ProductSubTypeStageSampleId = stage.ProductSubTypeStageSampleId;
		StageEntity.ProductId = stage.ProductId;
		StageEntity.Status = stage.Status;
		StageEntity.Date = stage.Date;
		StageEntity.CreateTime = stage.CreateTime;
		StageEntity.UpdateTime = stage.UpdateTime;

		await _dbcontext.SaveChangesAsync();
	}
	public async Task Delete(Guid id)
	{
		await _dbcontext.Stage
			.Where(s => s.Id == id)
			.ExecuteDeleteAsync();
		await _dbcontext.SaveChangesAsync();
	}
}