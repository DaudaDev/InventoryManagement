using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using Sales.Core.ValueObjects;

namespace Sales.Core.Domain;

public class SalesCost
{
    public Money Amount { get; set; }
    public Money CostPerUnit { get; set; }
    public SalesCostType CostType { get; set; }

    private SalesCost(Money amount, Money costPerUnit, SalesCostType costType)
    {
        Amount = amount;
        CostPerUnit = costPerUnit;
        CostType = costType;
    }
    
    public Result UpdateAmount(Money amount)
    {
        Amount = amount;
        
        return Result.Success();
    }

    public Result UpdateCostPerUnit(Money costPerUnit)
    {
        CostPerUnit = costPerUnit;
        
        return Result.Success();
    }
    public static SalesCost CreateSalesCost(Money amount, Money costPerUnit, SalesCostType costType)
    {
        return new SalesCost(amount, costPerUnit, costType);
    }
}