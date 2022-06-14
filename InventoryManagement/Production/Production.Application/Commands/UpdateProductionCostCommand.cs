using CSharpFunctionalExtensions;
using MediatR;
using Production.Application.Model;
using Production.Core.Domain;
using Production.Core.ValueObjects;

namespace Production.Application.Commands;

public class UpdateProductionCostCommand : IRequest<Result> 
{
    public Guid ProductionId { get; set; }
    
    public ProductionCost ProductionCost { get; set; }
    
    public ProductionCostType ProductionCostType { get; set; }
    
    public UpdateProductionCostType UpdateProductionCostType { get; set; }
}