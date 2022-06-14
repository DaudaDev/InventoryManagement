using CSharpFunctionalExtensions;
using Production.Application.Model;
using Production.Core.Domain;
using Production.Core.ValueObjects;

namespace Production.Application.Services.UpdateProduction;

public class CreateProductionCost : IProductionCostUpdater
{
    public UpdateProductionCostType UpdateProductionCostType => UpdateProductionCostType.Create;

    public Result PerformUpdate(ProductionEntity productionEntity, ProductionCostType productionCostType,
        ProductionCost productionCost)
    {
        return productionEntity.CreateProductionCosts(productionCost);
    }
}