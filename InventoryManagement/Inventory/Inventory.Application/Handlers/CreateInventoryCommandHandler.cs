using Blocks.Shared.Infrastructure;
using CSharpFunctionalExtensions;
using Inventory.Application.Commands;
using Inventory.Core;
using Inventory.Core.Domain;
using MediatR;

namespace Inventory.Application.Handlers;

public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, Result<InventoryItem>>
{
    private readonly IGeneralRepository<InventoryItem> _repository;

    public CreateInventoryCommandHandler(IGeneralRepository<InventoryItem> repository)
    {
        _repository = repository;
    }

    public async Task<Result<InventoryItem>> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventoryItem = InventoryItem.CreateInventoryItem(request.ItemId, request.EntityName);
        
       var result = await _repository.SaveEntity(inventoryItem.Value);

       return result.Match(() => inventoryItem, Result.Failure<InventoryItem>);
    }
}