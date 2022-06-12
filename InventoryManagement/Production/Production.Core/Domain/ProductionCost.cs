using Blocks.Shared.ValueObjects;
using Production.Core.ValueObjects;

namespace Production.Core.Domain;

public class ProductionCost
{
    public ProductionCostType CostType { get; set; }
    public Money TotalCost { set; get; }
    public Money CostPerUnit { get; set; }
    public Size Quantity { get; set; }

    private ProductionCost(ProductionCostType costType, Money totalCost, Money costPerUnit, Size quantity)
    {
        CostType = costType;
        TotalCost = totalCost;
        CostPerUnit = costPerUnit;
        Quantity = quantity;
    }

    public static ProductionCost CreateProduction(ProductionCostType costType, Money totalCost, Money costPerUnit,
        Size quantity)
    {
        return new ProductionCost(costType, totalCost, costPerUnit, quantity);
    }
}