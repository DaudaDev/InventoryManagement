using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using Inventory.Core;
using Inventory.Core.Domain;
using MediatR;

namespace Inventory.Application.Commands;

public class CreateInventoryCommand: IRequest<Result<InventoryItem>>  
{
    public Guid ItemId { get; set; }
    public EntityName EntityName { get; set; }
}