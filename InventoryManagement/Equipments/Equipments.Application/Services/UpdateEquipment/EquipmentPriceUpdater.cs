using Equipments.Application.Commands;
using Equipments.Application.Model;
using Equipments.Core.Domain.Equipment;

namespace Equipments.Application.Services.UpdateEquipment;

public class EquipmentPriceUpdater : IEquipmentUpdater
{
    public EquipmentUpdateType EquipmentUpdateType => EquipmentUpdateType.EquipmentPrice;
    public void PerformUpdate(Equipment equipment, UpdateEquipmentCommand updateEquipmentCommand)
    {
        equipment.SetEquipmentPrice(updateEquipmentCommand.EquipmentPrice);
    }
}