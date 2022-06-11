using Blocks.MongoDb.Databases;
using Blocks.MongoDb.Facade;
using Blocks.Shared.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawMaterials.Core.Domain.RawMaterials;
using RawMaterials.Infrastructure.Models;
using RawMaterials.Infrastructure.Repository.MongoDB;

namespace RawMaterials.Infrastructure;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepository(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var configSection = configuration.GetSection("RawMaterialRepository");
        serviceCollection.Configure<RawMaterialRepositoryOptions>(configSection);
        serviceCollection.AddSingleton<IMongodbDatabase>(
            _ => new MongodbDatabase("mongodb://localhost:27017", "Blocks"));
        serviceCollection.AddSingleton<IMongoDbFacade<RawMaterialDto>, MongoDbFacade<RawMaterialDto>>();
        serviceCollection.AddSingleton<IGeneralRepository<RawMaterial>, MongoDbRepository>();
        return serviceCollection;
    }
}