using Blocks.Shared.Infrastructure;
using CSharpFunctionalExtensions;
using MediatR;
using RawMaterials.Application.Commands;
using RawMaterials.Core.Domain.RawMaterials;

namespace RawMaterials.Application.Handlers;

public class ReceiveShipmentCommandHandler : IRequestHandler<ReceiveShipmentCommand, Result>
{
    private readonly IGeneralRepository<RawMaterial> _repository;

    public ReceiveShipmentCommandHandler(IGeneralRepository<RawMaterial> repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(ReceiveShipmentCommand request, CancellationToken cancellationToken)
    {
        var rawMaterial = await _repository.GetEntityById(request.RawMaterialId.ToString());

        if (rawMaterial.IsFailure)
        {
            return Result.Failure(rawMaterial.Error);
        }

        rawMaterial.Value.ReceiveShipment(request.Size, request.ShipmentCOst, request.Vendor);

        var result = await _repository.SaveEntity(rawMaterial.Value);

        return result.Match(Result.Success, Result.Failure);
    }
}