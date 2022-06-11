using Blocks.Shared.Aggregates;
using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using RawMaterials.Core.ValueObjects;

namespace RawMaterials.Core.Domain.RawMaterials;

public class RawMaterial : AggregateEntity
{
    public MaterialType MaterialType { get; set; }
    public Brand Brand { get; set; }
    public IList<Shipment> Shipments { get; } = new List<Shipment>();
    public IList<Size> CurrentStock { get; } = new List<Size>();

    private RawMaterial(Guid rawMaterialId, MaterialType materialType, Brand brand)
    {
        MaterialType = materialType;
        Brand = brand;
        EntityId = rawMaterialId;
    }

    public void SetMaterialType(MaterialType materialType)
    {
        MaterialType = materialType;
    }

    public void SetBrand(Brand brand)
    {
        Brand = brand;
    }

    public void ReceiveShipment(Size size, Money shipmentCost, Vendor vendor)
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

    public Result AddStock(Size size)
    {
        var existingStock = CurrentStock.SingleOrDefault(c => c.Unit == size.Unit);

        if (existingStock is not null)
        {
            existingStock.Amount += size.Amount;
            return Result.Success();
        }

        CurrentStock.Add(size);
        return Result.Success();
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

    public static Result<RawMaterial> CreatRawMaterial(Guid rawMaterialId, MaterialType materialType, Brand brand)
    {
        return new RawMaterial(rawMaterialId, materialType, brand);
    }
}