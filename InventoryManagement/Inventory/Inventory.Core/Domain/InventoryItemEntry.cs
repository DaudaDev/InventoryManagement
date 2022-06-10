using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;

namespace Inventory.Core.Domain;

public class InventoryItemEntry
{
    public MoneyTrail CostPerUnit { get; set; }
    public MoneyTrail PricePerUnit { get; set; }
    public SizeTrail NumberOfItems { get; set; }

    private InventoryItemEntry(Size numberOfItems, Money costPerUnit, Money pricePerUnit)
    {
        NumberOfItems = new (numberOfItems);
        CostPerUnit =  new (costPerUnit);
        PricePerUnit =  new (pricePerUnit);
    }

    public static Result<InventoryItemEntry> CreateInventoryItemEntry(Size numberOfItems, Money costPerUnit, Money pricePerUnit)
    {
        return Result.Success<InventoryItemEntry>(new (numberOfItems, costPerUnit, pricePerUnit));
    }

    public Result AddItems(double amount)
    {
        return NumberOfItems.AddAmount(amount);
    }

    public Result WithdrawItem(double amount)
    {
        return NumberOfItems.SubtractAmount(amount);
    }

    public Result UpdateCostPerUnit(Money newCostPerUnit)
    {
       return CostPerUnit.UpdateMoney(newCostPerUnit);
    }

    public Result UpdatePricePerUnit(Money newPricePerUnit)
    {
        return PricePerUnit.UpdateMoney(newPricePerUnit);
    }
}