using Blocks.Shared.Contracts.Infrastructure;
using CSharpFunctionalExtensions;
using MediatR;
using Production.Application.Commands;
using Production.Application.Services.UpdateProductionCost.Factory;
using Production.Core.Domain;

namespace Production.Application.Handlers;

public class UpdateProductionCommandHandler: IRequestHandler<UpdateProductionCommand, Result>
{
    private readonly IGeneralRepository<ProductionEntity> _repository;
    private readonly IProductionUpdaterFactory _productionUpdaterFactory;
    
    public UpdateProductionCommandHandler(IGeneralRepository<ProductionEntity> repository, IProductionUpdaterFactory productionUpdaterFactory)
    {
        _repository = repository;
        _productionUpdaterFactory = productionUpdaterFactory;
    }
    
    public async Task<Result> Handle(UpdateProductionCommand request, CancellationToken cancellationToken)
    {
        var productionEntity = await _repository.GetEntityById(request.ProductionId.ToString());

        if (productionEntity.IsFailure)
        {
            return Result.Failure(productionEntity.Error);
        }

        var productionUpdater = _productionUpdaterFactory.GetProductionUpdater(request.UpdateProductionType);
        
        productionUpdater.Value.PerformUpdate(productionEntity.Value, request.ProductionPeriod, request.Quantity);

        var result = await _repository.SaveEntity(productionEntity.Value);
        
        return result.Match(Result.Success, Result.Failure);
    }
}