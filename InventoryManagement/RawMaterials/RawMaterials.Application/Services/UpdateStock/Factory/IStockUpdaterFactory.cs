using CSharpFunctionalExtensions;
using RawMaterials.Application.Models;

namespace RawMaterials.Application.Services.UpdateStock.Factory;

public interface IStockUpdaterFactory
{
    Result<IStockUpdater> GetStockUpdater(RawMaterialUpdateType rawMaterialUpdateType);
}