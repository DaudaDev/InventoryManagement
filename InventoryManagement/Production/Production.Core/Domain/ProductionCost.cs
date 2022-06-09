using Blocks.Shared.ValueObjects;
using Production.Core.ValueObjects;

namespace Production.Core.Domain;

public class ProductionCost
{
    public ProductionCostType CostType { get; set; }
    public Money TotalCost { set; get; }
    public Money CostPerUnit { get; set; }
    public Size Quantity { get; set; }
}