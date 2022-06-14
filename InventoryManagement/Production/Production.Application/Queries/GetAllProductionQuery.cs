using CSharpFunctionalExtensions;
using MediatR;
using Production.Core.Domain;

namespace Production.Application.Queries;

public class GetAllProductionQuery : IRequest<Result<IEnumerable<ProductionEntity>>> 
{
    
}