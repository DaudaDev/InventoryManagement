using Blocks.MongoDb.Databases;
using Blocks.MongoDb.Facade;
using Blocks.Shared.Contracts.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sales.Core.Domain;
using Sales.Infrastructure.Models;
using Sales.Infrastructure.Repository.MongoDB;

namespace Sales.Infrastructure;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepository(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var configSection = configuration.GetSection("ProductionRepository");
        serviceCollection.Configure<SalesRepositoryOptions>(configSection);
        serviceCollection.AddSingleton<IMongodbDatabase>(
            _ => new MongodbDatabase("mongodb://localhost:27017", "Blocks"));
        serviceCollection.AddSingleton<IMongoDbFacade<SalesDto>, MongoDbFacade<SalesDto>>();
        serviceCollection.AddSingleton<IGeneralRepository<SalesEntity>, MongoDbRepository>();
        return serviceCollection;
    }
}