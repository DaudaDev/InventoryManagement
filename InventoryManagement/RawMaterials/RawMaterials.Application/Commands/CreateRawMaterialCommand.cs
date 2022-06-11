using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using MediatR;
using RawMaterials.Core.Domain.RawMaterials;
using RawMaterials.Core.ValueObjects;

namespace RawMaterials.Application.Commands;

public class CreateRawMaterialCommand: IRequest<Result<RawMaterial>>  
{
    public Guid RawMaterialId { get; set; }
    public MaterialType MaterialType { get; set; }
    public Brand Brand { get; set; }
}