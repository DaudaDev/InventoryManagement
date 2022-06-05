using Blocks.Shared.Aggregates;
using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using Equipments.Core.ValueObjects;

namespace Equipments.Core.Domain.Equipment;

public class Equipments : AggregateEntity
{
    public EntityName? EquipmentName { get; private set; }
    public IList<MaintenanceLog> MaintenanceLogs { get; private set; } = Array.Empty<MaintenanceLog>();
    public DateTimeOffset PurchaseDate { get; private set; }
    public Money? EquipmentPrice { get; private set; }
    public EquipmentType EquipmentType { get; private set; }

    public void SetEquipmentName(string name)
    {
        EquipmentName = new(name);
    }

    public void SetEquipmentType(EquipmentType equipmentType)
    {
        EquipmentType = equipmentType;
    }

    public void SetEquipmentPrice(Money money)
    {
        EquipmentPrice = money;
    }

    public void SetPurchaseDate(DateTimeOffset purchaseDate)
    {
        PurchaseDate = purchaseDate;
    }

    public void StartMaintainance(string name, Vendor vendor, DateTimeOffset? startDate = null, Money? cost = null)
    {
        var maintainanceLog = new MaintenanceLog(Guid.NewGuid());

        maintainanceLog.SetVendor(vendor);
        maintainanceLog.SetDateStarted(startDate);
        maintainanceLog.SetName(name);

        if (cost is not null)
        {
            maintainanceLog.AddCost(cost.Currency, cost.Amount);
        }

        MaintenanceLogs.Add(maintainanceLog);
    }

    public void FinishMaintainance(Guid logId, DateTimeOffset? endDate = null)
    {
        var result = GetMaintenanceLog(logId);

        result.Match(
            maintenanceLog => maintenanceLog.SetDateEnded(endDate),
            error => Console.WriteLine(error));
    
    }

    public void UpdateMaintenanceCost(Guid logId, Money money)
    {
        var result = GetMaintenanceLog(logId);

        result.Match(
            maintenanceLog => maintenanceLog.UpdateCost(money.Currency, money.Amount),
            error => Console.WriteLine(error));
       
    }

    public void AddMaintenanceComment(Guid logId, string comment)
    {
        var result = GetMaintenanceLog(logId);

        result.Match(
            maintenanceLog => maintenanceLog.AddComment(comment),
            error => Console.WriteLine(error));
    }

    public void UpdateComment(Guid logId, Guid commentId, string commentText)
    {
        var result = GetMaintenanceLog(logId);

        result.Match(
            maintenanceLog => maintenanceLog.UpdateComment(commentId, commentText),
            error => Console.WriteLine(error));
    }

    private Result<MaintenanceLog> GetMaintenanceLog(Guid logId)
    {
        var maintenanceLog = MaintenanceLogs.SingleOrDefault(log => log.Id == logId);

        if (maintenanceLog is null)
        {
            return Result.Failure<MaintenanceLog>($"Maintenance Log with ID {logId} cannot be found");
        }

        return Result.Success(maintenanceLog);
    }
}