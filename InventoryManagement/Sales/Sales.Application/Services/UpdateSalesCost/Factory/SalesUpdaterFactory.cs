using CSharpFunctionalExtensions;
using Sales.Application.Model;

namespace Sales.Application.Services.UpdateSalesCost.Factory;

public class SalesUpdaterFactory : ISalesUpdaterFactory
{
    private readonly IEnumerable<ISalesUpdater> _salesUpdaters;

    public SalesUpdaterFactory(IEnumerable<ISalesUpdater> salesUpdaters)
    {
        _salesUpdaters = salesUpdaters;
    }

  
    public Result<ISalesUpdater> GetSalesUpdater(UpdateSalesType updateSalesType)
    {
        var salesUpdater = _salesUpdaters.FirstOrDefault(s => s.UpdateSalesType == updateSalesType);

        return salesUpdater == null
            ? Result.Failure<ISalesUpdater>($"SalesUpdater with type {updateSalesType} not found")
            : Result.Success(salesUpdater);
    }
}