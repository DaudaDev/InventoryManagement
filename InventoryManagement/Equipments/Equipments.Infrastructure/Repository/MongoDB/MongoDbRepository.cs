using AutoMapper;
using Blocks.MongoDb.Facade;
using Blocks.Shared.Contracts.Infrastructure;
using CSharpFunctionalExtensions;
using Equipments.Core.Domain.Equipment;
using Equipments.Infrastructure.Models;
using Microsoft.Extensions.Options;

namespace Equipments.Infrastructure.Repository.MongoDB;

public class MongoDbRepository : IGeneralRepository<Equipment>
{
    private readonly IMongoDbFacade<EquipmentDto> _mongoDbFacade;
    private readonly EquipmentsRepositoryOptions _repositoryOptions;
    private readonly IMapper _mapper;

    public MongoDbRepository(IMongoDbFacade<EquipmentDto> mongoDbFacade,
        IOptions<EquipmentsRepositoryOptions> repositoryOptions, IMapper mapper)
    {
        _mongoDbFacade = mongoDbFacade;
        _mapper = mapper;
        _repositoryOptions = repositoryOptions.Value;
    }

    public async Task<Result<IEnumerable<Equipment>>> GetAllEntities()
    {
        var result = await _mongoDbFacade.GetAllEntities(_repositoryOptions.CollectionName);
       
        return result.IsSuccess 
            ? Result.Success(result.Value.Select(inventory => _mapper.Map<Equipment>(inventory)))
            : Result.Failure<IEnumerable<Equipment>>(result.Error);
    }

    public async Task<Result> SaveEntity(Equipment inventoryItem)
    {
        return await _mongoDbFacade.SaveEntity(_repositoryOptions.CollectionName, _mapper.Map<EquipmentDto>(inventoryItem));
    }

    public async Task<Result> DeleteEntity(string inventoryItemId)
    {
        return await _mongoDbFacade.DeleteEntity(_repositoryOptions.CollectionName, inventoryItemId);
    }

    public async Task<Result<Equipment>> GetEntityById(string inventoryItemId)
    {
        var result = await _mongoDbFacade.GetEntityById(_repositoryOptions.CollectionName, inventoryItemId);
        
        return result.IsSuccess 
            ? Result.Success(_mapper.Map<Equipment>(result.Value))
            : Result.Failure<Equipment>(result.Error);
    }
}