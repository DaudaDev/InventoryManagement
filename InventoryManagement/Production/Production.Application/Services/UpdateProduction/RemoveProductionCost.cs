using CSharpFunctionalExtensions;
using Production.Application.Model;
using Production.Core.Domain;
using Production.Core.ValueObjects;

namespace Production.Application.Services.UpdateProduction;

public class RemoveProductionCost : IProductionCostUpdater
{
    public UpdateProductionCostType UpdateProductionCostType => UpdateProductionCostType.Remove;

    public Result PerformUpdate(ProductionEntity productionEntity, ProductionCostType productionCostType,
        ProductionCost productionCost)
    {
        return productionEntity.RemoveProductionCost(productionCostType);
    }
}