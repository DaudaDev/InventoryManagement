using CSharpFunctionalExtensions;

namespace Blocks.Shared.Infrastructure;

public interface IGeneralRepository<TEntity>
{
    Task<Result<IEnumerable<TEntity>>> GetAllEntities();
    Task<Result> SaveEntity(TEntity entity);
    Task<Result> DeleteEntity(string entityId);
    Task<Result<TEntity>> GetEntityById(string entityId);
}