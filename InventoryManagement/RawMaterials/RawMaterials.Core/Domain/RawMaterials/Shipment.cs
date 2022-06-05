using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;

namespace RawMaterials.Core.Domain.RawMaterials;

public class Shipment
{
    public Size Size { get; set; }
    public Money ShipmentCost { get; set; }
    public Vendor Vendor { get; set; }

    private Shipment(Size size, Money shipmentCost, Vendor vendor)
    {
        Size = size;
        ShipmentCost = shipmentCost;
        Vendor = vendor;
    }

    public static Result<Shipment> CreateShipment(Size size, Money shipmentCost,  Vendor vendor)
    {
        return Result.Success(new Shipment(size, shipmentCost, vendor));
    }
}