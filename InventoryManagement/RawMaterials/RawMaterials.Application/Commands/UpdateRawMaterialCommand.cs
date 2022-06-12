using CSharpFunctionalExtensions;
using MediatR;
using RawMaterials.Core.ValueObjects;

namespace RawMaterials.Application.Commands;

public class UpdateRawMaterialCommand: IRequest<Result>  
{
    public Guid RawMaterialId { get; set; }
    public MaterialType MaterialType { get; set; }
    public Brand Brand { get; set; }
}