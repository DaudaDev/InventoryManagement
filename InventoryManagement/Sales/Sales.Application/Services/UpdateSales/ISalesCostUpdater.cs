using CSharpFunctionalExtensions;
using Sales.Application.Model;
using Sales.Core.Domain;
using Sales.Core.ValueObjects;

namespace Sales.Application.Services.UpdateSales;

public interface ISalesCostUpdater
{
    public UpdateSalesCostType UpdateSalesCostType  { get;  }

    public Result PerformUpdate(SalesEntity salesEntity, SalesCostType salesCostType, SalesCost salesCost);
}