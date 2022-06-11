using Blocks.Shared.Infrastructure;
using CSharpFunctionalExtensions;
using MediatR;
using RawMaterials.Application.Queries;
using RawMaterials.Core.Domain.RawMaterials;

namespace RawMaterials.Application.Handlers;

public class GetAllRawMaterialsQueryHandler : IRequestHandler<GetAllRawMaterialsQuery, Result<IEnumerable<RawMaterial>>>
{
    private readonly IGeneralRepository<RawMaterial> _repository;

    public GetAllRawMaterialsQueryHandler(IGeneralRepository<RawMaterial> repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<RawMaterial>>> Handle(GetAllRawMaterialsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllEntities();
    }
}