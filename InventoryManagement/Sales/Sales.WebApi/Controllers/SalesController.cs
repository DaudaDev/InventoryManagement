using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.Commands;
using Sales.Application.Queries;
using Sales.Core.Domain;

namespace Sales.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class SalesController : ControllerBase
{
  
    private readonly IMediator _mediator;  

    private readonly ILogger<SalesController> _logger;

    public SalesController(ILogger<SalesController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet(Name = "GetAllSalesEntity")]
    public IEnumerable<SalesEntity> GetAllSalesEntity(CancellationToken cancellationToken)
    {
        var result = _mediator.Send(new GetAllSalesQuery(), cancellationToken).Result;

        return result.Value;
    }
    
    [HttpPost(Name = "CreateRawMaterial")]
    public SalesEntity CreateRawMaterial(CreateSalesEntityCommand createRawMaterialCommand, CancellationToken cancellationToken)
    {
        var result = _mediator.Send(createRawMaterialCommand, cancellationToken).Result;

        return result.Value;
    }
    
    [HttpPost(Name = "UpdateSalesCommand")]
    public bool UpdateSalesCommand(UpdateSalesCommand receiveShipmentCommand, CancellationToken cancellationToken)
    {
        var result = _mediator.Send(receiveShipmentCommand, cancellationToken).Result;

        return result.IsSuccess;
    }
    
    [HttpPatch(Name = "UpdateSalesCostCommand")]
    public bool UpdateSalesCostCommand(UpdateSalesCostCommand updateRawMaterialCommand, CancellationToken cancellationToken)
    {
        var result = _mediator.Send(updateRawMaterialCommand, cancellationToken).Result;

        return result.IsSuccess;
    }
}