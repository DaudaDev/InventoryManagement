using Blocks.Shared.Contracts.Infrastructure;
using CSharpFunctionalExtensions;
using MediatR;
using Sales.Application.Commands;
using Sales.Application.Services.UpdateSalesCost.Factory;
using Sales.Core.Domain;

namespace Sales.Application.Handlers;

public class UpdateSalesCommandHandler: IRequestHandler<UpdateSalesCommand, Result>
{
    private readonly IGeneralRepository<SalesEntity> _repository;
    private readonly ISalesUpdaterFactory _salesUpdaterFactory;
    
    public UpdateSalesCommandHandler(IGeneralRepository<SalesEntity> repository, ISalesUpdaterFactory salesUpdaterFactory)
    {
        _repository = repository;
        _salesUpdaterFactory = salesUpdaterFactory;
    }
    
    public async Task<Result> Handle(UpdateSalesCommand request, CancellationToken cancellationToken)
    {
        var salesEntity = await _repository.GetEntityById(request.SalesId.ToString());

        if (salesEntity.IsFailure)
        {
            return Result.Failure(salesEntity.Error);
        }

        var salesUpdater = _salesUpdaterFactory.GetSalesUpdater(request.UpdateSalesType);
        
        salesUpdater.Value.PerformUpdate(salesEntity.Value, request.CostType, request.Amount);

        var result = await _repository.SaveEntity(salesEntity.Value);
        
        return result.Match(Result.Success, Result.Failure);
    }
}