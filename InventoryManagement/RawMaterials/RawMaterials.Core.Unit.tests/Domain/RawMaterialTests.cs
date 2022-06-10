using AutoFixture;
using Blocks.Shared.ValueObjects;
using FluentAssertions;
using RawMaterials.Core.Domain.RawMaterials;
using RawMaterials.Core.ValueObjects;

namespace RawMaterials.Core.Unit.tests.Domain;

public class RawMaterialTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void CreateInventoryItem_Creates_Valid_InventoryItem()
    {
        var itemId = _fixture.Create<Guid>();
        var materialType = _fixture.Create<MaterialType>();
        var brand = _fixture.Create<Brand>();

        var rawMaterial = RawMaterial.CreatRawMaterial(itemId, materialType, brand);

        rawMaterial.IsSuccess.Should().BeTrue();
        rawMaterial.Value.EntityId.Should().Be(itemId);
        rawMaterial.Value.Brand.Should().BeEquivalentTo(brand);
        rawMaterial.Value.Shipments.Count.Should().Be(0);
        rawMaterial.Value.CurrentStock.Count.Should().Be(0);
    }

    [Fact]
    public void SetBrand_Updates_Brand()
    {
        var brand = _fixture.Create<Brand>();
        var rawMaterial = GetRawMaterial();

        rawMaterial.SetBrand(brand);

        rawMaterial.Brand.Should().BeEquivalentTo(brand);
    }

    [Fact]
    public void SetMaterialType_Updates_MaterialType()
    {
        var materialType = _fixture.Create<MaterialType>();
        var rawMaterial = GetRawMaterial();

        rawMaterial.SetMaterialType(materialType);

        rawMaterial.MaterialType.Should().BeEquivalentTo(materialType);
    }

    [Fact]
    public void AddStock_Stock_Doest_not_Exist_Increments_Stock()
    {
        var stockSize = _fixture.Create<Size>();
        var rawMaterial = GetRawMaterial();

        rawMaterial.AddStock(stockSize);

        rawMaterial.CurrentStock.Count.Should().Be(1);
    }

    [Fact]
    public void AddStock_Stock_Exist_Updates_Stock()
    {
        var amount = 100;
        var stockSize = new Size
        {
            Amount = amount,
            Unit = Blocks.Shared.ValueObjects.Unit.Empty
        };

        var rawMaterial = GetRawMaterial();
        rawMaterial.CurrentStock.Add(new Size
        {
            Amount = amount,
            Unit = Blocks.Shared.ValueObjects.Unit.Empty
        });

        rawMaterial.AddStock(stockSize);

        rawMaterial.CurrentStock.Count.Should().Be(1);
        rawMaterial.CurrentStock.ElementAt(0).Amount.Should().Be(amount * 2);
    }

    [Fact]
    public void UseStock_Stock_Doest_not_Exist_Returns_Failure()
    {
        var stockSize = _fixture.Create<Size>();
        var rawMaterial = GetRawMaterial();

        var result = rawMaterial.UseStock(stockSize);

        result.IsFailure.Should().BeTrue();
    }
    
    [Fact]
    public void UseStock_Stock_Item_Amount_Lower_Than_Requested_Returns_Failure()
    {
        var amount = 100;
        var stockSize = new Size
        {
            Amount = amount,
            Unit = Blocks.Shared.ValueObjects.Unit.Empty
        };

        var rawMaterial = GetRawMaterial();
        rawMaterial.CurrentStock.Add(new Size
        {
            Amount = 10,
            Unit = Blocks.Shared.ValueObjects.Unit.Empty
        });

        var result = rawMaterial.UseStock(stockSize);

        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public void UseStock_Stock_Exist_Updates_Stock()
    {
        var amount = 100;
        var stockSize = new Size
        {
            Amount = amount,
            Unit = Blocks.Shared.ValueObjects.Unit.Empty
        };

        var rawMaterial = GetRawMaterial();
        rawMaterial.CurrentStock.Add(new Size
        {
            Amount = amount,
            Unit = Blocks.Shared.ValueObjects.Unit.Empty
        });

        var result = rawMaterial.UseStock(stockSize);

        result.IsSuccess.Should().BeTrue();
        rawMaterial.CurrentStock.ElementAt(0).Amount.Should().Be(0);
    }
    
    [Fact]
    public void ReceiveShipment_Stock_Doest_not_Exist_Increments_Shipment()
    {
        var stockSize = _fixture.Create<Size>();
        var money = _fixture.Create<Money>();
        var vendor = _fixture.Create<Vendor>();

        var rawMaterial = GetRawMaterial();

        rawMaterial.ReceiveShipment(stockSize, money, vendor);

        rawMaterial.CurrentStock.Count.Should().Be(1);
        rawMaterial.Shipments.Count.Should().Be(1);
    }
    
    [Fact]
    public void ReceiveShipment_Stock_Doest_not_Exist_Adds_Expected_Values()
    {
        var stockSize = _fixture.Create<Size>();
        var money = _fixture.Create<Money>();
        var vendor = _fixture.Create<Vendor>();

        var rawMaterial = GetRawMaterial();

        rawMaterial.ReceiveShipment(stockSize, money, vendor);

        var shipment = rawMaterial.Shipments.ElementAt(0);
        shipment.Size.Should().BeEquivalentTo(stockSize);
        shipment.ShipmentCost.Should().BeEquivalentTo(money);
        shipment.Vendor.Should().BeEquivalentTo(vendor);
    }
    
    [Fact]
    public void ReceiveShipment_Stock_Exist_Updates_Stock()
    {
        var money = _fixture.Create<Money>();
        var vendor = _fixture.Create<Vendor>();
        var amount = 100;
        var stockSize = new Size
        {
            Amount = amount,
            Unit = Blocks.Shared.ValueObjects.Unit.Empty
        };

        var rawMaterial = GetRawMaterial();
        rawMaterial.CurrentStock.Add(new Size
        {
            Amount = amount,
            Unit = Blocks.Shared.ValueObjects.Unit.Empty
        });

        rawMaterial.ReceiveShipment(stockSize, money, vendor);

        rawMaterial.CurrentStock.Count.Should().Be(1);
        rawMaterial.CurrentStock.ElementAt(0).Amount.Should().Be(amount * 2);
    }
    
    [Fact]
    public void ReceiveShipment_Stock_Does_Not_Exist_Adds_Stock()
    {
        var money = _fixture.Create<Money>();
        var vendor = _fixture.Create<Vendor>();
        var amount = 100;
        var stockSize = new Size
        {
            Amount = amount,
            Unit = Blocks.Shared.ValueObjects.Unit.Kilograms
        };

        var rawMaterial = GetRawMaterial();
        rawMaterial.CurrentStock.Add(new Size
        {
            Amount = amount,
            Unit = Blocks.Shared.ValueObjects.Unit.Empty
        });

        rawMaterial.ReceiveShipment(stockSize, money, vendor);

        rawMaterial.CurrentStock.Count.Should().Be(2);
        rawMaterial.CurrentStock.ElementAt(1).Amount.Should().Be(amount);
    }
    
    private static RawMaterial GetRawMaterial()
    {
        return RawMaterial.CreatRawMaterial(Guid.NewGuid(), new MaterialType(Guid.NewGuid().ToString()),
            new Brand(Guid.NewGuid().ToString())).Value;
    }
}