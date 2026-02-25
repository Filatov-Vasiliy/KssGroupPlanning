using System;
using System.Xml.Linq;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Infrastuction.Db;
using KssGroupPlanning.Interfaces.EntityInterfaces;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KssGroupPlanning.Repositories;

public class BrigadeRepository : IBrigadeRepository
{
    private readonly ProjectDbContext _dbContext;
    private readonly ILogger<BrigadeRepository> _logger;

    public BrigadeRepository(ProjectDbContext context, ILogger<BrigadeRepository> logger)
    {
        _dbContext = context;
        _logger = logger;
    }
    public async Task<List<BrigadeEntity>> GetAll()
    {
        try
        {
            _logger.LogDebug("Получение всех бригад");
            var brigades = await _dbContext.Brigade
                .AsNoTracking()
                .OrderBy(c => c.CountEmployee)
                .ToListAsync();
            
            _logger.LogInformation("Успешно получено {BrigadeCount} бригад", brigades.Count);
            return brigades;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении всех бригад");
            throw;
        }
    }

    public async Task<List<BrigadeEntity?>> GetByFactoryId(Guid factoryId)
    {
        try
        {
            _logger.LogDebug("Поиск бригад по FactoryId {FactoryId}", factoryId);
            
            if (factoryId == Guid.Empty)
            {
                _logger.LogWarning("GetByFactoryId: передан пустой ID фабрики");
                return new List<BrigadeEntity?>();
            }

            var brigades = await _dbContext.Brigade
                .AsNoTracking()
                .Where(c => c.FactoryId == factoryId)
                .OrderBy(c => c.CountEmployee)
                .ToListAsync();
            
            _logger.LogInformation("Найдено {BrigadeCount} бригад для фабрики {FactoryId}", brigades.Count, factoryId);
            return brigades;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при поиске бригад по FactoryId {FactoryId}", factoryId);
            throw;
        }
    }


    public async Task<List<BrigadeEntity?>> GetByName(string name)
    {
        try
        {
            _logger.LogDebug("Поиск бригад по названию {Name}", name);
            
            if (string.IsNullOrWhiteSpace(name))
            {
                _logger.LogWarning("GetByName: передано пустое название");
                return new List<BrigadeEntity?>();
            }

            var brigades = await _dbContext.Brigade
                .AsNoTracking()
                .Where(c => c.Name == name)
                .OrderBy(c => c.CountEmployee)
                .ToListAsync();
            
            _logger.LogInformation("Найдено {BrigadeCount} бригад с названием {Name}", brigades.Count, name);
            return brigades;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при поиске бригад по названию {Name}", name);
            throw;
        }
    }

    public async Task<List<BrigadeEntity?>> GetByStageTypeId(Guid stageTypeId)
    {
        try
        {
            _logger.LogDebug("Поиск бригад по StageTypeId {StageTypeId}", stageTypeId);
            
            if (stageTypeId == Guid.Empty)
            {
                _logger.LogWarning("GetByStageTypeId: передан пустой ID типа этапа");
                return new List<BrigadeEntity?>();
            }

            var brigades = await _dbContext.Brigade
                .AsNoTracking()
                .Where(c => c.StageTypeId == stageTypeId)
                .OrderBy(c => c.CountEmployee)
                .ToListAsync();
            
            _logger.LogInformation("Найдено {BrigadeCount} бригад для типа этапа {StageTypeId}", brigades.Count, stageTypeId);
            return brigades;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при поиске бригад по StageTypeId {StageTypeId}", stageTypeId);
            throw;
        }
    }

    public async Task<BrigadeEntity?> GetById(Guid id)
    {
        try
        {
            _logger.LogDebug("Поиск бригады по ID {BrigadeId}", id);
            
            if (id == Guid.Empty)
            {
                _logger.LogWarning("GetById: передан пустой ID бригады");
                return null;
            }

            var brigade = await _dbContext.Brigade
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
            
            if (brigade == null)
            {
                _logger.LogWarning("Бригада с ID {BrigadeId} не найдена", id);
            }
            else
            {
                _logger.LogDebug("Бригада {BrigadeId} найдена: {BrigadeName}", id, brigade.Name);
            }
            
            return brigade;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при поиске бригады по ID {BrigadeId}", id);
            throw;
        }
    }





    public async Task Add(BrigadeEntity brigade)
    {
        try
        {
            if (brigade == null)
            {
                _logger.LogError("Add: попытка добавить null бригаду");
                throw new ArgumentNullException(nameof(brigade));
            }

            _logger.LogInformation("Добавление новой бригады {BrigadeName}", brigade.Name);
            
            await _dbContext.AddAsync(brigade);
            await _dbContext.SaveChangesAsync();
            
            _logger.LogInformation("Бригада {BrigadeName} успешно добавлена с ID {BrigadeId}", brigade.Name, brigade.Id);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Ошибка БД при добавлении бригады {BrigadeName}", brigade?.Name);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при добавлении бригады {BrigadeName}", brigade?.Name);
            throw;
        }
    }
    public async Task Update(BrigadeEntity brigade)
    {
        try
        {
            if (brigade == null)
            {
                _logger.LogError("Update: попытка обновить null бригаду");
                throw new ArgumentNullException(nameof(brigade));
            }

            if (brigade.Id == Guid.Empty)
            {
                _logger.LogError("Update: попытка обновить бригаду с пустым ID");
                throw new ArgumentException("ID бригады не может быть пустым", nameof(brigade.Id));
            }

            _logger.LogInformation("Обновление бригады {BrigadeId} ({BrigadeName})", brigade.Id, brigade.Name);

            var brigadeEntity = await _dbContext.Brigade.FirstOrDefaultAsync(c => c.Id == brigade.Id);

            if (brigadeEntity == null)
            {
                _logger.LogWarning("Бригада с ID {BrigadeId} не найдена для обновления", brigade.Id);
                throw new InvalidOperationException($"Бригада с ID {brigade.Id} не найдена");
            }

            brigadeEntity.Name = brigade.Name;
            brigadeEntity.FactoryId = brigade.FactoryId;
            brigadeEntity.StageTypeId = brigade.StageTypeId;
            brigadeEntity.CountEmployee = brigade.CountEmployee;

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Бригада {BrigadeId} ({BrigadeName}) успешно обновлена", brigade.Id, brigade.Name);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Ошибка БД при обновлении бригады {BrigadeId}", brigade?.Id);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении бригады {BrigadeId}", brigade?.Id);
            throw;
        }
    }
    public async Task Delete(Guid id)
    {
        try
        {
            if (id == Guid.Empty)
            {
                _logger.LogError("Delete: попытка удалить бригаду с пустым ID");
                throw new ArgumentException("ID бригады не может быть пустым", nameof(id));
            }

            _logger.LogInformation("Удаление бригады {BrigadeId}", id);

            var brigade = await _dbContext.Brigade.FirstOrDefaultAsync(c => c.Id == id);

            if (brigade == null)
            {
                _logger.LogWarning("Бригада с ID {BrigadeId} не найдена для удаления", id);
                throw new InvalidOperationException($"Бригада с ID {id} не найдена");
            }

            await _dbContext.Brigade
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Бригада {BrigadeId} ({BrigadeName}) успешно удалена", id, brigade.Name);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Ошибка БД при удалении бригады {BrigadeId}", id);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении бригады {BrigadeId}", id);
            throw;
        }
    }
}