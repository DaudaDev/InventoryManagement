using CSharpFunctionalExtensions;
using Production.Application.Model;

namespace Production.Application.Services.UpdateProduction.Factory;

public class ProductionCostUpdaterFactory : IProductionCostUpdaterFactory
{
    private readonly IEnumerable<IProductionCostUpdater> _productionCostUpdaters;

    public ProductionCostUpdaterFactory(IEnumerable<IProductionCostUpdater> productionCostUpdaters)
    {
        _productionCostUpdaters = productionCostUpdaters;
    }

  
    public Result<IProductionCostUpdater> GetProductionCostUpdater(UpdateProductionCostType updateProductionCostType)
    {
        var stockUpdater = _productionCostUpdaters.FirstOrDefault(s => s.UpdateProductionCostType == updateProductionCostType);

        return stockUpdater == null
            ? Result.Failure<IProductionCostUpdater>($"ProductionCostUpdater with type {updateProductionCostType} not found")
            : Result.Success(stockUpdater);
    }
}