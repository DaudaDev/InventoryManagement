using Microsoft.Extensions.DependencyInjection;
using Production.Application.Services.UpdateProduction;
using Production.Application.Services.UpdateProduction.Factory;
using Production.Application.Services.UpdateProductionCost;
using Production.Application.Services.UpdateProductionCost.Factory;

namespace Production.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IProductionCostUpdaterFactory, ProductionCostUpdaterFactory>();
        serviceCollection.AddScoped<IProductionCostUpdater, CreateProductionCost>();
        serviceCollection.AddScoped<IProductionCostUpdater, RemoveProductionCost>();
        
        serviceCollection.AddScoped<IProductionUpdaterFactory, ProductionUpdaterFactory>();
        serviceCollection.AddScoped<IProductionUpdater, ProductionDateUpdater>();
        serviceCollection.AddScoped<IProductionUpdater, ProductionQuantityUpdater>();

        return serviceCollection;
    }
}