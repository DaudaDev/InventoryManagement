using Blocks.Shared.Aggregates;
using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using Equipments.Core.ValueObjects;

namespace Equipments.Core.Domain.Equipment;

public class Equipment : AggregateEntity
{
    public EntityName EquipmentName { get; private set; }
    public IList<MaintenanceLog> MaintenanceLogs { get; private set; } = new List<MaintenanceLog>();
    public DateTimeOffset PurchaseDate { get; private set; }
    public Money EquipmentPrice { get; private set; }
    public EquipmentType EquipmentType { get; private set; }

    private Equipment(Guid equipmentId, EntityName equipmentName, Money equipmentPrice, EquipmentType equipmentType,
        DateTimeOffset purchaseDate)
    {
        EquipmentName = equipmentName;
        EquipmentPrice = equipmentPrice;
        EquipmentType = equipmentType;
        PurchaseDate = purchaseDate;
        EntityId = equipmentId;
    }
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

    public Result StartMaintenance(string name, Vendor vendor, DateTimeOffset startDate ,Money? cost = null)
    {
        var maintainanceLog =  MaintenanceLog.CreateMaintenanceLog(Guid.NewGuid());

        maintainanceLog.SetVendor(vendor);
        maintainanceLog.SetDateStarted(startDate);
        maintainanceLog.SetName(name);

        if (cost is not null)
        {
            maintainanceLog.AddCost(cost.Currency, cost.Amount);
        }

        MaintenanceLogs.Add(maintainanceLog);
        return Result.Success();

    }

    public Result FinishMaintenance(Guid logId, DateTimeOffset endDate)
    {
        var result = GetMaintenanceLog(logId);

        return result.Match(
            maintenanceLog => maintenanceLog.SetDateEnded(endDate),
            Result.Failure);
    
    }

    public Result UpdateMaintenanceCost(Guid logId, Money money)
    {
        var result = GetMaintenanceLog(logId);

        return  result.Match(
            maintenanceLog => maintenanceLog.UpdateCost(money.Currency, money.Amount),
            Result.Failure);
       
    }

    public Result AddMaintenanceComment(Guid logId, string comment)
    {
        var result = GetMaintenanceLog(logId);

        return result.Match(
            maintenanceLog => maintenanceLog.AddComment(comment),
            Result.Failure);
    }

    public Result UpdateComment(Guid logId, Guid commentId, string commentText)
    {
        var result = GetMaintenanceLog(logId);

        return result.Match(
            maintenanceLog => maintenanceLog.UpdateComment(commentId, commentText),
            Result.Failure);
    }

    private Result<MaintenanceLog> GetMaintenanceLog(Guid logId)
    {
        var maintenanceLog = MaintenanceLogs.SingleOrDefault(log => log.Id == logId);

        return maintenanceLog is null 
            ? Result.Failure<MaintenanceLog>($"Maintenance Log with ID {logId} cannot be found")
            : Result.Success(maintenanceLog);
    }

    public static Equipment CreateEquipments(
        Guid equipmentId,
        EntityName equipmentName,
        Money equipmentPrice,
        EquipmentType equipmentType,
        DateTimeOffset purchaseDate)
    {
        return new Equipment(equipmentId, equipmentName, equipmentPrice, equipmentType, purchaseDate);
    }
}