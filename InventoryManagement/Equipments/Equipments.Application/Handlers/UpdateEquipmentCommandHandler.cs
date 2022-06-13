using Blocks.Shared.Contracts.Infrastructure;
using CSharpFunctionalExtensions;
using Equipments.Application.Commands;
using Equipments.Application.Services.UpdateEquipment.Factory;
using Equipments.Core.Domain.Equipment;
using MediatR;

namespace Equipments.Application.Handlers;

public class UpdateEquipmentCommandHandler: IRequestHandler<UpdateEquipmentCommand, Result>
{
    private readonly IGeneralRepository<Equipment> _repository;
    private readonly IEquipmentUpdaterFactory _equipmentUpdaterFactory;
    
    public UpdateEquipmentCommandHandler(IGeneralRepository<Equipment> repository, IEquipmentUpdaterFactory equipmentUpdaterFactory)
    {
        _repository = repository;
        _equipmentUpdaterFactory = equipmentUpdaterFactory;
    }
    
    public async Task<Result> Handle(UpdateEquipmentCommand request, CancellationToken cancellationToken)
    {
        var equipment = await _repository.GetEntityById(request.InventoryId.ToString());

        if (equipment.IsFailure)
        {
            return Result.Failure(equipment.Error);
        }

        var equipmentUpdater = _equipmentUpdaterFactory.GetEquipmentUpdater(request.EquipmentUpdateType);

        equipmentUpdater.Value.PerformUpdate(equipment.Value, request);
        
        var result = await _repository.SaveEntity(equipment.Value);
        
        return result.Match(Result.Success, Result.Failure);
    }
}