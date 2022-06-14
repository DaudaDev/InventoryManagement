using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using Production.Application.Model;
using Production.Core.Domain;

namespace Production.Application.Services.UpdateProductionCost;

public interface IProductionUpdater
{
    public UpdateProductionType UpdateProductionType  { get;  }

    public Result PerformUpdate(ProductionEntity productionEntity, Period productionPeriod, Size quantity);
}  