using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using Sales.Application.Model;
using Sales.Core.Domain;
using Sales.Core.ValueObjects;

namespace Sales.Application.Services.UpdateSalesCost;

public class SalesCostPerUnitUpdater : ISalesUpdater
{
    public UpdateSalesType UpdateSalesType => UpdateSalesType.CostPerUnit;
    public Result PerformUpdate(SalesEntity salesEntity, SalesCostType costType, Money amount)
    {
        return salesEntity.UpdateCostPerUnit(costType, amount);
    }
}