using Blocks.MongoDb.Databases;
using Blocks.MongoDB.Tests.Setup;
using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Blocks.MongoDB.Tests.Databases;

public class MongodbDatabaseTests : MongodbFixture
{
    private const string TestCollection = "TestCollection";
    private const string TestDatabase = "TestDatabase";

    public MongodbDatabaseTests() : base(TestCollection, TestDatabase)
    {
    }
    
    [Fact]
    public async Task GetCollection_Database_Does_Not_Exists_Returns_Error()
    {
        const string InvalidName = "InvalidName";
        MongodbDatabase sut = new(Runner.ConnectionString, InvalidName);

        var mongoCollection = await sut.GetCollection<BsonDocument>(TestCollection);
      
        mongoCollection.IsFailure.Should().BeTrue();
        mongoCollection.Error.Should().Be($"Database {InvalidName} does not exist");
    }
    
    [Fact]
    public async Task GetCollection_Collection_Does_Not_Exists_Returns_Error()
    {
        const string InvalidName = "InvalidName";
        MongodbDatabase sut = new(Runner.ConnectionString, TestDatabase);

        var mongoCollection = await sut.GetCollection<BsonDocument>(InvalidName);
      
        mongoCollection.IsFailure.Should().BeTrue();
        mongoCollection.Error.Should().Be($"Collection {InvalidName} does not exist");
    }
    
    [Fact]
    public async Task GetCollection_Connection_Exists_Returns_Valid_Database()
    {
        MongodbDatabase sut = new(Runner.ConnectionString, TestDatabase);

        var mongoCollection = await sut.GetCollection<BsonDocument>(TestCollection);
       
        mongoCollection.IsSuccess.Should().BeTrue();
        mongoCollection.Value.Should().BeAssignableTo<IMongoCollection<BsonDocument>>();
        mongoCollection.Value.Database.DatabaseNamespace.DatabaseName.Should().Be(TestDatabase);
        mongoCollection.Value.CollectionNamespace.CollectionName.Should().Be(TestCollection);
    }

    
}