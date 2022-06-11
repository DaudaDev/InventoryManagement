using Blocks.Shared.Infrastructure;
using CSharpFunctionalExtensions;
using MediatR;
using RawMaterials.Application.Commands;
using RawMaterials.Application.Services.UpdateStock.Factory;
using RawMaterials.Core.Domain.RawMaterials;

namespace RawMaterials.Application.Handlers;

public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand, Result>
{
    private readonly IGeneralRepository<RawMaterial> _repository;
    private readonly IStockUpdaterFactory _stockUpdaterFactory;

    public UpdateStockCommandHandler(IGeneralRepository<RawMaterial> repository,
        IStockUpdaterFactory stockUpdaterFactory)
    {
        _repository = repository;
        _stockUpdaterFactory = stockUpdaterFactory;
    }

    public async Task<Result> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
    {
        var rawMaterial = await _repository.GetEntityById(request.RawMaterialId.ToString());

        if (rawMaterial.IsFailure)
        {
            return Result.Failure(rawMaterial.Error);
        }

        var stockUpdater = _stockUpdaterFactory.GetStockUpdater(request.RawMaterialUpdateType);

        stockUpdater.Value.PerformUpdate(rawMaterial.Value, request.Size);

        var result = await _repository.SaveEntity(rawMaterial.Value);

        return result.Match(Result.Success, Result.Failure);
    }
}