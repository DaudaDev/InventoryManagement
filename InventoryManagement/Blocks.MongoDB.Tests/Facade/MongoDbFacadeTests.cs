using Blocks.MongoDb.Databases;
using Blocks.MongoDb.Facade;
using Blocks.MongoDB.Tests.Setup;
using CSharpFunctionalExtensions;
using FluentAssertions;
using MongoDB.Driver;
using NSubstitute;

namespace Blocks.MongoDB.Tests.Facade;

public class MongoDbFacadeTests : MongodbFixture
{
    private const string TestCollection = "InventoryItems";
    private const string TestDatabase = "TestDatabase";

    public MongoDbFacadeTests() : base(TestCollection, TestDatabase)
    {
    }

    private readonly TestDocument _testDocument1 = TestDocument.DummyData1();
    private readonly TestDocument _testDocument2 = TestDocument.DummyData2();

    private Result<IMongoCollection<TestDocument>> failureResult =
        Result.Failure<IMongoCollection<TestDocument>>("Failed");

    #region GetAllEntities

    [Fact]
    public async Task GetAllEntities_GetCollection_Failes_Return_Error()
    {
        var (sut, mongodbDatabase) = Setup();
        mongodbDatabase.GetCollection<TestDocument>(Arg.Any<string>()).Returns(failureResult);

        var result = await sut.GetAllEntities(TestCollection);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeEquivalentTo(failureResult.Error);
    }

    [Fact]
    public async Task GetAllEntities_Should_Return_All_Documents()
    {
        var (sut, _) = Setup();

        var result = await sut.GetAllEntities(TestCollection);

        result.IsSuccess.Should().BeTrue();

        var items = result.Value.ToList();
        items.Count
            .Should()
            .Be(2);

        items.ElementAt(0)
            .Should()
            .BeEquivalentTo(_testDocument1);

        items.ElementAt(1)
            .Should()
            .BeEquivalentTo(_testDocument2);
    }

    #endregion

    #region GetEntityById

    [Fact]
    public async Task GetEntityById_GetCollection_Failes_Return_Error()
    {
        var (sut, mongodbDatabase) = Setup();
        mongodbDatabase.GetCollection<TestDocument>(Arg.Any<string>()).Returns(failureResult);

        var result = await sut.GetAllEntities(TestCollection);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeEquivalentTo(failureResult.Error);
    }

    [Fact]
    public async Task GetEntityById_Should_Return_Expected_Document()
    {
        var (sut, _) = Setup();

        var result = await sut
            .GetEntityById(TestCollection, _testDocument1.Id);

        result.IsSuccess.Should().BeTrue();

        result.Value.Should().BeEquivalentTo(_testDocument1);
    }

    #endregion

    #region DeleteEntity

    [Fact]
    public async Task DeleteEntity_GetCollection_Failes_Return_Error()
    {
        var (sut, mongodbDatabase) = Setup();
        mongodbDatabase.GetCollection<TestDocument>(Arg.Any<string>()).Returns(failureResult);

        var result = await sut.DeleteEntity(TestCollection, _testDocument1.Id);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeEquivalentTo(failureResult.Error);
    }

    [Fact]
    public async Task DeleteEntity_Should_Delete_Document()
    {
        var (sut, _) = Setup();

        var initialDocuments = await MongoCollection
            .FindAsync(Builders<TestDocument>.Filter.Empty);

        initialDocuments.ToList().Count.Should().Be(2);
        
        var result = await sut
            .DeleteEntity(TestCollection, _testDocument1.Id);

        result.IsSuccess.Should().BeTrue();

        var currentDocuments = await MongoCollection
            .FindAsync(Builders<TestDocument>.Filter.Empty);
        currentDocuments.ToList().Count.Should().Be(1);

    }

    #endregion

    #region GetEntityById

    [Fact]
    public async Task SaveEntity_GetCollection_Failes_Return_Error()
    {
        var (sut, mongodbDatabase) = Setup();
        mongodbDatabase.GetCollection<TestDocument>(Arg.Any<string>()).Returns(failureResult);
       
        var result = await sut.SaveEntity(TestCollection, _testDocument1);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeEquivalentTo(failureResult.Error);
    }

    [Fact]
    public async Task SaveEntity_Should_Return_Expected_Document()
    {
        var (sut, _) = Setup();
        
        var stringTest = Guid.NewGuid().ToString();
        _testDocument1.StringTest = stringTest;
        
        var result = await sut
            .SaveEntity(TestCollection, _testDocument1);

        result.IsSuccess.Should().BeTrue();
        
        var updatedDocument = await MongoCollection
            .FindAsync(entity => entity.Id.Equals(_testDocument1.Id));

        updatedDocument.FirstOrDefault().StringTest.Should().Be(stringTest);
    }

    #endregion

    private (IMongoDbFacade<TestDocument> sut,
        IMongodbDatabase mongodbDatabase) Setup()
    {
        MongoCollection.InsertMany(new[] { _testDocument1, _testDocument2 });

        var mongodbDatabase = Substitute.For<IMongodbDatabase>();
        mongodbDatabase.GetCollection<TestDocument>(Arg.Any<string>()).Returns(Result.Success(MongoCollection));

        return (new MongoDbFacade<TestDocument>(mongodbDatabase), mongodbDatabase);
    }
}