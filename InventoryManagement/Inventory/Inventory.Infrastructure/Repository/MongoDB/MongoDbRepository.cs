using Blocks.MongoDb.Facade;
using Blocks.Shared.Infrastructure;
using CSharpFunctionalExtensions;
using Inventory.Core;
using Microsoft.Extensions.Options;

namespace Inventory.Infrastructure.Repository.MongoDB;

public class MongoDbRepository : IGeneralRepository<InventoryItem>
{
    private readonly IMongoDbFacade<InventoryItem> _mongoDbFacade;
    private readonly InventoryRepositoryOptions _repositoryOptions;

    public MongoDbRepository(IMongoDbFacade<InventoryItem> mongoDbFacade,
        IOptions<InventoryRepositoryOptions> repositoryOptions)
    {
        _mongoDbFacade = mongoDbFacade;
        _repositoryOptions = repositoryOptions.Value;
    }

    public async Task<Result<IEnumerable<InventoryItem>>> GetAllEntities()
    {
        return await _mongoDbFacade.GetAllEntities(_repositoryOptions.CollectionName);
    }

    public async Task<Result> SaveEntity(InventoryItem inventoryItem)
    {
        return await _mongoDbFacade.SaveEntity(_repositoryOptions.CollectionName, inventoryItem);
    }

    public async Task<Result> DeleteEntity(string inventoryItemId)
    {
        return await _mongoDbFacade.DeleteEntity(_repositoryOptions.CollectionName, inventoryItemId);
    }

    public async Task<Result<InventoryItem>> GetEntityById(string inventoryItemId)
    {
        return await _mongoDbFacade.GetEntityById(_repositoryOptions.CollectionName, inventoryItemId);
    }
}