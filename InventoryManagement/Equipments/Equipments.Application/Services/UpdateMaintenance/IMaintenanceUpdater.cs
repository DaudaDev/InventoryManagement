using Equipments.Application.Commands;
using Equipments.Application.Model;
using Equipments.Core.Domain.Equipment;

namespace Equipments.Application.Services.UpdateMaintenance;

public interface IMaintenanceUpdater
{
    public MaintenanceUpdateType MaintenanceUpdateType  { get;  }

    public void PerformUpdate(Equipment inventoryItem, UpdateMaintenanceCommand updateMaintenanceCommand);
}