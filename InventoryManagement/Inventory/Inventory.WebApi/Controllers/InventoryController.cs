using Inventory.Application.Commands;
using Inventory.Application.Queries;
using Inventory.Core;
using Inventory.Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class InventoryController : ControllerBase
{
  
    private readonly IMediator _mediator;  

    private readonly ILogger<InventoryController> _logger;

    public InventoryController(ILogger<InventoryController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet(Name = "GetInventoryItems")]
    public IEnumerable<InventoryItem> GetInventoryItems(CancellationToken cancellationToken)
    {
        var result = _mediator.Send(new GetAllInventoryQuery(), cancellationToken).Result;

        return result.Value;
    }
    
    [HttpPost(Name = "CreatInventoryItem")]
    public InventoryItem CreatInventoryItem(CreateInventoryCommand inventoryCommand, CancellationToken cancellationToken)
    {
        var result = _mediator.Send(inventoryCommand, cancellationToken).Result;

        return result.Value;
    }
    
    [HttpPost(Name = "AddStock")]
    public bool AddStock(AddStockCommand inventoryCommand, CancellationToken cancellationToken)
    {
        var result = _mediator.Send(inventoryCommand, cancellationToken).Result;

        return result.IsSuccess;
    }
    
    [HttpPatch(Name = "UpdateStock")]
    public bool UpdateStock(UpdateStockCommand updateStockCommand, CancellationToken cancellationToken)
    {
        var result = _mediator.Send(updateStockCommand, cancellationToken).Result;

        return result.IsSuccess;
    }
}