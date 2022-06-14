using MediatR;
using Microsoft.AspNetCore.Mvc;
using Production.Application.Commands;
using Production.Application.Queries;
using Production.Core.Domain;

namespace Production.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProductionController : ControllerBase
{
  
    private readonly IMediator _mediator;  

    private readonly ILogger<ProductionController> _logger;

    public ProductionController(ILogger<ProductionController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet(Name = "GetAllProduction")]
    public IEnumerable<ProductionEntity> GetAllProduction(CancellationToken cancellationToken)
    {
        var result = _mediator.Send(new GetAllProductionQuery(), cancellationToken).Result;

        return result.Value;
    }
    
    [HttpPost(Name = "CreateProductionCommand")]
    public ProductionEntity CreateProductionCommand(CreateProductionCommand createRawMaterialCommand, CancellationToken cancellationToken)
    {
        var result = _mediator.Send(createRawMaterialCommand, cancellationToken).Result;

        return result.Value;
    }
    
    [HttpPost(Name = "UpdateProductionCommand")]
    public bool UpdateProductionCommand(UpdateProductionCommand receiveShipmentCommand, CancellationToken cancellationToken)
    {
        var result = _mediator.Send(receiveShipmentCommand, cancellationToken).Result;

        return result.IsSuccess;
    }
    
    [HttpPatch(Name = "UpdateProductionCostCommand")]
    public bool UpdateProductionCostCommand(UpdateProductionCostCommand updateRawMaterialCommand, CancellationToken cancellationToken)
    {
        var result = _mediator.Send(updateRawMaterialCommand, cancellationToken).Result;

        return result.IsSuccess;
    }
}