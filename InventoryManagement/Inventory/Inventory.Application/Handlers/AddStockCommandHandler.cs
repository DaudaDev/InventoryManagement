using Blocks.Shared.Contracts.Infrastructure;
using CSharpFunctionalExtensions;
using Inventory.Application.Commands;
using Inventory.Core.Domain;
using MediatR;

namespace Inventory.Application.Handlers;

public class AddStockCommandHandler: IRequestHandler<AddStockCommand, Result>
{
    private readonly IGeneralRepository<InventoryItem> _repository;

    public AddStockCommandHandler(IGeneralRepository<InventoryItem> repository)
    {
        _repository = repository;
    }
    
    public async Task<Result> Handle(AddStockCommand request, CancellationToken cancellationToken)
    {
        var inventoryItem = await _repository.GetEntityById(request.InventoryId.ToString());

        if (inventoryItem.IsFailure)
        {
            return Result.Failure(inventoryItem.Error);
        }

        inventoryItem.Value.AddStockEntry(request.NumberOfItems, request.CostPerUnit, request.PricePerUnit);

        var result = await _repository.SaveEntity(inventoryItem.Value);
        
        return result.Match(Result.Success, Result.Failure);
    }
}