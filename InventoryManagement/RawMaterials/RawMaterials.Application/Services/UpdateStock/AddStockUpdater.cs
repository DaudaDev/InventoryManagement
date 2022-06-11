using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using RawMaterials.Application.Models;
using RawMaterials.Core.Domain.RawMaterials;

namespace RawMaterials.Application.Services.UpdateStock;

public class AddStockUpdater : IStockUpdater
{
    public RawMaterialUpdateType RawMaterialUpdateType => RawMaterialUpdateType.AddStock;
    public Result PerformUpdate(RawMaterial rawMaterial, Size size)
    {
        return rawMaterial.AddStock(size);
    }
}