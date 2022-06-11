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

        var equipments = EquipmentsCore.Equipments.CreateEquipments(itemId, entityName, equipmentPrice, EquipmentType.Generator, purchaseDate) ;

        equipments.EntityId.Should().Be(itemId);
        equipments.EquipmentName.Should().BeEquivalentTo(entityName);
        equipments.EquipmentPrice.Should().BeEquivalentTo(equipmentPrice);
        equipments.EquipmentType.Should().Be(EquipmentType.Generator);
        equipments.PurchaseDate.Should().Be(purchaseDate);
        equipments.MaintenanceLogs.Count.Should().Be(0);
    }
}