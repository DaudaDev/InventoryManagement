using CSharpFunctionalExtensions;

namespace Inventory.Contracts.Repository;

public interface IInventoryRepository<TInventory>
{
    IEnumerable<TInventory> GetAllInventoryItems();
    Result SaveInventoryItem(TInventory inventoryItem);
    Result DeleteInventory(Guid inventorItemId);
}