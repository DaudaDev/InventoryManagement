using Blocks.Shared.Infrastructure;
using CSharpFunctionalExtensions;
using MediatR;
using RawMaterials.Application.Commands;
using RawMaterials.Core.Domain.RawMaterials;

namespace RawMaterials.Application.Handlers;

public class CreateRawMaterialCommandHandler : IRequestHandler<CreateRawMaterialCommand, Result<RawMaterial>>
{
    private readonly IGeneralRepository<RawMaterial> _repository;

    public CreateRawMaterialCommandHandler(IGeneralRepository<RawMaterial> repository)
    {
        _repository = repository;
    }

    public async Task<Result<RawMaterial>> Handle(CreateRawMaterialCommand request, CancellationToken cancellationToken)
    {
        var rawMaterial = RawMaterial.CreatRawMaterial(request.RawMaterialId, request.MaterialType, request.Brand);
        
        var result = await _repository.SaveEntity(rawMaterial.Value);

        return result.Match(() => rawMaterial, Result.Failure<RawMaterial>);    }
}