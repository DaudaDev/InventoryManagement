using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using Sales.Application.Model;
using Sales.Core.Domain;
using Sales.Core.ValueObjects;

namespace Sales.Application.Services.UpdateSalesCost;

public class SalesAmountUpdater : ISalesUpdater
{
    public UpdateSalesType UpdateSalesType => UpdateSalesType.Amount;
    public Result PerformUpdate(SalesEntity salesEntity, SalesCostType costType, Money amount)
    {
        return salesEntity.UpdateAmount(costType, amount);
    }
}