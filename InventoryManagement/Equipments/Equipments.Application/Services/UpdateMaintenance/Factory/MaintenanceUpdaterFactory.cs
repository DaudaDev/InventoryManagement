using CSharpFunctionalExtensions;
using Equipments.Application.Model;

namespace Equipments.Application.Services.UpdateMaintenance.Factory;

public abstract class MaintenanceUpdaterFactory : IMaintenanceUpdaterFactory
{
    private readonly IEnumerable<IMaintenanceUpdater> _stockUpdaters;

    public MaintenanceUpdaterFactory(IEnumerable<IMaintenanceUpdater> stockUpdaters)
    {
        _stockUpdaters = stockUpdaters;
    }

    public Result<IMaintenanceUpdater> GetMaintenanceUpdater(MaintenanceUpdateType maintenanceUpdateType)
    {
        var stockUpdater = _stockUpdaters.FirstOrDefault(s => s.MaintenanceUpdateType == maintenanceUpdateType);

        return stockUpdater == null
            ? Result.Failure<IMaintenanceUpdater>($"StockUpdater with type {maintenanceUpdateType} not found")
            : Result.Success(stockUpdater);
    }
}