using Blocks.MongoDb.Databases;
using Blocks.MongoDb.Facade;
using Blocks.Shared.Contracts.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Production.Core.Domain;
using Production.Infrastructure.Models;
using Production.Infrastructure.Repository.MongoDB;

namespace Production.Infrastructure;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepository(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var configSection = configuration.GetSection("ProductionRepository");
        serviceCollection.Configure<ProductionRepositoryOptions>(configSection);
        serviceCollection.AddSingleton<IMongodbDatabase>(
            _ => new MongodbDatabase("mongodb://localhost:27017", "Blocks"));
        serviceCollection.AddSingleton<IMongoDbFacade<ProductionDto>, MongoDbFacade<ProductionDto>>();
        serviceCollection.AddSingleton<IGeneralRepository<ProductionEntity>, MongoDbRepository>();
        return serviceCollection;
    }
}