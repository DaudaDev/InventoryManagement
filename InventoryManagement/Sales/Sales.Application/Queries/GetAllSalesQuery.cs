using CSharpFunctionalExtensions;
using MediatR;
using Sales.Core.Domain;

namespace Sales.Application.Queries;

public class GetAllSalesQuery : IRequest<Result<IEnumerable<SalesEntity>>> 
{
    
}