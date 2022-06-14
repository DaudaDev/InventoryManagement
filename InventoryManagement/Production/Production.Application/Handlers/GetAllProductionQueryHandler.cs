using Blocks.Shared.Contracts.Infrastructure;
using CSharpFunctionalExtensions;
using MediatR;
using Production.Application.Queries;
using Production.Core.Domain;

namespace Production.Application.Handlers;

public class GetAllProductionQueryHandler : IRequestHandler<GetAllProductionQuery, Result<IEnumerable<ProductionEntity>>>
{
    private readonly IGeneralRepository<ProductionEntity> _repository;

    public GetAllProductionQueryHandler(IGeneralRepository<ProductionEntity> repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<ProductionEntity>>> Handle(GetAllProductionQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllEntities();
    }
}