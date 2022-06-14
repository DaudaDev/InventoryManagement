using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using Production.Core.ValueObjects;

namespace Production.Core.Domain;

public class ProductionEntity
{
    public ProductType ProductType { get; set; }
    public Period ProductionPeriod { get; set; }
    public Size Quantity { get; set; }
    public IList<ProductionCost> ProductionCosts { get; set; } = new List<ProductionCost>();

    private ProductionEntity(ProductType productType, Period productionPeriod)
    {
        ProductType = productType;
        ProductionPeriod = productionPeriod;
    }

    public Result SetProductionDate(Period period)
    {
        ProductionPeriod = period;
        
        return Result.Success();
    }
    
    public Result SetQuantity(Size size)
    {
        Quantity = size;
        
        return Result.Success();
    }
    
    public Result CreateProductionCosts(ProductionCost productionCost)
    {
        if (ProductionCosts.Any(s => s.CostType == productionCost.CostType))
        {
            return Result.Failure($"There is already an entry with cost type {productionCost.CostType}");
        }

        ProductionCosts.Add(productionCost);

        return Result.Success();
    }
    
    public Result RemoveProductionCost(ProductionCostType productionCostType)
    {
        var productionCost = ProductionCosts.SingleOrDefault(s => s.CostType == productionCostType);

        if (productionCost is not null)
        {
            ProductionCosts.Remove(productionCost);
        }
        return Result.Success();
    }
    
    public static ProductionEntity CreateProduction(ProductType productType, Period salesPeriod)
    {
        return new ProductionEntity(productType, salesPeriod);
    }
}

