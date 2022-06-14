using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using MediatR;
using Sales.Core.Domain;

namespace Sales.Application.Commands;

public class CreateSalesEntityCommand: IRequest<Result<SalesEntity>>  
{
    public Guid ProductionId { get; set; }
    
    public EntityName Name { get; set; }
    
    public Period SalesPeriod { get; set; }

     
}