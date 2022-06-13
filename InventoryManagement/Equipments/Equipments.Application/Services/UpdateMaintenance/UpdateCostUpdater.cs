using Equipments.Application.Commands;
using Equipments.Application.Model;
using Equipments.Core.Domain.Equipment;

namespace Equipments.Application.Services.UpdateMaintenance;

public class UpdateCostUpdater : IMaintenanceUpdater
{
    public MaintenanceUpdateType MaintenanceUpdateType => MaintenanceUpdateType.UpdateCost;
    public void PerformUpdate(Equipment equipment, UpdateMaintenanceCommand updateMaintenanceCommand)
    {
        equipment.UpdateMaintenanceCost(updateMaintenanceCommand.LogId, updateMaintenanceCommand.Money);
    }
}