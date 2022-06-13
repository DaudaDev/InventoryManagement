using Equipments.Application.Commands;
using Equipments.Application.Model;
using Equipments.Core.Domain.Equipment;

namespace Equipments.Application.Services.UpdateEquipment;

public class EquipmentTypeUpdater : IEquipmentUpdater
{
    public EquipmentUpdateType EquipmentUpdateType => EquipmentUpdateType.EquipmentType;
    public void PerformUpdate(Equipment equipment, UpdateEquipmentCommand updateEquipmentCommand)
    {
        equipment.SetEquipmentType(updateEquipmentCommand.EquipmentType);
    }
}