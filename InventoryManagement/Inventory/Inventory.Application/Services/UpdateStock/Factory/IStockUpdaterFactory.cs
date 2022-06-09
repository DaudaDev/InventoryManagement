using CSharpFunctionalExtensions;
using Inventory.Application.Model;

namespace Inventory.Application.Services.UpdateStock.Factory;

public interface IStockUpdaterFactory
{
    Result<IStockUpdater> GetStockUpdater(InventoryUpdateType inventoryUpdateType);
}