using Blocks.Shared.Contracts.Infrastructure;
using CSharpFunctionalExtensions;
using Inventory.Application.Queries;
using Inventory.Core.Domain;
using MediatR;

namespace Inventory.Application.Handlers;

public class GetAllInventoryQueryHandler : IRequestHandler<GetAllInventoryQuery, Result<IEnumerable<InventoryItem>>>
{
    private readonly IGeneralRepository<InventoryItem> _repository;

    public GetAllInventoryQueryHandler(IGeneralRepository<InventoryItem> repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<InventoryItem>>> Handle(GetAllInventoryQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllEntities();
    }
}