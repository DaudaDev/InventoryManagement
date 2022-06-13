using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using Equipments.Application.Model;
using Equipments.Core.ValueObjects;
using MediatR;

namespace Equipments.Application.Commands;

public abstract class UpdateEquipmentCommand : IRequest<Result>
{
    public Guid InventoryId { get; set; }
    public EntityName EquipmentName { get; set; }
    public Money EquipmentPrice { get; set; }
    public EquipmentType EquipmentType { get; set; }
    public DateTimeOffset PurchaseDate { get; set; }
    public EquipmentUpdateType EquipmentUpdateType { get; set; }
}