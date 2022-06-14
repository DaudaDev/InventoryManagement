using Blocks.Shared.Contracts.Infrastructure;
using CSharpFunctionalExtensions;
using MediatR;
using Production.Application.Commands;
using Production.Core.Domain;

namespace Production.Application.Handlers;

public class CreateProductionCommandHandler : IRequestHandler<CreateProductionCommand, Result<ProductionEntity>>
{
    private readonly IGeneralRepository<ProductionEntity> _repository;

    public CreateProductionCommandHandler(IGeneralRepository<ProductionEntity> repository)
    {
        _repository = repository;
    }

    public async Task<Result<ProductionEntity>> Handle(CreateProductionCommand request, CancellationToken cancellationToken)
    {
        var productionEntity = ProductionEntity.CreateProduction(Guid.NewGuid(), request.ProductType, request.SalesPeriod);
        
       var result = await _repository.SaveEntity(productionEntity);

       return result.Match(() => productionEntity, Result.Failure<ProductionEntity>);
    }
}