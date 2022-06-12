using Blocks.MongoDb.Databases;
using Blocks.MongoDb.Facade;
using Blocks.Shared.Contracts.Infrastructure;
using Equipments.Core.Domain.Equipment;
using Equipments.Infrastructure.Models;
using Equipments.Infrastructure.Repository.MongoDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Equipments.Infrastructure;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepository(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var configSection = configuration.GetSection("EquipmentRepository");
        serviceCollection.Configure<EquipmentsRepositoryOptions>(configSection);
        serviceCollection.AddSingleton<IMongodbDatabase>(
            _ => new MongodbDatabase("mongodb://localhost:27017", "Blocks"));
        serviceCollection.AddSingleton<IMongoDbFacade<EquipmentDto>, MongoDbFacade<EquipmentDto>>();
        serviceCollection.AddSingleton<IGeneralRepository<Equipment>, MongoDbRepository>();
        return serviceCollection;
    }
}