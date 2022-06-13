using CSharpFunctionalExtensions;
using MediatR;

namespace Equipments.Application.Commands;

public abstract class FinishMaintenanceCommand : IRequest<Result>
{
    public Guid EquipmentId { get; set; }
    public Guid LogId { get; set; }
    public DateTimeOffset EndDate { get; set; }
}