using CSharpFunctionalExtensions;
using Equipments.Application.Model;

namespace Equipments.Application.Services.UpdateMaintenance.Factory;

public interface IMaintenanceUpdaterFactory
{
    Result<IMaintenanceUpdater> GetMaintenanceUpdater(MaintenanceUpdateType maintenanceUpdateType);
}