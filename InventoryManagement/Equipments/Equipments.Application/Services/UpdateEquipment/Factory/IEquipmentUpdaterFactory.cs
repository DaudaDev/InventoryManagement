using CSharpFunctionalExtensions;
using Equipments.Application.Model;

namespace Equipments.Application.Services.UpdateEquipment.Factory;

public interface IEquipmentUpdaterFactory
{
    Result<IEquipmentUpdater> GetEquipmentUpdater(EquipmentUpdateType equipmentUpdateType);
}