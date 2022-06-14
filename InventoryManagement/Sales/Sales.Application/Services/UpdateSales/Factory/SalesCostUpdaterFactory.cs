using CSharpFunctionalExtensions;
using Sales.Application.Model;

namespace Sales.Application.Services.UpdateSales.Factory;

public class SalesCostUpdaterFactory : ISalesCostUpdaterFactory
{
    private readonly IEnumerable<ISalesCostUpdater> _salesCostUpdaters;

    public SalesCostUpdaterFactory(IEnumerable<ISalesCostUpdater> salesCostUpdaters)
    {
        _salesCostUpdaters = salesCostUpdaters;
    }

  
    public Result<ISalesCostUpdater> GetSalesCostUpdater(UpdateSalesCostType updateSalesCostType)
    {
        var salesCostUpdater = _salesCostUpdaters.FirstOrDefault(s => s.UpdateSalesCostType == updateSalesCostType);

        return salesCostUpdater == null
            ? Result.Failure<ISalesCostUpdater>($"SalesCostUpdater with type {updateSalesCostType} not found")
            : Result.Success(salesCostUpdater);
    }
}