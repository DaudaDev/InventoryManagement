using Blocks.MongoDb.Models;
using Blocks.Shared.ValueObjects;
using Sales.Core.Domain;

namespace Sales.Infrastructure.Models;

public class SalesDto : MongoDbBaseDocument
{
    public EntityName Name { get; set; }
    public Period SalesPeriod { get; set; }
    public IList<SalesCost> SalesCosts { get; set; } = new List<SalesCost>();
}