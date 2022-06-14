using Microsoft.Extensions.DependencyInjection;
using Sales.Application.Services.UpdateSales;
using Sales.Application.Services.UpdateSales.Factory;
using Sales.Application.Services.UpdateSalesCost;
using Sales.Application.Services.UpdateSalesCost.Factory;

namespace Sales.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ISalesCostUpdaterFactory, SalesCostUpdaterFactory>();
        serviceCollection.AddScoped<ISalesCostUpdater, CreateSalesCost>();
        serviceCollection.AddScoped<ISalesCostUpdater, RemoveSalesCost>();
        
        serviceCollection.AddScoped<ISalesUpdaterFactory, SalesUpdaterFactory>();
        serviceCollection.AddScoped<ISalesUpdater, SalesAmountUpdater>();
        serviceCollection.AddScoped<ISalesUpdater, SalesCostPerUnitUpdater>();

        return serviceCollection;
    }
}