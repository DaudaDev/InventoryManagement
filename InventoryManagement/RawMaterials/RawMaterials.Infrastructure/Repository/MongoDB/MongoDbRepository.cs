using AutoMapper;
using Blocks.MongoDb.Facade;
using Blocks.Shared.Contracts.Infrastructure;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using RawMaterials.Core.Domain.RawMaterials;
using RawMaterials.Infrastructure.Models;

namespace RawMaterials.Infrastructure.Repository.MongoDB;

public class MongoDbRepository : IGeneralRepository<RawMaterial>
{
    private readonly IMongoDbFacade<RawMaterialDto> _mongoDbFacade;
    private readonly RawMaterialRepositoryOptions _repositoryOptions;
    private readonly IMapper _mapper;

    public MongoDbRepository(IMongoDbFacade<RawMaterialDto> mongoDbFacade,
        IOptions<RawMaterialRepositoryOptions> repositoryOptions, IMapper mapper)
    {
        _mongoDbFacade = mongoDbFacade;
        _mapper = mapper;
        _repositoryOptions = repositoryOptions.Value;
    }

    public async Task<Result<IEnumerable<RawMaterial>>> GetAllEntities()
    {
        var result = await _mongoDbFacade.GetAllEntities(_repositoryOptions.CollectionName);
       
        return result.IsSuccess 
            ? Result.Success(result.Value.Select(inventory => _mapper.Map<RawMaterial>(inventory)))
            : Result.Failure<IEnumerable<RawMaterial>>(result.Error);
    }

    public async Task<Result> SaveEntity(RawMaterial inventoryItem)
    {
        return await _mongoDbFacade.SaveEntity(_repositoryOptions.CollectionName, _mapper.Map<RawMaterialDto>(inventoryItem));
    }

    public async Task<Result> DeleteEntity(string inventoryItemId)
    {
        return await _mongoDbFacade.DeleteEntity(_repositoryOptions.CollectionName, inventoryItemId);
    }

    public async Task<Result<RawMaterial>> GetEntityById(string inventoryItemId)
    {
        var result = await _mongoDbFacade.GetEntityById(_repositoryOptions.CollectionName, inventoryItemId);
        
        return result.IsSuccess 
            ? Result.Success(_mapper.Map<RawMaterial>(result.Value))
            : Result.Failure<RawMaterial>(result.Error);
    }
}