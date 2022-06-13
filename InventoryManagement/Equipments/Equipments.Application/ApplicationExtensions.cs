using Equipments.Application.Services.UpdateEquipment;
using Equipments.Application.Services.UpdateEquipment.Factory;
using Equipments.Application.Services.UpdateMaintenance;
using Equipments.Application.Services.UpdateMaintenance.Factory;
using Microsoft.Extensions.DependencyInjection;

namespace Equipments.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IEquipmentUpdaterFactory, EquipmentUpdaterFactory>();
        serviceCollection.AddScoped<IEquipmentUpdater, EquipmentTypeUpdater>();
        serviceCollection.AddScoped<IEquipmentUpdater, EquipmentPriceUpdater>();
        serviceCollection.AddScoped<IEquipmentUpdater, EquipmentNameUpdater>();
        serviceCollection.AddScoped<IEquipmentUpdater, PurchaseDateUpdater>();
        
        serviceCollection.AddScoped<IMaintenanceUpdaterFactory, MaintenanceUpdaterFactory>();
        serviceCollection.AddScoped<IMaintenanceUpdater, AddCommmentUpdater>();
        serviceCollection.AddScoped<IMaintenanceUpdater, UpdateCommentUpdater>();
        serviceCollection.AddScoped<IMaintenanceUpdater, UpdateCostUpdater>();
        return serviceCollection;
    }
}