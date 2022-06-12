using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using Inventory.Application.Model;
using Inventory.Core.Domain;

namespace Inventory.Application.Services.UpdateStock;

public class CostPerUnitStockUpdater : IStockUpdater
{
    public InventoryUpdateType InventoryUpdateType => InventoryUpdateType.CostPerUnit;
    public Result PerformUpdate(InventoryItem inventoryItem, Size size, Money money)
    {
        return inventoryItem.UpdateItemCostPerUnit(size.Unit, money);
    }
}