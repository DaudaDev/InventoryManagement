using AutoFixture;
using Blocks.Shared.ValueObjects;
using FluentAssertions;
using Inventory.Core.Domain;

namespace Inventory.Core.Unit.Tests.Domain;

public class InventoryItemTests
{
    private Fixture _fixture = new();

    [Fact]
    public void CreateInventoryItem_Creates_Valid_InventoryItem()
    {
        var itemId = _fixture.Create<Guid>();
        var entityName = _fixture.Create<EntityName>();

        var inventoryItem = InventoryItem.CreateInventoryItem(itemId, entityName);

        inventoryItem.IsSuccess.Should().BeTrue();
        inventoryItem.Value.EntityId.Should().Be(itemId);
        inventoryItem.Value.Name.Should().BeEquivalentTo(entityName);
    }

    [Fact]
    public void SetName_Updates_Entity_Name()
    {
        var entityName = _fixture.Create<EntityName>();
        var inventoryItem = GetInventoryItem();

        inventoryItem.SetName(entityName);

        inventoryItem.Name.Should().BeEquivalentTo(entityName);
    }

    [Fact]
    public void AddStockEntry_Adds_Stock_Entry()
    {
        var numberOfItems = _fixture.Create<Size>();
        var pricePerUnit = _fixture.Create<Money>();
        var costPerUnit = _fixture.Create<Money>();
        var inventoryItem = GetInventoryItem();

        var result = inventoryItem.AddStockEntry(numberOfItems, costPerUnit, pricePerUnit);
        
        result.IsSuccess.Should().BeTrue();
        inventoryItem.CurrentStock.Count.Should().Be(1);
    }

    [Fact]
    public void AddStockEntry_Adds_Stock_Entry_With_The_Correct_Values()
    {
        var numberOfItems = _fixture.Create<Size>();
        var pricePerUnit = _fixture.Create<Money>();
        var costPerUnit = _fixture.Create<Money>();
        var inventoryItem = GetInventoryItem();

        var result = inventoryItem.AddStockEntry(numberOfItems, costPerUnit, pricePerUnit);

        result.IsSuccess.Should().BeTrue();
        var stock = inventoryItem.CurrentStock.ElementAt(0);
        stock.NumberOfItems.CurrentValue.Should().BeEquivalentTo(numberOfItems);
        stock.PricePerUnit.CurrentValue.Should().BeEquivalentTo(pricePerUnit);
        stock.CostPerUnit.CurrentValue.Should().BeEquivalentTo(costPerUnit);
    }
    
    [Fact]
    public void AddStockEntry_Stock_Entry_Exists_Returns_Failure()
    {
        var amount = 1000;
        var pricePerUnit = _fixture.Create<Money>();
        var costPerUnit = _fixture.Create<Money>();
        var inventoryItem = GetInventoryItem();
        AddInventoryItemEntry(amount, inventoryItem, Blocks.Shared.ValueObjects.Unit.Empty);

        var result = inventoryItem.AddStockEntry(new Size
        {
            Amount = amount,
            Unit = Blocks.Shared.ValueObjects.Unit.Empty
        }, costPerUnit, pricePerUnit);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("An item of size Unit { description = Empty } already exists, try updating it");
    }

    [Fact]
    public void AddItemToStockEntry_Updates_Stock_Entry()
    {
        var amount = 1000;
        var inventoryItem = GetInventoryItem();
        AddInventoryItemEntry(amount, inventoryItem, Blocks.Shared.ValueObjects.Unit.Empty);

        var result = inventoryItem.AddItemToStockEntry(new Size
        {
            Amount = amount,
            Unit = Blocks.Shared.ValueObjects.Unit.Empty
        });

        var stock = inventoryItem.CurrentStock.ElementAt(0);
        result.IsSuccess.Should().BeTrue();
        stock.NumberOfItems.CurrentValue.Amount.Should()
            .Be(amount * 2);
    }
    
    [Fact]
    public void AddItemToStockEntry_Stock_Does_Not_Exist_Returns_Failure()
    {
        var amount = 1000;
        var inventoryItem = GetInventoryItem();
        AddInventoryItemEntry(amount, inventoryItem, Blocks.Shared.ValueObjects.Unit.Empty);

        var result = inventoryItem.AddItemToStockEntry(new Size
        {
            Amount = amount,
            Unit = Blocks.Shared.ValueObjects.Unit.Bags
        });

        result.IsFailure.Should().BeTrue();
    }
    
    [Fact]
    public void WithdrawItemFromStockEntry_Stock_Does_Not_Exist_Returns_Failure()
    {
        var amount = 1000;
        var inventoryItem = GetInventoryItem();
        AddInventoryItemEntry(amount, inventoryItem, Blocks.Shared.ValueObjects.Unit.Empty);

        var result = inventoryItem.WithdrawItemFromStockEntry(new Size
        {
            Amount = amount,
            Unit = Blocks.Shared.ValueObjects.Unit.Bags
        });

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Stock with size Unit { description = Bags } does not exist, please add it first");
    }
    
    [Fact]
    public void WithdrawItemFromStockEntry_Stock_Exist_Returns_Successful()
    {
        var amount = 1000;
        var inventoryItem = GetInventoryItem();
        AddInventoryItemEntry(amount, inventoryItem, Blocks.Shared.ValueObjects.Unit.Empty);

        var result = inventoryItem.WithdrawItemFromStockEntry(new Size
        {
            Amount = amount,
            Unit = Blocks.Shared.ValueObjects.Unit.Empty
        });

        var stock = inventoryItem.CurrentStock.ElementAt(0);
        result.IsSuccess.Should().BeTrue();
        stock.NumberOfItems.CurrentValue.Amount.Should().Be(0);
    }

    [Fact]
    public void WithdrawItemFromStockEntry_Stock_Amount_Not_Enough_Returns_Failure()
    {
        var amount = 1000;
        var inventoryItem = GetInventoryItem();
        AddInventoryItemEntry(100, inventoryItem, Blocks.Shared.ValueObjects.Unit.Empty);

        var result = inventoryItem.WithdrawItemFromStockEntry(new Size
        {
            Amount = amount,
            Unit = Blocks.Shared.ValueObjects.Unit.Empty
        });

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("There are only 100 items remaining");
    }
    
    [Fact]
    public void UpdateItemCostPerUnit_Updates_Stock_Entry()
    {
        var amount = 1000;
        var newAmount = 10000;
        var money = new Money(Currency.NGN, amount);
        var inventoryItem = GetInventoryItem();
        AddInventoryItemEntry(amount, inventoryItem, Blocks.Shared.ValueObjects.Unit.Empty, money);

        var result = inventoryItem
            .UpdateItemCostPerUnit(Blocks.Shared.ValueObjects.Unit.Empty, new Money(Currency.NGN, newAmount));

        var stock = inventoryItem.CurrentStock.ElementAt(0);
        result.IsSuccess.Should().BeTrue();
        stock.CostPerUnit.CurrentValue.Amount.Should()
            .Be(newAmount);
    }
    
    [Fact]
    public void UpdateItemCostPerUnit_Stock_Does_Not_Exist_Returns_Failure()
    {
        var amount = 1000;
        var newAmount = 10000;
        var money = new Money(Currency.NGN, amount);
        var inventoryItem = GetInventoryItem();
        AddInventoryItemEntry(amount, inventoryItem, Blocks.Shared.ValueObjects.Unit.Empty, money);

        var result = inventoryItem
            .UpdateItemCostPerUnit(Blocks.Shared.ValueObjects.Unit.Bags, new Money(Currency.NGN, newAmount));

        result.IsFailure.Should().BeTrue();
    }
    
    [Fact]
    public void UpdateItemPricePerUnit_Updates_Stock_Entry()
    {
        var amount = 1000;
        var newAmount = 10000;
        var money = new Money(Currency.NGN, amount);
        var inventoryItem = GetInventoryItem();
        AddInventoryItemEntry(amount, inventoryItem, Blocks.Shared.ValueObjects.Unit.Empty, null, money);

        var result = inventoryItem
            .UpdateItemPricePerUnit(Blocks.Shared.ValueObjects.Unit.Empty, new Money(Currency.NGN, newAmount));

        var stock = inventoryItem.CurrentStock.ElementAt(0);
        result.IsSuccess.Should().BeTrue();
        stock.PricePerUnit.CurrentValue.Amount.Should()
            .Be(newAmount);
    }
    
    [Fact]
    public void UpdateItemPricePerUnit_Stock_Does_Not_Exist_Returns_Failure()
    {
        var amount = 1000;
        var newAmount = 10000;
        var money = new Money(Currency.NGN, amount);
        var inventoryItem = GetInventoryItem();
        AddInventoryItemEntry(amount, inventoryItem, Blocks.Shared.ValueObjects.Unit.Empty, null, money);

        var result = inventoryItem
            .UpdateItemPricePerUnit(Blocks.Shared.ValueObjects.Unit.Bags, new Money(Currency.NGN, newAmount));

        result.IsFailure.Should().BeTrue();
    }
    
    private void AddInventoryItemEntry(int amount, InventoryItem inventoryItem, Blocks.Shared.ValueObjects.Unit empty,
        Money? costPerUnit = null, Money? pricePerUnit = null)
    {
        var numberOfItems = new Size
        {
            Amount = amount,
            Unit = empty
        };
        pricePerUnit ??= _fixture.Create<Money>();
        costPerUnit ??= _fixture.Create<Money>();
        var inventoryItemEntry = InventoryItemEntry
            .CreateInventoryItemEntry(numberOfItems, costPerUnit, pricePerUnit).Value;
        inventoryItem.CurrentStock = new List<InventoryItemEntry> { inventoryItemEntry };
    }

    private static InventoryItem GetInventoryItem()
    {
        return InventoryItem.CreateInventoryItem(Guid.NewGuid(), new EntityName(Guid.NewGuid().ToString())).Value;
    }
}