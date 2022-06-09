using CSharpFunctionalExtensions;
using Inventory.Application.Model;
using MediatR;

namespace Inventory.Application.Commands;

public class UpdateItemCommand: IRequest<Result>
{
    public Guid ItemId { get; set; }

    public InventoryUpdateType InventoryUpdateType { get; set; }
}