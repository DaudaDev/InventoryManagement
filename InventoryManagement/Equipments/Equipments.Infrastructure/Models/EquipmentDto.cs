using Blocks.MongoDb.Models;
using Blocks.Shared.ValueObjects;
using Equipments.Core.Domain.Equipment;
using Equipments.Core.ValueObjects;

namespace Equipments.Infrastructure.Models;

public class EquipmentDto : MongoDbBaseDocument
{
    public EntityName EquipmentName { get; private set; }
    public IList<MaintenanceLog> MaintenanceLogs { get; private set; } = new List<MaintenanceLog>();
    public DateTimeOffset PurchaseDate { get; private set; }
    public Money EquipmentPrice { get; private set; }
    public EquipmentType EquipmentType { get; private set; }
}