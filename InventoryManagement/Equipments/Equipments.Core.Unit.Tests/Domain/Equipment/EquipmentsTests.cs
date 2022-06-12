using AutoFixture;
using Blocks.Shared.ValueObjects;
using Equipments.Core.ValueObjects;
using FluentAssertions;
using EquipmentsCore = Equipments.Core.Domain.Equipment;
namespace Equipments.Core.Unit.Tests.Domain.Equipment;

public class EquipmentsTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void CreateCreateEquipments_Creates_Valid_CreateEquipments()
    {
        var itemId = _fixture.Create<Guid>();
        var entityName = _fixture.Create<EntityName>();
        var equipmentPrice = _fixture.Create<Money>();
        var purchaseDate = DateTimeOffset.Now;

        var equipments = EquipmentsCore.Equipment.CreateEquipments(itemId, entityName, equipmentPrice, EquipmentType.Generator, purchaseDate) ;

        equipments.EntityId.Should().Be(itemId);
        equipments.EquipmentName.Should().BeEquivalentTo(entityName);
        equipments.EquipmentPrice.Should().BeEquivalentTo(equipmentPrice);
        equipments.EquipmentType.Should().Be(EquipmentType.Generator);
        equipments.PurchaseDate.Should().Be(purchaseDate);
        equipments.MaintenanceLogs.Count.Should().Be(0);
    }
    
    [Fact]
    public void SetEquipmentName_Updates_EquipmentName()
    {
        var name = _fixture.Create<string>();
        var equipments = GetEquipments();

        equipments.SetEquipmentName(name);

        equipments.EquipmentName.Name.Should().BeEquivalentTo(name);
    }

    [Fact]
    public void SetEquipmentPrice_Updates_EquipmentPrice()
    {
        var money = _fixture.Create<Money>();
        var equipments = GetEquipments();

        equipments.SetEquipmentPrice(money);

        equipments.EquipmentPrice.Should().BeEquivalentTo(money);
    }
    
    [Fact]
    public void SetPurchaseDate_Updates_PurchaseDate()
    {
        var purchaseDate = DateTimeOffset.Now.AddDays(-2);
        var equipments = GetEquipments();

        equipments.SetPurchaseDate(purchaseDate);

        equipments.PurchaseDate.Should().Be(purchaseDate);
    }
    
    [Fact]
    public void SetMaterialType_Updates_MaterialType()
    {
        var equipments = GetEquipments();

        equipments.SetEquipmentType(EquipmentType.Tank);

        equipments.EquipmentType.Should().Be(EquipmentType.Tank);
    }
    
    [Fact]
    public void StartMaintenance_Updates_MaintenanceLogs_Count()
    {
        var name = _fixture.Create<string>();
        var vendor = _fixture.Create<EquipmentsCore.Vendor>();
        var startDate = DateTimeOffset.Now.AddHours(10);
        var cost = _fixture.Create<Money>();

        var equipments = GetEquipments();

        equipments.StartMaintenance(name, vendor, startDate, cost);

        equipments.MaintenanceLogs.Count.Should().Be(1);
    }
    
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void StartMaintenance_Updates_MaintenanceLogs_With_Correct_Values(bool isCostNull)
    {
        var name = _fixture.Create<string>();
        var vendor = _fixture.Create<EquipmentsCore.Vendor>();
        var startDate = DateTimeOffset.Now.AddHours(10);
        var cost = isCostNull ? null : _fixture.Create<Money>();

        var equipments = GetEquipments();

        equipments.StartMaintenance(name, vendor, startDate, cost);

        var  maintenanceLog = equipments.MaintenanceLogs.ElementAt(0);

        maintenanceLog.Name.Name.Should().Be(name);
        maintenanceLog.Vendor.Should().BeEquivalentTo(vendor);
        maintenanceLog.DateStarted.Should().Be(startDate);
        maintenanceLog.TotalCosts.Should().Be(cost);
    }
    
    [Fact]
    public void FinishMaintenance_EndDate_Before_StartDate_Returns_Failure()
    {
        var endDate = DateTimeOffset.Now.AddHours(-10);
        var equipments = GetEquipmentsWithLogs(2);
        var firstLog = equipments.MaintenanceLogs.ElementAt(0);
        
        var result = equipments.FinishMaintenance(firstLog.Id, endDate);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be($"The end date cannot be before the start date {endDate}");
    }
    
    [Fact]
    public void FinishMaintenance_Updates_Expected_EndDate()
    {
        var endDate = DateTimeOffset.Now.AddHours(10);
        var equipments = GetEquipmentsWithLogs(2);
        var secondLog = equipments.MaintenanceLogs.ElementAt(1);
        
        var result =  equipments.FinishMaintenance(secondLog.Id, endDate);

        result.IsSuccess.Should().BeTrue();
        equipments.MaintenanceLogs.ElementAt(1).DateEnded.Should().Be(endDate);
    }

    private static EquipmentsCore.Equipment GetEquipmentsWithLogs(int logCount)
    {
        var equipments = GetEquipments();
        for (var j = 0; j <= logCount; j++)
        {
            var maintenanceLog = EquipmentsCore.MaintenanceLog.CreateMaintenanceLog(Guid.NewGuid());
            maintenanceLog.SetDateStarted(DateTimeOffset.Now);
            equipments.MaintenanceLogs.Add(maintenanceLog);
        }
    
        return equipments;
    }

    private static EquipmentsCore.Equipment GetEquipments()
    {
        return EquipmentsCore.Equipment.CreateEquipments(Guid.NewGuid(), new EntityName(Guid.NewGuid().ToString()), new Money(Currency.NGN, 100), EquipmentType.Generator, DateTimeOffset.Now);
    }
}