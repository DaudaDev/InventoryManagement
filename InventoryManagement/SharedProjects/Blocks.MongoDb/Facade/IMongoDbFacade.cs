using CSharpFunctionalExtensions;

namespace Blocks.MongoDb.Facade;

public interface IMongoDbFacade<TEntity>
{
    Task<Result<List<TEntity>>> GetAllEntities(string collectionId);
    
    Task<Result> SaveEntity(string collectionId, TEntity entity);

    Task<Result> DeleteEntity(string collectionId, string entityId);

    Task<Result<TEntity>> GetEntityById(string collectionId, string entityId);
    
}