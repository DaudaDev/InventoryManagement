using CSharpFunctionalExtensions;
using Equipments.Application.Model;

namespace Equipments.Application.Services.UpdateEquipment.Factory;

public abstract class EquipmentUpdaterFactory : IEquipmentUpdaterFactory
{
    private readonly IEnumerable<IEquipmentUpdater> _stockUpdaters;

    public EquipmentUpdaterFactory(IEnumerable<IEquipmentUpdater> stockUpdaters)
    {
        _stockUpdaters = stockUpdaters;
    }

    public Result<IEquipmentUpdater> GetEquipmentUpdater(EquipmentUpdateType equipmentUpdateType)
    {
        var stockUpdater = _stockUpdaters.FirstOrDefault(s => s.EquipmentUpdateType == equipmentUpdateType);

        return stockUpdater == null
            ? Result.Failure<IEquipmentUpdater>($"StockUpdater with type {equipmentUpdateType} not found")
            : Result.Success(stockUpdater);
    }
}