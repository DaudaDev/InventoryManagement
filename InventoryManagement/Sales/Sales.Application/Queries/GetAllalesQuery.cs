using CSharpFunctionalExtensions;
using MediatR;
using Sales.Core.Domain;

namespace Sales.Application.Queries;

public class GetAllalesQuery : IRequest<Result<IEnumerable<SalesEntity>>> 
{
    
}