using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;

namespace Inventory.Core;

public class InventoryItemEntry
{
    public Money CostPerUnit { get; set; }
    public Money PricePerUnit { get; set; }
    public Size NumberOfItems { get; set; }

    private InventoryItemEntry(Size numberOfItems, Money costPerUnit, Money pricePerUnit)
    {
        NumberOfItems = numberOfItems;
        CostPerUnit = costPerUnit;
        PricePerUnit = pricePerUnit;
    }

    public static Result<InventoryItemEntry> CreateInventoryItemEntry(Size numberOfItems, Money costPerUnit, Money pricePerUnit)
    {
        return Result.Success<InventoryItemEntry>(new (numberOfItems, costPerUnit, pricePerUnit));
    }

    public Result AddItems(int amount)
    {
        NumberOfItems.Amount += amount;
        
        return Result.Success();
    }

    public Result WithdrawItem(int amount)
    {
        if (NumberOfItems.Amount < amount)
        {
            return Result.Failure($"There are only {NumberOfItems.Amount} items remaining");
        }

        NumberOfItems.Amount -= amount;
        
        return Result.Success();
    }

    public Result UpdateCostPerUnit(Money newCostPerUnit)
    {
        CostPerUnit = newCostPerUnit;
        
        return Result.Success();
    }

    public Result UpdatePricePerUnit(Money newPricePerUnit)
    {
        PricePerUnit = newPricePerUnit;
        
        return Result.Success();
    }
}