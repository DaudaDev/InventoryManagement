using Equipments.Application.Commands;
using Equipments.Application.Model;
using Equipments.Core.Domain.Equipment;

namespace Equipments.Application.Services.UpdateEquipment;

public class EquipmentNameUpdater : IEquipmentUpdater
{
    public EquipmentUpdateType EquipmentUpdateType => EquipmentUpdateType.EquipmentName;
    public void PerformUpdate(Equipment equipment, UpdateEquipmentCommand updateEquipmentCommand)
    {
        equipment.SetEquipmentName(updateEquipmentCommand.EquipmentName.Name);
    }
}