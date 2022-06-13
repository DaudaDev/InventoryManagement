using Equipments.Application.Commands;
using Equipments.Application.Model;
using Equipments.Core.Domain.Equipment;

namespace Equipments.Application.Services.UpdateEquipment;

public class PurchaseDateUpdater : IEquipmentUpdater
{
    public EquipmentUpdateType EquipmentUpdateType => EquipmentUpdateType.PurchaseDate;
    public void PerformUpdate(Equipment equipment, UpdateEquipmentCommand updateEquipmentCommand)
    {
        equipment.SetEquipmentType(updateEquipmentCommand.EquipmentType);
    }
}