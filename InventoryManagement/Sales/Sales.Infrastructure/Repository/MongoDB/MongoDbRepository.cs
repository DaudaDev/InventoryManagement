using AutoMapper;
using Blocks.MongoDb.Facade;
using Blocks.Shared.Contracts.Infrastructure;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using Sales.Core.Domain;
using Sales.Infrastructure.Models;

namespace Sales.Infrastructure.Repository.MongoDB;

public class MongoDbRepository : IGeneralRepository<SalesEntity>
{
    private readonly IMongoDbFacade<SalesDto> _mongoDbFacade;
    private readonly SalesRepositoryOptions _repositoryOptions;
    private readonly IMapper _mapper;

    public MongoDbRepository(IMongoDbFacade<SalesDto> mongoDbFacade,
        IOptions<SalesRepositoryOptions> repositoryOptions, IMapper mapper)
    {
        _mongoDbFacade = mongoDbFacade;
        _mapper = mapper;
        _repositoryOptions = repositoryOptions.Value;
    }

    public async Task<Result<IEnumerable<SalesEntity>>> GetAllEntities()
    {
        var result = await _mongoDbFacade.GetAllEntities(_repositoryOptions.CollectionName);
       
        return result.IsSuccess 
            ? Result.Success(result.Value.Select(inventory => _mapper.Map<SalesEntity>(inventory)))
            : Result.Failure<IEnumerable<SalesEntity>>(result.Error);
    }

    public async Task<Result> SaveEntity(SalesEntity inventoryItem)
    {
        return await _mongoDbFacade.SaveEntity(_repositoryOptions.CollectionName, _mapper.Map<SalesDto>(inventoryItem));
    }

    public async Task<Result> DeleteEntity(string inventoryItemId)
    {
        return await _mongoDbFacade.DeleteEntity(_repositoryOptions.CollectionName, inventoryItemId);
    }

    public async Task<Result<SalesEntity>> GetEntityById(string inventoryItemId)
    {
        var result = await _mongoDbFacade.GetEntityById(_repositoryOptions.CollectionName, inventoryItemId);
        
        return result.IsSuccess 
            ? Result.Success(_mapper.Map<SalesEntity>(result.Value))
            : Result.Failure<SalesEntity>(result.Error);
    }
}