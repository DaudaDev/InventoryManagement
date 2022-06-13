using Equipments.Application.Commands;
using Equipments.Application.Queries;
using Equipments.Core.Domain.Equipment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Equipments.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class EquipmentController : ControllerBase
{
  
    private readonly IMediator _mediator;  

    private readonly ILogger<EquipmentController> _logger;

    public EquipmentController(ILogger<EquipmentController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet(Name = "GetAllEquipmentsQuery")]
    public IEnumerable<Equipment> GetAllEquipmentsQuery(CancellationToken cancellationToken)
    {
        var result = _mediator.Send(new GetAllEquipmentsQuery(), cancellationToken).Result;

        return result.Value;
    }
    
    [HttpPost(Name = "CreateEquipment")]
    public Equipment CreateEquipment(CreateEquipmentCommand createEquipmentCommand, CancellationToken cancellationToken)
    {
        var result = _mediator.Send(createEquipmentCommand, cancellationToken).Result;

        return result.Value;
    }
    
    [HttpPost(Name = "UpdateEquipment")]
    public bool UpdateEquipment(UpdateEquipmentCommand updateEquipmentCommand, CancellationToken cancellationToken)
    {
        var result = _mediator.Send(updateEquipmentCommand, cancellationToken).Result;

        return result.IsSuccess;
    }
    
    [HttpPost(Name = "StartMaintenance")]
    public bool StartMaintenance(StartMaintenanceCommand startMaintenanceCommand, CancellationToken cancellationToken)
    {
        var result = _mediator.Send(startMaintenanceCommand, cancellationToken).Result;

        return result.IsSuccess;
    }
    
    [HttpPatch(Name = "FinishMaintenance")]
    public bool FinishMaintenance(FinishMaintenanceCommand finishMaintenanceCommand, CancellationToken cancellationToken)
    {
        var result = _mediator.Send(finishMaintenanceCommand, cancellationToken).Result;

        return result.IsSuccess;
    }
    
    [HttpPatch(Name = "UpdateMaintenance")]
    public bool UpdateMaintenance(UpdateMaintenanceCommand updateMaintenanceCommand, CancellationToken cancellationToken)
    {
        var result = _mediator.Send(updateMaintenanceCommand, cancellationToken).Result;

        return result.IsSuccess;
    }
}