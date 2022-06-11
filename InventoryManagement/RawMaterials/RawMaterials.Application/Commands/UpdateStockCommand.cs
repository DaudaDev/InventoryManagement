using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using MediatR;
using RawMaterials.Application.Models;

namespace RawMaterials.Application.Commands;

public class UpdateStockCommand : IRequest<Result> 
{
    public Guid RawMaterialId { get; set; }
    public Size Size { get; set; }
    public RawMaterialUpdateType RawMaterialUpdateType { get; set; }
}