using Blocks.MongoDb.Models;
using Blocks.Shared.ValueObjects;
using Production.Core.Domain;
using Production.Core.ValueObjects;

namespace Production.Infrastructure.Models;

public class ProductionDto : MongoDbBaseDocument
{
    public ProductType ProductType { get; set; }
    public Period Date { get; set; }
    public Size Quantity { get; set; }
    public IList<ProductionCost> ProductionCosts { get; set; } = new List<ProductionCost>();
}