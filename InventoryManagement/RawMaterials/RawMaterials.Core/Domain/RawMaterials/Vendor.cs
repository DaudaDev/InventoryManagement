using Blocks.Shared.ValueObjects;

namespace RawMaterials.Core.Domain.RawMaterials;

public class Vendor
{
    public EntityName? Name { get; init; }
    public IList<Address> Addresses { get; init; } = new List<Address>();
}