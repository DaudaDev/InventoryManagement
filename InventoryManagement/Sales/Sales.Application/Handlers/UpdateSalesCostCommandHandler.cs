using Blocks.Shared.Contracts.Infrastructure;
using CSharpFunctionalExtensions;
using MediatR;
using Sales.Application.Commands;
using Sales.Application.Services.UpdateSales.Factory;
using Sales.Core.Domain;

namespace Sales.Application.Handlers;

public class UpdateSalesCostCommandHandler: IRequestHandler<UpdateSalesCostCommand, Result>
{
    private readonly IGeneralRepository<SalesEntity> _repository;
    private readonly ISalesCostUpdaterFactory _salesCostUpdaterFactory;
    
    public UpdateSalesCostCommandHandler(IGeneralRepository<SalesEntity> repository, ISalesCostUpdaterFactory salesCostUpdaterFactory)
    {
        _repository = repository;
        _salesCostUpdaterFactory = salesCostUpdaterFactory;
    }
    
    public async Task<Result> Handle(UpdateSalesCostCommand request, CancellationToken cancellationToken)
    {
        var salesEntity = await _repository.GetEntityById(request.ProductionId.ToString());

        if (salesEntity.IsFailure)
        {
            return Result.Failure(salesEntity.Error);
        }

        var salesCostUpdater = _salesCostUpdaterFactory.GetSalesCostUpdater(request.UpdateSalesCostType);

        salesCostUpdater.Value.PerformUpdate(salesEntity.Value, request.SalesCostType, request.SalesCost);
        
        var result = await _repository.SaveEntity(salesEntity.Value);
        
        return result.Match(Result.Success, Result.Failure);
    }
}