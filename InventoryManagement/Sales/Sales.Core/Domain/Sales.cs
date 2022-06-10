using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using Sales.Core.ValueObjects;

namespace Sales.Core.Domain;

public class Sales
{
    public EntityName Name { get; set; }
    public Period SalesPeriod { get; set; }
    public IList<SalesCost> SalesCosts { get; set; } = new List<SalesCost>();

    private Sales(EntityName name, Period salesPeriod)
    {
        Name = name;
        SalesPeriod = salesPeriod;
    }
    
    public Result RemoveSalesCost(SalesCostType salesCostType)
    {
        var existingSalesCost = SalesCosts.SingleOrDefault(s => s.CostType == salesCostType);

        if (existingSalesCost is not null)
        {
            SalesCosts.Remove(existingSalesCost);
        }
        return Result.Success();
    }
    
    public Result CreateSalesCost(SalesCost salesCost)
    {
        if (SalesCosts.Any(s => s.CostType == salesCost.CostType))
        {
            return Result.Failure($"There is already an entry with cost type {salesCost.CostType}");
        }

        SalesCosts.Add(salesCost);

        return Result.Success();
    }
    
    public Result UpdateCostPerUnit(SalesCostType costType, Money amount)
    {
        var existingSalesCost = SalesCosts.SingleOrDefault(s => s.CostType == costType);
        
        return existingSalesCost is null 
            ? Result.Failure($"There is no entry with cost type {costType}") 
            : UpdateSales(amount, money => existingSalesCost.UpdateCostPerUnit(money));
    }

    public Result UpdateAmount(SalesCostType costType, Money amount)
    {
        var existingSalesCost = SalesCosts.SingleOrDefault(s => s.CostType == costType);
        
        return existingSalesCost is null 
            ? Result.Failure($"There is no entry with cost type {costType}") 
            : UpdateSales(amount, money => existingSalesCost.UpdateAmount(money));
    }

    private static Result UpdateSales(Money amount, Func<Money, Result> updateSales)
    {
        return updateSales(amount);
    }
    public static Sales CreateSales(EntityName name, Period salesPeriod)
    {
        return new Sales(name, salesPeriod);
    }
}