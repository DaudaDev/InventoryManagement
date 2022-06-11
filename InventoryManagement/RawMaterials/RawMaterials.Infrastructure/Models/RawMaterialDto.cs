using Blocks.MongoDb.Models;
using Blocks.Shared.ValueObjects;
using RawMaterials.Core.Domain.RawMaterials;
using RawMaterials.Core.ValueObjects;

namespace RawMaterials.Infrastructure.Models;

public class RawMaterialDto : MongoDbBaseDocument
{
    public MaterialType MaterialType { get; set; }
    public Brand Brand { get; set; }
    public IList<Shipment> Shipments { get; } = new List<Shipment>();
    public IList<Size> CurrentStock { get; } = new List<Size>();
}