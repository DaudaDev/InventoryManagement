using CSharpFunctionalExtensions;
using Inventory.Core;
using Inventory.Core.Domain;
using MediatR;

namespace Inventory.Application.Queries;

public class GetAllInventoryQuery : IRequest<Result<IEnumerable<InventoryItem>>> 
{
    
}