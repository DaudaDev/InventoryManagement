using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using Inventory.Application.Model;
using Inventory.Core;
using Inventory.Core.Domain;

namespace Inventory.Application.Services.UpdateStock;

public interface IStockUpdater
{
    public InventoryUpdateType InventoryUpdateType  { get;  }

    public Result PerformUpdate(InventoryItem inventoryItem, Size size, Money money);
}