using CSharpFunctionalExtensions;
using Production.Application.Model;
using Production.Core.Domain;
using Production.Core.ValueObjects;

namespace Production.Application.Services.UpdateProduction;

public interface IProductionCostUpdater
{
    public UpdateProductionCostType UpdateProductionCostType  { get;  }

    public Result PerformUpdate(ProductionEntity productionEntity, ProductionCostType productionCostType, ProductionCost productionCost);
}