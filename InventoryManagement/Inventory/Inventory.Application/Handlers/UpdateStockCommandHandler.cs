using Blocks.Shared.Contracts.Infrastructure;
using CSharpFunctionalExtensions;
using Inventory.Application.Commands;
using Inventory.Application.Services.UpdateStock.Factory;
using Inventory.Core.Domain;
using MediatR;

namespace Inventory.Application.Handlers;

public class UpdateStockCommandHandler: IRequestHandler<UpdateStockCommand, Result>
{
    private readonly IGeneralRepository<InventoryItem> _repository;
    private readonly IStockUpdaterFactory _stockUpdaterFactory;
    
    public UpdateStockCommandHandler(IGeneralRepository<InventoryItem> repository, IStockUpdaterFactory stockUpdaterFactory)
    {
        _repository = repository;
        _stockUpdaterFactory = stockUpdaterFactory;
    }
    
    public async Task<Result> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
    {
        var inventoryItem = await _repository.GetEntityById(request.InventoryId.ToString());

        if (inventoryItem.IsFailure)
        {
            return Result.Failure(inventoryItem.Error);
        }

        var stockUpdater = _stockUpdaterFactory.GetStockUpdater(request.InventoryUpdateType);

        stockUpdater.Value.PerformUpdate(inventoryItem.Value, request.Size, request.Money);
        
        var result = await _repository.SaveEntity(inventoryItem.Value);
        
        return result.Match(Result.Success, Result.Failure);
    }
}