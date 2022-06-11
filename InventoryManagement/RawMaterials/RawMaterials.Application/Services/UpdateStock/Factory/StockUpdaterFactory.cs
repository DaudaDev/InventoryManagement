using CSharpFunctionalExtensions;
using RawMaterials.Application.Models;

namespace RawMaterials.Application.Services.UpdateStock.Factory;

public class StockUpdaterFactory : IStockUpdaterFactory
{
    private readonly IEnumerable<IStockUpdater> _stockUpdaters;

    public StockUpdaterFactory(IEnumerable<IStockUpdater> stockUpdaters)
    {
        _stockUpdaters = stockUpdaters;
    }

    public Result<IStockUpdater> GetStockUpdater(RawMaterialUpdateType rawMaterialUpdateType)
    {
        var stockUpdater = _stockUpdaters.FirstOrDefault(s => s.RawMaterialUpdateType == rawMaterialUpdateType);

        return stockUpdater == null
            ? Result.Failure<IStockUpdater>($"StockUpdater with type {rawMaterialUpdateType} not found")
            : Result.Success(stockUpdater);
    }
}