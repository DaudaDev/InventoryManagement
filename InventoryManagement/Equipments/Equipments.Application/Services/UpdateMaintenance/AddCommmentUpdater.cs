using Equipments.Application.Commands;
using Equipments.Application.Model;
using Equipments.Core.Domain.Equipment;

namespace Equipments.Application.Services.UpdateMaintenance;

public class AddCommmentUpdater : IMaintenanceUpdater
{
    public MaintenanceUpdateType MaintenanceUpdateType => MaintenanceUpdateType.AddCommment;
    public void PerformUpdate(Equipment equipment, UpdateMaintenanceCommand updateMaintenanceCommand)
    {
        equipment.AddMaintenanceComment(updateMaintenanceCommand.LogId, updateMaintenanceCommand.Comment);
    }
}