using Blocks.Shared.Contracts.Infrastructure;
using CSharpFunctionalExtensions;
using Equipments.Application.Commands;
using Equipments.Core.Domain.Equipment;
using MediatR;

namespace Equipments.Application.Handlers;

public class CreateEquipmentCommandHandler: IRequestHandler<CreateEquipmentCommand, Result<Equipment>>
{
    private readonly IGeneralRepository<Equipment> _repository;

    public CreateEquipmentCommandHandler(IGeneralRepository<Equipment> repository)
    {
        _repository = repository;
    }
    
    public async Task<Result<Equipment>> Handle(CreateEquipmentCommand request, CancellationToken cancellationToken)
    {
        var equipment = Equipment.CreateEquipments(
            request.EquipmentId,
            request.EquipmentName, 
            request.EquipmentPrice,
            request.EquipmentType, 
            request.PurchaseDate);
        
        var result = await _repository.SaveEntity(equipment);
        
        return result.Match(() => equipment, Result.Failure<Equipment>);
    }
}