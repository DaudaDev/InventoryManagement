using Blocks.Shared.ValueObjects;

namespace Equipments.Core.Domain.Equipment;

public class Vendor
{
    public EntityName? Name { get; private set; }
    public IList<Address> Addresses { get; private set; } = Array.Empty<Address>();

    public void SetName(string name)
    {
        Name = new (name);
    }
    
    public void AddAddress(string name)
    {
        Name = new(name);
    }
}