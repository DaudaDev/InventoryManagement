using Blocks.Shared.Aggregates;
using Blocks.Shared.Extenstions;
using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;

namespace Inventory.Core.Domain;

public class InventoryItem : AggregateEntity
{
    public EntityName Name { get; set; }
    public IList<InventoryItemEntry> CurrentStock { get; set; } = new List<InventoryItemEntry>();

#pragma warning disable CS8618
    private InventoryItem()
#pragma warning restore CS8618
    {
        
    }
    private InventoryItem(Guid inventoryId, EntityName entityName)
    {
        EntityId = inventoryId;
        Name = entityName;
    }

    public void SetName(EntityName entityName)
    {
        Name = entityName;
    }
    
    public static Result<InventoryItem> CreateInventoryItem(Guid itemId, EntityName entityName)
    {
        return itemId == Guid.Empty 
            ? Result.Failure<InventoryItem>($"{nameof(itemId)} cannot be empty") 
            : Result.Success<InventoryItem>(new (itemId, entityName));
    }

    public Result AddStockEntry(Size numberOfItems, Money costPerUnit, Money pricePerUnit)
    {
        if (CurrentStock.Any(c => MatchUnits(numberOfItems.Unit, c)))
        {
            return Result.Failure($"An item of size {numberOfItems.Unit} already exists, try updating it");
        }
        
        CurrentStock.Add(InventoryItemEntry.CreateInventoryItemEntry(numberOfItems, costPerUnit, pricePerUnit).Value);
        
        return Result.Success();
    }
    public Result AddItemToStockEntry(Size size)
    {
        var existingStock = GetExistingStock(size.Unit);
        
        return existingStock
            .ExecuteSuccess(stock => stock.AddItems(size.Amount));
    }
    
    public Result WithdrawItemFromStockEntry(Size size)
    {
        var existingStock = GetExistingStock(size.Unit);

        return existingStock
            .ExecuteSuccess(stock => stock.WithdrawItem(size.Amount));
    }
    
    public Result UpdateItemCostPerUnit(Unit itemSize, Money newCostPerUnit)
    {
        var existingStock = GetExistingStock(itemSize);
        
        return existingStock
            .ExecuteSuccess(stock => stock.UpdateCostPerUnit(newCostPerUnit));
    }
    
    public Result UpdateItemPricePerUnit(Unit itemSize, Money newPricePerUnit)
    {
        var existingStock = GetExistingStock(itemSize);

        return existingStock
            .ExecuteSuccess(stock => stock.UpdatePricePerUnit(newPricePerUnit));
    }
    
    private static bool MatchUnits(Unit unit, InventoryItemEntry inventoryItem)
    {
        return inventoryItem.NumberOfItems.CurrentValue.Unit == unit;
    }

    private Result<InventoryItemEntry> GetExistingStock(Unit itemSize)
    {
        var existingStock = CurrentStock.SingleOrDefault(c => MatchUnits(itemSize, c));

        return existingStock is null 
            ? Result.Failure<InventoryItemEntry>($"Stock with size {itemSize} does not exist, please add it first") 
            : Result.Success(existingStock);
    }
}