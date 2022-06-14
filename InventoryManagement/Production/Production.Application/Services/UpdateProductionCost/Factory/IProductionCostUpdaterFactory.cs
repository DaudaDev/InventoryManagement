using CSharpFunctionalExtensions;
using Production.Application.Model;

namespace Production.Application.Services.UpdateProductionCost.Factory;

public interface IProductionUpdaterFactory
{
    Result<IProductionUpdater> GetProductionUpdater(UpdateProductionType updateProductionType);
}