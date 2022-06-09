using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using Inventory.Application.Model;
using Inventory.Core;
using Inventory.Core.Domain;

namespace Inventory.Application.Services.UpdateStock;

public class WithdrawStockUpdater : IStockUpdater
{
    public InventoryUpdateType InventoryUpdateType => InventoryUpdateType.Withdraw;
    public Result PerformUpdate(InventoryItem inventoryItem, Size size, Money money)
    {
        return inventoryItem.WithdrawItemFromStockEntry(size);
    }
}