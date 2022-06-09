using Blocks.Shared.ValueObjects;
using Sales.Core.ValueObjects;

namespace Sales.Core.Domain;

public class SalesCost
{
    public Money Amount { get; set; }
    public Money CostPerUnit { get; set; }
    public SalesCostType CostType { get; set; }
}