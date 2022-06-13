using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using Equipments.Application.Model;
using MediatR;

namespace Equipments.Application.Commands;

public class UpdateMaintenanceCommand: IRequest<Result>
{
    public Guid EquipmentId { get; set; }
    public Guid LogId { get; set; }
    public Guid CommentID { get; set; }
    public string Comment { get; set; }
    public Money Money { get; set; }
    public MaintenanceUpdateType MaintenanceUpdateType { get; set; }
}