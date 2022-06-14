using CSharpFunctionalExtensions;
using Production.Application.Model;

namespace Production.Application.Services.UpdateProductionCost.Factory;

public class ProductionUpdaterFactory : IProductionUpdaterFactory
{
    private readonly IEnumerable<IProductionUpdater> _productionUpdaters;

    public ProductionUpdaterFactory(IEnumerable<IProductionUpdater> productionUpdaters)
    {
        _productionUpdaters = productionUpdaters;
    }

  
    public Result<IProductionUpdater> GetProductionUpdater(UpdateProductionType updateProductionType)
    {
        var stockUpdater = _productionUpdaters.FirstOrDefault(s => s.UpdateProductionType == updateProductionType);

        return stockUpdater == null
            ? Result.Failure<IProductionUpdater>($"ProductionUpdater with type {updateProductionType} not found")
            : Result.Success(stockUpdater);
    }
}