using Blocks.Shared.Aggregates;
using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using RawMaterials.Core.ValueObjects;

namespace RawMaterials.Core.Domain.RawMaterials;

public class RawMaterial : AggregateEntity
{
    public MaterialType MaterialType { get; set; }
    public Brand Brand { get; set; }
    public IList<Shipment> Shipments { get; set; } = Array.Empty<Shipment>();
    public IList<Size> CurrentStock { get; set; } = Array.Empty<Size>();

    public void SetMaterialType(MaterialType materialType)
    {
        MaterialType = materialType;
    }

    public void SetBrand(Brand brand)
    {
        Brand = brand;
    }

    public void RecieveShipment(Size size, Money shipmentCost, Vendor vendor)
    {
        Shipments.Add(Shipment.CreateShipment(size, shipmentCost, vendor).Value);

        var existingStock = CurrentStock.SingleOrDefault(c => c.Unit == size.Unit);

        if (existingStock is not null)
        {
            existingStock.Amount += size.Amount;
            return;
        }

        CurrentStock.Add(size);
    }

    public void AddStock(Size size)
    {
        var existingStock = CurrentStock.SingleOrDefault(c => c.Unit == size.Unit);

        if (existingStock is not null)
        {
            existingStock.Amount += size.Amount;
        }

        CurrentStock.Add(size);
    }

    public Result UseStock(Size size)
    {
        var existingStock = CurrentStock.SingleOrDefault(c => c.Unit == size.Unit);

        if (existingStock is null)
        {
            return Result.Failure($"{MaterialType} with unit = {size.Unit} is not available");
        }

        if (existingStock.Amount < size.Amount)
        {
            return Result.Failure(
                $"Cannot subtract {size.Amount} from remaining {existingStock.Amount} {MaterialType}");
        }

        existingStock.Amount -= size.Amount;
        
        return Result.Success();
    }
}