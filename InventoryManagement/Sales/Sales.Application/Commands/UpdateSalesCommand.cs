using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using MediatR;
using Sales.Application.Model;
using Sales.Core.ValueObjects;

namespace Sales.Application.Commands;

public class UpdateSalesCommand: IRequest<Result>
{
    public Guid SalesId { get; set; }
    
    public SalesCostType CostType { get; set; }
    
    public Money Amount { get; set; }

    public UpdateSalesType UpdateSalesType { get; set; }
}