using CSharpFunctionalExtensions;
using Sales.Application.Model;
using Sales.Core.Domain;
using Sales.Core.ValueObjects;

namespace Sales.Application.Services.UpdateSales;

public class RemoveSalesCost : ISalesCostUpdater
{
    public UpdateSalesCostType UpdateSalesCostType => UpdateSalesCostType.Remove;
    public Result PerformUpdate(SalesEntity salesEntity, SalesCostType salesCostType, SalesCost salesCost)
    {
        return salesEntity.RemoveSalesCost(salesCostType);
    }
}