using CSharpFunctionalExtensions;
using Production.Application.Model;

namespace Production.Application.Services.UpdateProduction.Factory;

public interface IProductionCostUpdaterFactory
{
    Result<IProductionCostUpdater> GetProductionCostUpdater(UpdateProductionCostType updateProductionCostType);
}