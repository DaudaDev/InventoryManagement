using Blocks.Shared.Contracts.Infrastructure;
using CSharpFunctionalExtensions;
using Equipments.Application.Commands;
using Equipments.Application.Services.UpdateEquipment.Factory;
using Equipments.Application.Services.UpdateMaintenance.Factory;
using Equipments.Core.Domain.Equipment;
using MediatR;

namespace Equipments.Application.Handlers;

public class UpdateMaintenanceCommandHandler: IRequestHandler<UpdateMaintenanceCommand, Result>
{
    private readonly IGeneralRepository<Equipment> _repository;
    private readonly IMaintenanceUpdaterFactory _maintenanceUpdaterFactory;
    
    public UpdateMaintenanceCommandHandler(IGeneralRepository<Equipment> repository, IMaintenanceUpdaterFactory maintenanceUpdaterFactory)
    {
        _repository = repository;
        _maintenanceUpdaterFactory = maintenanceUpdaterFactory;
    }
    
    public async Task<Result> Handle(UpdateMaintenanceCommand request, CancellationToken cancellationToken)
    {
        var equipment = await _repository.GetEntityById(request.EquipmentId.ToString());

        if (equipment.IsFailure)
        {
            return Result.Failure(equipment.Error);
        }

        var equipmentUpdater = _maintenanceUpdaterFactory.GetMaintenanceUpdater(request.MaintenanceUpdateType);

        equipmentUpdater.Value.PerformUpdate(equipment.Value, request);
        
        var result = await _repository.SaveEntity(equipment.Value);
        
        return result.Match(Result.Success, Result.Failure);
    }
}