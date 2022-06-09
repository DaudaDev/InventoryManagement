using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using Inventory.Application.Model;
using Inventory.Core;
using Inventory.Core.Domain;

namespace Inventory.Application.Services.UpdateStock;

public class PricePerUnitStockUpdater : IStockUpdater
{
    public InventoryUpdateType InventoryUpdateType => InventoryUpdateType.PricePerUnit;
    public Result PerformUpdate(InventoryItem inventoryItem, Size size, Money money)
    {
        return inventoryItem.UpdateItemPricePerUnit(size.Unit, money);
    }
}