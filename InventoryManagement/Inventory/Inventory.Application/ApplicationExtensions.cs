using Inventory.Application.Services.UpdateStock;
using Inventory.Application.Services.UpdateStock.Factory;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IStockUpdaterFactory, StockUpdaterFactory>();
        serviceCollection.AddScoped<IStockUpdater, DepositStockUpdater>();
        serviceCollection.AddScoped<IStockUpdater, WithdrawStockUpdater>();
        serviceCollection.AddScoped<IStockUpdater, CostPerUnitStockUpdater>();
        serviceCollection.AddScoped<IStockUpdater, PricePerUnitStockUpdater>();

        return serviceCollection;
    }
}