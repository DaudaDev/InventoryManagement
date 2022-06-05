using Blocks.MongoDb.Databases;
using Blocks.MongoDb.Models;
using CSharpFunctionalExtensions;
using MongoDB.Driver;

namespace Blocks.MongoDb.Facade;

public class MongoDbFacade<TEntity> : IMongoDbFacade<TEntity> where TEntity : MongoDbBaseDocument
{
    private readonly IMongodbDatabase _mongodb;

    public MongoDbFacade(IMongodbDatabase mongodb)
    {
        _mongodb = mongodb;
    }

    public async Task<Result<IEnumerable<TEntity>>> GetAllEntities(string collectionId)
    {
        var collection = await _mongodb.GetCollection<TEntity>(collectionId);

        if (collection.IsFailure)
        {
            return Result.Failure<IEnumerable<TEntity>>(collection.Error);
        }
        
        var results = await collection.Value.FindAsync<TEntity>(Builders<TEntity>.Filter.Empty);

        return Result.Success(results.ToEnumerable());
    }

    public async Task<Result> SaveEntity(string collectionId, TEntity entity)
    {
        var collection = await _mongodb.GetCollection<TEntity>(collectionId);

        if (collection.IsFailure)
        {
            return collection;
        } 
        
        var result = await collection.Value.ReplaceOneAsync(
            document => document.Id.Equals(entity.Id),
            entity,
            new ReplaceOptions
            {
                IsUpsert = true
            });

        return result.IsAcknowledged switch
        {
            true => Result.Success(),
            _ => Result.Failure($"Entity not saved")
        };
    }

    public async Task<Result> DeleteEntity(string collectionId, string entityId)
    {
        var collection = await _mongodb.GetCollection<TEntity>(collectionId);

        if (collection.IsFailure)
        {
            return collection;
        }
        
        var result = await collection.Value
            .DeleteOneAsync(entity => entity.Id.Equals(entityId));

        return result.IsAcknowledged switch
        {
            true => Result.Success(),
            _ => Result.Failure("Entity not deleted")
        };
    }

    public async Task<Result<TEntity>> GetEntityById(string collectionId, string entityId)
    {
        var collection = await _mongodb.GetCollection<TEntity>(collectionId);

        if (collection.IsFailure)
        {
            return Result.Failure<TEntity>(collection.Error);
        }
        
        var results = await collection.Value
            .FindAsync(entity => entity.Id.Equals(entityId));

        return results.FirstOrDefault();
    }
}