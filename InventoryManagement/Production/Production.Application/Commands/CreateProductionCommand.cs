using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using MediatR;
using Production.Core.Domain;
using Production.Core.ValueObjects;

namespace Production.Application.Commands;

public class CreateProductionCommand: IRequest<Result<ProductionEntity>>  
{
    public Guid ProductionId { get; set; }
    
    public ProductType ProductType { get; set; }
    
    public Period SalesPeriod { get; set; }

     
}