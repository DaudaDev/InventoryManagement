using Blocks.Shared.Contracts.Infrastructure;
using CSharpFunctionalExtensions;
using MediatR;
using Sales.Application.Queries;
using Sales.Core.Domain;

namespace Sales.Application.Handlers;

public class GetAllSalesQueryHandler : IRequestHandler<GetAllalesQuery, Result<IEnumerable<SalesEntity>>>
{
    private readonly IGeneralRepository<SalesEntity> _repository;

    public GetAllSalesQueryHandler(IGeneralRepository<SalesEntity> repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<SalesEntity>>> Handle(GetAllalesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllEntities();
    }
}