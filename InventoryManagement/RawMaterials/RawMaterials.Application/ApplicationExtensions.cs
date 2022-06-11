using Microsoft.Extensions.DependencyInjection;
using RawMaterials.Application.Services.UpdateStock;
using RawMaterials.Application.Services.UpdateStock.Factory;

namespace RawMaterials.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IStockUpdaterFactory, StockUpdaterFactory>();
        serviceCollection.AddScoped<IStockUpdater, UseStockUpdater>();
        serviceCollection.AddScoped<IStockUpdater, AddStockUpdater>();

        return serviceCollection;
    }
}