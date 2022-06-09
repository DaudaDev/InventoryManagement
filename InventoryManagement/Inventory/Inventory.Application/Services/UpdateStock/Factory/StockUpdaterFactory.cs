using CSharpFunctionalExtensions;
using Inventory.Application.Model;

namespace Inventory.Application.Services.UpdateStock.Factory;

public class StockUpdaterFactory : IStockUpdaterFactory
{
    private readonly IEnumerable<IStockUpdater> _stockUpdaters;

    public StockUpdaterFactory(IEnumerable<IStockUpdater> stockUpdaters)
    {
        _stockUpdaters = stockUpdaters;
    }

    public Result<IStockUpdater> GetStockUpdater(InventoryUpdateType inventoryUpdateType)
    {
        var stockUpdater = _stockUpdaters.FirstOrDefault(s => s.InventoryUpdateType == inventoryUpdateType);

        return stockUpdater == null
            ? Result.Failure<IStockUpdater>($"StockUpdater with type {inventoryUpdateType} not found")
            : Result.Success(stockUpdater);
    }
}