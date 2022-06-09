using Blocks.Shared.ValueObjects;

namespace Sales.Core.Domain;

public class Sales
{
    public EntityName Name { get; set; }
    public Period SalesPeriod { get; set; }
    public IList<SalesCost> SalesCosts { get; set; }
}