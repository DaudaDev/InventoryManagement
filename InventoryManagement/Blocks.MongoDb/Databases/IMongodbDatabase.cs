using CSharpFunctionalExtensions;
using MongoDB.Driver;

namespace Blocks.MongoDb.Databases;

public interface IMongodbDatabase
{
    Task<Result<IMongoCollection<TCollection>>> GetCollection<TCollection>(string collection);
    
}