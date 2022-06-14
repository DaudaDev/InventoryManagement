using CSharpFunctionalExtensions;
using MediatR;
using Sales.Application.Model;
using Sales.Core.Domain;
using Sales.Core.ValueObjects;

namespace Sales.Application.Commands;

public class UpdateSalesCostCommand : IRequest<Result> 
{
    public Guid ProductionId { get; set; }
    
    public SalesCost SalesCost { get; set; }
    
    public SalesCostType SalesCostType { get; set; }
    
    public UpdateSalesCostType UpdateSalesCostType { get; set; }
}