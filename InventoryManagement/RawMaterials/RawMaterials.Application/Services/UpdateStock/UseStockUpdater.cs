using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using RawMaterials.Application.Models;
using RawMaterials.Core.Domain.RawMaterials;

namespace RawMaterials.Application.Services.UpdateStock;

public class UseStockUpdater : IStockUpdater
{
    public RawMaterialUpdateType RawMaterialUpdateType => RawMaterialUpdateType.UseStock;
    public Result PerformUpdate(RawMaterial rawMaterial, Size size)
    {
        return rawMaterial.UseStock(size);
    }
}