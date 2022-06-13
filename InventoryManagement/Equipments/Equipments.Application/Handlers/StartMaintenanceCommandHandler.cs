using Blocks.Shared.Contracts.Infrastructure;
using CSharpFunctionalExtensions;
using Equipments.Application.Commands;
using Equipments.Core.Domain.Equipment;
using MediatR;

namespace Equipments.Application.Handlers;

public class StartMaintenanceCommandHandler : IRequestHandler<StartMaintenanceCommand, Result>
{
    private readonly IGeneralRepository<Equipment> _repository;

    public StartMaintenanceCommandHandler(IGeneralRepository<Equipment> repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(StartMaintenanceCommand request, CancellationToken cancellationToken)
    {
        var equipment = await _repository.GetEntityById(request.EquipmentId.ToString());

        if (equipment.IsFailure)
        {
            return Result.Failure(equipment.Error);
        }

        equipment.Value.StartMaintenance(request.Name, request.Vendor, request.StartDate, request.Money);

        var result = await _repository.SaveEntity(equipment.Value);

        return result.Match(() => equipment, Result.Failure);
    }
}