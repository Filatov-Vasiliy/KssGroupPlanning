using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Models.Help;
using Microsoft.EntityFrameworkCore;

namespace KssGroupPlanning.Repositories;

public class StageRepository : IStageRepository
{
	private readonly ProjectDbContext _dbcontext;

	public StageRepository(ProjectDbContext context)
	{
		_dbcontext = context;
	}
	public async Task<List<Stage>> GetAll()
	{
		var stageEntities = await _dbcontext.Stage.AsNoTracking().OrderBy(c => c.Name).ToListAsync();
		List<Stage> stages = new List<Stage>();
		foreach (var stageEntity in stageEntities)
		{
			stages.Add(Stage.Create(stageEntity.Id, stageEntity.Name, stageEntity.ProductId, stageEntity.Status, stageEntity.Date, stageEntity.CreateTime, stageEntity.UpdateTime));
		}
		return stages;
	}

	public async Task<Stage?> GetById(Guid id)
	{

		var stageEntity = await _dbcontext.Stage.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
		return Stage.Create(stageEntity.Id, stageEntity.Name, stageEntity.ProductId, stageEntity.Status, stageEntity.Date, stageEntity.CreateTime, stageEntity.UpdateTime);
	}
	public async Task<Stage?> GetByProductId(Guid productId)
	{
		var stageEntity = await _dbcontext.Stage.AsNoTracking().FirstOrDefaultAsync(c => c.ProductId == productId);
		return Stage.Create(stageEntity.Id, stageEntity.Name,stageEntity.ProductId,stageEntity.Status, stageEntity.Date, stageEntity.CreateTime,stageEntity.UpdateTime);
	}
	
	public async Task Add(Stage stage)
	{
		var stageEntity = new StageEntity
		{
			Id = stage.Id,
			Name = stage.Name,
			ProductId = stage.ProductId,
			Status = stage.Status,
			Date = stage.Date,
			CreateTime = stage.CreateTime,
			UpdateTime = stage.UpdateTime
		};
		await _dbcontext.AddAsync(stageEntity);
		await _dbcontext.SaveChangesAsync();
	}
	public async Task Update(Stage stage)
	{
		var StageEntity = await _dbcontext.Stage.FirstOrDefaultAsync(s => s.Id == stage.Id)
			?? throw new Exception();

		StageEntity.Name = stage.Name;
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