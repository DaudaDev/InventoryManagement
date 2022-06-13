using CSharpFunctionalExtensions;
using Equipments.Application.Commands;
using Equipments.Application.Model;
using Equipments.Core.Domain.Equipment;

namespace Equipments.Application.Services.UpdateEquipment;

public interface IEquipmentUpdater
{
    public EquipmentUpdateType EquipmentUpdateType  { get;  }

    public void PerformUpdate(Equipment inventoryItem, UpdateEquipmentCommand updateEquipmentCommand);
}