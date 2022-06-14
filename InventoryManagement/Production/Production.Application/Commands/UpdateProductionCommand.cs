using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using MediatR;
using Production.Application.Model;

namespace Production.Application.Commands;

public class UpdateProductionCommand: IRequest<Result>
{
    public Guid ProductionId { get; set; }
    
    public Period ProductionPeriod { get; set; }
    
    public Size Quantity { get; set; }

    public UpdateProductionType UpdateProductionType { get; set; }
}