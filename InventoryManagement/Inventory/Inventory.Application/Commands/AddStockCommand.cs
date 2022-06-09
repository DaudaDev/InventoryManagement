using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using MediatR;

namespace Inventory.Application.Commands;

public class AddStockCommand: IRequest<Result>  
{
    public Guid InventoryId { get; set; }
    public Size NumberOfItems { get; set; }
    public Money CostPerUnit { get; set; }
    public Money PricePerUnit { get; set; }
}