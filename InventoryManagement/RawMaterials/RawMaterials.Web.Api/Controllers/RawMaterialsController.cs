using MediatR;
using Microsoft.AspNetCore.Mvc;
using RawMaterials.Application.Commands;
using RawMaterials.Application.Queries;
using RawMaterials.Core.Domain.RawMaterials;

namespace RawMaterials.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class RawMaterialsController : ControllerBase
{
  
    private readonly IMediator _mediator;  

    private readonly ILogger<RawMaterialsController> _logger;

    public RawMaterialsController(ILogger<RawMaterialsController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet(Name = "GetAllRawMaterials")]
    public IEnumerable<RawMaterial> GetAllRawMaterials(CancellationToken cancellationToken)
    {
        var result = _mediator.Send(new GetAllRawMaterialsQuery(), cancellationToken).Result;

        return result.Value;
    }
    
    [HttpPost(Name = "CreateRawMaterial")]
    public RawMaterial CreateRawMaterial(CreateRawMaterialCommand createRawMaterialCommand, CancellationToken cancellationToken)
    {
        var result = _mediator.Send(createRawMaterialCommand, cancellationToken).Result;

        return result.Value;
    }
    
    [HttpPost(Name = "ReceiveShipment")]
    public bool ReceiveShipment(ReceiveShipmentCommand receiveShipmentCommand, CancellationToken cancellationToken)
    {
        var result = _mediator.Send(receiveShipmentCommand, cancellationToken).Result;

        return result.IsSuccess;
    }
    
    [HttpPatch(Name = "UpdateRawMaterial")]
    public bool UpdateRawMaterial(UpdateRawMaterialCommand updateRawMaterialCommand, CancellationToken cancellationToken)
    {
        var result = _mediator.Send(updateRawMaterialCommand, cancellationToken).Result;

        return result.IsSuccess;
    }
}