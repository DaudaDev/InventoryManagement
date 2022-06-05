using Blocks.Shared.Aggregates;
using Blocks.Shared.Extenstions;
using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;

namespace Inventory.Core;

public class InventoryItem : AggregateEntity
{
    public EntityName Name { get; set; }
    private IList<InventoryItemEntry> CurrentStock { get; set; } = Array.Empty<InventoryItemEntry>();

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
        if (CurrentStock.Any(c => c.NumberOfItems.Unit == numberOfItems.Unit))
        {
            return Result.Failure($"An item of size {numberOfItems.Unit} already exists, try updating it");
        }
        
        CurrentStock.Add(InventoryItemEntry.CreateInventoryItemEntry(numberOfItems, costPerUnit, pricePerUnit).Value);
        
        return Result.Success();
    }

    public Result AddItemToStockEntry(Unit itemSize, int amount)
    {
        var existingStock = GetExistingStock(itemSize);
        
        return existingStock
            .ExecuteSuccess(stock => stock.AddItems(amount));
    }
    
    public Result WithdrawItemFromStockEntry(Unit itemSize, int amount)
    {
        var existingStock = GetExistingStock(itemSize);

        return existingStock
            .ExecuteSuccess(stock => stock.WithdrawItem(amount));
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

    private Result<InventoryItemEntry> GetExistingStock(Unit itemSize)
    {
        var existingStock = CurrentStock.SingleOrDefault(c => c.NumberOfItems.Unit == itemSize);

        return existingStock is null 
            ? Result.Failure<InventoryItemEntry>($"Stock with size {itemSize} does not exist, please add it first") 
            : Result.Success(existingStock);
    }
}