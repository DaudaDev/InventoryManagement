using CSharpFunctionalExtensions;
using Sales.Application.Model;

namespace Sales.Application.Services.UpdateSalesCost.Factory;

public interface ISalesUpdaterFactory
{
    Result<ISalesUpdater> GetSalesUpdater(UpdateSalesType updateSalesType);
}