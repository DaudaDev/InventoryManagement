using CSharpFunctionalExtensions;

namespace RawMaterials.Contracts.Repository;

public interface IRawMaterialsRepository<TRawMaterial>
{
    IEnumerable<TRawMaterial> GetAllInventoryItems();
    Result SaveInventoryItem(TRawMaterial inventoryItem);
    Result DeleteInventory(Guid inventorItemId);
}