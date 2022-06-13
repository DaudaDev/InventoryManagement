using AutoMapper;
using Blocks.MongoDb.Facade;
using Blocks.Shared.Contracts.Infrastructure;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using Production.Core.Domain;
using Production.Infrastructure.Models;

namespace Production.Infrastructure.Repository.MongoDB;

public class MongoDbRepository : IGeneralRepository<ProductionEntity>
{
    private readonly IMongoDbFacade<ProductionDto> _mongoDbFacade;
    private readonly ProductionRepositoryOptions _repositoryOptions;
    private readonly IMapper _mapper;

    public MongoDbRepository(IMongoDbFacade<ProductionDto> mongoDbFacade,
        IOptions<ProductionRepositoryOptions> repositoryOptions, IMapper mapper)
    {
        _mongoDbFacade = mongoDbFacade;
        _mapper = mapper;
        _repositoryOptions = repositoryOptions.Value;
    }

    public async Task<Result<IEnumerable<ProductionEntity>>> GetAllEntities()
    {
        var result = await _mongoDbFacade.GetAllEntities(_repositoryOptions.CollectionName);
       
        return result.IsSuccess 
            ? Result.Success(result.Value.Select(inventory => _mapper.Map<ProductionEntity>(inventory)))
            : Result.Failure<IEnumerable<ProductionEntity>>(result.Error);
    }

    public async Task<Result> SaveEntity(ProductionEntity inventoryItem)
    {
        return await _mongoDbFacade.SaveEntity(_repositoryOptions.CollectionName, _mapper.Map<ProductionDto>(inventoryItem));
    }

    public async Task<Result> DeleteEntity(string inventoryItemId)
    {
        return await _mongoDbFacade.DeleteEntity(_repositoryOptions.CollectionName, inventoryItemId);
    }

    public async Task<Result<ProductionEntity>> GetEntityById(string inventoryItemId)
    {
        var result = await _mongoDbFacade.GetEntityById(_repositoryOptions.CollectionName, inventoryItemId);
        
        return result.IsSuccess 
            ? Result.Success(_mapper.Map<ProductionEntity>(result.Value))
            : Result.Failure<ProductionEntity>(result.Error);
    }
}