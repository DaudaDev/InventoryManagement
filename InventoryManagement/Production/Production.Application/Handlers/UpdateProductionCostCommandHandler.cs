using Blocks.Shared.Contracts.Infrastructure;
using CSharpFunctionalExtensions;
using MediatR;
using Production.Application.Commands;
using Production.Application.Services.UpdateProduction.Factory;
using Production.Core.Domain;

namespace Production.Application.Handlers;

public class UpdateProductionCostCommandHandler: IRequestHandler<UpdateProductionCostCommand, Result>
{
    private readonly IGeneralRepository<ProductionEntity> _repository;
    private readonly IProductionCostUpdaterFactory _productionCostUpdaterFactory;
    
    public UpdateProductionCostCommandHandler(IGeneralRepository<ProductionEntity> repository, IProductionCostUpdaterFactory productionCostUpdaterFactory)
    {
        _repository = repository;
        _productionCostUpdaterFactory = productionCostUpdaterFactory;
    }
    
    public async Task<Result> Handle(UpdateProductionCostCommand request, CancellationToken cancellationToken)
    {
        var productionEntity = await _repository.GetEntityById(request.ProductionId.ToString());

        if (productionEntity.IsFailure)
        {
            return Result.Failure(productionEntity.Error);
        }

        var productionCostUpdater = _productionCostUpdaterFactory.GetProductionCostUpdater(request.UpdateProductionCostType);

        productionCostUpdater.Value.PerformUpdate(productionEntity.Value, request.ProductionCostType, request.ProductionCost);
        
        var result = await _repository.SaveEntity(productionEntity.Value);
        
        return result.Match(Result.Success, Result.Failure);
    }
}