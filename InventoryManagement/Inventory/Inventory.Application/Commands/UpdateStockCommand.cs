using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using Inventory.Application.Model;
using MediatR;

namespace Inventory.Application.Commands;

public class UpdateStockCommand : IRequest<Result> 
{
    public Guid InventoryId { get; set; }
    public Size Size { get; set; }
    public Money Money { get; set; }
    public InventoryUpdateType InventoryUpdateType { get; set; }
}