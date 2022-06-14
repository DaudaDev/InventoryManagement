using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using Sales.Application.Model;
using Sales.Core.Domain;
using Sales.Core.ValueObjects;

namespace Sales.Application.Services.UpdateSalesCost;

public interface ISalesUpdater
{
    public UpdateSalesType UpdateSalesType  { get;  }

    public Result PerformUpdate(SalesEntity salesEntity, SalesCostType costType, Money amount);
}  