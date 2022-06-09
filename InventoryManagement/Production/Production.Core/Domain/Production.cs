using Blocks.Shared.ValueObjects;
using Production.Core.ValueObjects;

namespace Production.Core.Domain;

public class Production
{
    public ProductType ProductType { get; set; }
    public Period Date { get; set; }
    public Size Quantity { get; set; }
    public IList<ProductionCost> ProductionCosts { get; set; }
}

