using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using MediatR;
using RawMaterials.Core.Domain.RawMaterials;

namespace RawMaterials.Application.Commands;

public class ReceiveShipmentCommand: IRequest<Result>
{
    public Guid RawMaterialId { get; set; }

    public Size Size { get; set; }
    
    public Money ShipmentCOst { get; set; }

    public Vendor Vendor { get; set; }
}