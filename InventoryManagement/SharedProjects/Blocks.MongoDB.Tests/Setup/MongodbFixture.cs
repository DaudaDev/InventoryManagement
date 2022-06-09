using Mongo2Go;
using MongoDB.Driver;

namespace Blocks.MongoDB.Tests.Setup;

public abstract class MongodbFixture : IDisposable
{
    protected MongoDbRunner Runner;
    protected IMongoCollection<TestDocument> MongoCollection;
    protected MongodbFixture(string collection, string testDatabase)
    {
        var collection1 = collection;
        Runner = MongoDbRunner.Start();
        MongoClient client = new(Runner.ConnectionString);
        var database = client.GetDatabase(testDatabase);
        database.CreateCollection(collection1);
        MongoCollection = database.GetCollection<TestDocument>(collection1);
    }

    public void Dispose()
    {
        Runner.Dispose();
    }
}

