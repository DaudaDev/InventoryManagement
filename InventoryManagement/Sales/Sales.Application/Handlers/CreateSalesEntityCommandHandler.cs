using Blocks.Shared.Contracts.Infrastructure;
using CSharpFunctionalExtensions;
using MediatR;
using Sales.Application.Commands;
using Sales.Core.Domain;

namespace Sales.Application.Handlers;

public class CreateSalesEntityCommandHandler : IRequestHandler<CreateSalesEntityCommand, Result<SalesEntity>>
{
    private readonly IGeneralRepository<SalesEntity> _repository;

    public CreateSalesEntityCommandHandler(IGeneralRepository<SalesEntity> repository)
    {
        _repository = repository;
    }

    public async Task<Result<SalesEntity>> Handle(CreateSalesEntityCommand request, CancellationToken cancellationToken)
    {
        var salesEntity = SalesEntity.CreateSales(Guid.NewGuid(), request.Name, request.SalesPeriod);
        
       var result = await _repository.SaveEntity(salesEntity);

       return result.Match(() => salesEntity, Result.Failure<SalesEntity>);
    }
}