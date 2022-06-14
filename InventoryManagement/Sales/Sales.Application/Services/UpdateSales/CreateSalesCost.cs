using CSharpFunctionalExtensions;
using Sales.Application.Model;
using Sales.Core.Domain;
using Sales.Core.ValueObjects;

namespace Sales.Application.Services.UpdateSales;

public class CreateSalesCost : ISalesCostUpdater
{
    public UpdateSalesCostType UpdateSalesCostType => UpdateSalesCostType.Create;
    public Result PerformUpdate(SalesEntity salesEntity, SalesCostType salesCostType, SalesCost salesCost)
    {
        return salesEntity.CreateSalesCost(salesCost);
    }
}