using Blocks.MongoDb.Databases;
using Blocks.MongoDb.Facade;
using Blocks.Shared.Contracts.Infrastructure;
using Inventory.Core.Domain;
using Inventory.Infrastructure.Persistence;
using Inventory.Infrastructure.Persistence.Models;
using Inventory.Infrastructure.Persistence.Repository.MongoDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var configSection = configuration.GetSection("InventoryRepository");
        serviceCollection.Configure<InventoryRepositoryOptions>(configSection);
        serviceCollection.AddSingleton<IMongodbDatabase>(
            _ => new MongodbDatabase("mongodb://localhost:27017", "Blocks"));
        serviceCollection.AddSingleton<IMongoDbFacade<InventoryItemDto>, MongoDbFacade<InventoryItemDto>>();
        serviceCollection.AddSingleton<IGeneralRepository<InventoryItem>, MongoDbRepository>();
        return serviceCollection;
    }
}