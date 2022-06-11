using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using RawMaterials.Application.Models;
using RawMaterials.Core.Domain.RawMaterials;

namespace RawMaterials.Application.Services.UpdateStock;

public interface IStockUpdater
{
    public RawMaterialUpdateType RawMaterialUpdateType  { get;  }

    public Result PerformUpdate(RawMaterial rawMaterial, Size size);
}