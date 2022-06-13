using Blocks.Shared.Contracts.Infrastructure;
using CSharpFunctionalExtensions;
using Equipments.Application.Commands;
using Equipments.Core.Domain.Equipment;
using MediatR;

namespace Equipments.Application.Handlers;

public class FinishMaintenanceCommandHandler : IRequestHandler<FinishMaintenanceCommand, Result>
{
    private readonly IGeneralRepository<Equipment> _repository;

    public FinishMaintenanceCommandHandler(IGeneralRepository<Equipment> repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(FinishMaintenanceCommand request, CancellationToken cancellationToken)
    {
        var equipment = await _repository.GetEntityById(request.EquipmentId.ToString());

        if (equipment.IsFailure)
        {
            return Result.Failure(equipment.Error);
        }

        equipment.Value.FinishMaintenance(request.LogId, request.EndDate);

        var result = await _repository.SaveEntity(equipment.Value);

        return result.Match(() => equipment, Result.Failure);
    }
}