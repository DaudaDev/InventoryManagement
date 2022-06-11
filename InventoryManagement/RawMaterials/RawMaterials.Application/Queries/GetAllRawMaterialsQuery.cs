using CSharpFunctionalExtensions;
using MediatR;
using RawMaterials.Core.Domain.RawMaterials;

namespace RawMaterials.Application.Queries;

public class GetAllRawMaterialsQuery: IRequest<Result<IEnumerable<RawMaterial>>> 
{
    
}