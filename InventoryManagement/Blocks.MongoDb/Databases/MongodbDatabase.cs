using CSharpFunctionalExtensions;
using MongoDB.Driver;

namespace Blocks.MongoDb.Databases;

public class MongodbDatabase : IMongodbDatabase
{
    private readonly string _connectionString;
    private readonly string _databaseName;

    public MongodbDatabase(string connectionString, string databaseName)
    {
        _connectionString = connectionString;
        _databaseName = databaseName;
    }

    public async Task<Result<IMongoCollection<TCollection>>> GetCollection<TCollection>(string collection)
    {
        MongoClient dbClient = new(_connectionString);

        var databaseNames = await dbClient.ListDatabaseNamesAsync();

        if (databaseNames.ToEnumerable().All(name => name != _databaseName))
        {
            return Result.Failure<IMongoCollection<TCollection>>($"Database {_databaseName} does not exist");
        }
        
        var database = dbClient.GetDatabase(_databaseName);
        
        var collections = await database.ListCollectionNamesAsync();
        
        return collections.ToEnumerable().Contains(collection)
            ? Result.Success(database.GetCollection<TCollection>(collection))
            : Result.Failure<IMongoCollection<TCollection>>($"Collection {collection} does not exist");
    }
}