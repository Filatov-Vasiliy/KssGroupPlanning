using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository;

public interface IStageRepository
{
    Task<List<Stage>> GetAll();
    Task<Stage?> GetById(Guid id);
    Task<Stage?> GetByProductId(Guid id);
    Task Add(Stage stage);
    Task Update(Stage stage);
    Task Delete(Guid id);
}