using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using Inventory.Application.Model;
using Inventory.Core.Domain;

namespace Inventory.Application.Services.UpdateStock;

public class DepositStockUpdater : IStockUpdater
{
    public InventoryUpdateType InventoryUpdateType => InventoryUpdateType.Deposit; 
    public Result PerformUpdate(InventoryItem inventoryItem, Size size, Money money)
    {
        return inventoryItem.AddItemToStockEntry(size);
    }
}