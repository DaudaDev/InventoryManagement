using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using Production.Application.Model;
using Production.Core.Domain;

namespace Production.Application.Services.UpdateProductionCost;

public class ProductionQuantityUpdater : IProductionUpdater
{
    public UpdateProductionType UpdateProductionType => UpdateProductionType.Quantity;
    public Result PerformUpdate(ProductionEntity productionEntity, Period productionPeriod, Size quantity)
    {
        return productionEntity.SetQuantity(quantity);
    }

}