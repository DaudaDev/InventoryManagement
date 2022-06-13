using Blocks.Shared.Contracts.Infrastructure;
using CSharpFunctionalExtensions;
using Equipments.Application.Queries;
using Equipments.Core.Domain.Equipment;
using MediatR;

namespace Equipments.Application.Handlers;

public class GetAllInventoryQueryHandler : IRequestHandler<GetAllEquipmentsQuery, Result<IEnumerable<Equipment>>>
{
    private readonly IGeneralRepository<Equipment> _repository;

    public GetAllInventoryQueryHandler(IGeneralRepository<Equipment> repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<Equipment>>> Handle(GetAllEquipmentsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllEntities();
    }
}