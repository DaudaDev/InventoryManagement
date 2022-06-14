using CSharpFunctionalExtensions;
using Sales.Application.Model;

namespace Sales.Application.Services.UpdateSales.Factory;

public interface ISalesCostUpdaterFactory
{
    Result<ISalesCostUpdater> GetSalesCostUpdater(UpdateSalesCostType updateSalesCostType);
}