using AutoMapper;
using Blocks.MongoDb.Facade;
using Blocks.Shared.Contracts.Infrastructure;
using CSharpFunctionalExtensions;
using Inventory.Core.Domain;
using Inventory.Infrastructure.Models;
using Microsoft.Extensions.Options;

namespace Inventory.Infrastructure.Repository.MongoDB;

public class MongoDbRepository : IGeneralRepository<InventoryItem>
{
    private readonly IMongoDbFacade<InventoryItemDto> _mongoDbFacade;
    private readonly InventoryRepositoryOptions _repositoryOptions;
    private readonly IMapper _mapper;

    public MongoDbRepository(IMongoDbFacade<InventoryItemDto> mongoDbFacade,
        IOptions<InventoryRepositoryOptions> repositoryOptions, IMapper mapper)
    {
        _mongoDbFacade = mongoDbFacade;
        _mapper = mapper;
        _repositoryOptions = repositoryOptions.Value;
    }

    public async Task<Result<IEnumerable<InventoryItem>>> GetAllEntities()
    {
        var result = await _mongoDbFacade.GetAllEntities(_repositoryOptions.CollectionName);
       
        return result.IsSuccess 
            ? Result.Success(result.Value.Select(inventory => _mapper.Map<InventoryItem>(inventory)))
            : Result.Failure<IEnumerable<InventoryItem>>(result.Error);
    }

    public async Task<Result> SaveEntity(InventoryItem inventoryItem)
    {
        return await _mongoDbFacade.SaveEntity(_repositoryOptions.CollectionName, _mapper.Map<InventoryItemDto>(inventoryItem));
    }

    public async Task<Result> DeleteEntity(string inventoryItemId)
    {
        return await _mongoDbFacade.DeleteEntity(_repositoryOptions.CollectionName, inventoryItemId);
    }

    public async Task<Result<InventoryItem>> GetEntityById(string inventoryItemId)
    {
        var result = await _mongoDbFacade.GetEntityById(_repositoryOptions.CollectionName, inventoryItemId);
        
        return result.IsSuccess 
            ? Result.Success(_mapper.Map<InventoryItem>(result.Value))
            : Result.Failure<InventoryItem>(result.Error);
    }
}