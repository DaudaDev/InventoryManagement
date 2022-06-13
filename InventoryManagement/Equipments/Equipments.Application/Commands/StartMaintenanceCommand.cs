using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using Equipments.Core.Domain.Equipment;
using MediatR;

namespace Equipments.Application.Commands;

public class StartMaintenanceCommand : IRequest<Result>
{
    public Guid EquipmentId { get; set; }
    public string Name { get; set; }
    public Vendor Vendor { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public Money Money { get; set; }
}