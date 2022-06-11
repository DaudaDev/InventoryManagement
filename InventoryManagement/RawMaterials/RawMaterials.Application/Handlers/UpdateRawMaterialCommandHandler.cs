using Blocks.Shared.Infrastructure;
using CSharpFunctionalExtensions;
using MediatR;
using RawMaterials.Application.Commands;
using RawMaterials.Core.Domain.RawMaterials;

namespace RawMaterials.Application.Handlers;

public class UpdateRawMaterialCommandHandler : IRequestHandler<UpdateRawMaterialCommand, Result>
{
    private readonly IGeneralRepository<RawMaterial> _repository;

    public UpdateRawMaterialCommandHandler(IGeneralRepository<RawMaterial> repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(UpdateRawMaterialCommand request, CancellationToken cancellationToken)
    {
        var rawMaterial = await _repository.GetEntityById(request.RawMaterialId.ToString());

        if (rawMaterial.IsFailure)
        {
            return Result.Failure(rawMaterial.Error);
        }
        
        rawMaterial.Value.SetMaterialType(request.MaterialType);
        rawMaterial.Value.SetBrand(request.Brand);
        
        var result = await _repository.SaveEntity(rawMaterial.Value);
        
        return result.Match(Result.Success, Result.Failure);
    }
}