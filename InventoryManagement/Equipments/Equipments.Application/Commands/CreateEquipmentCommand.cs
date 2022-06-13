using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;
using Equipments.Core.ValueObjects;
using MediatR;

namespace Equipments.Application.Commands;

public abstract class CreateEquipmentCommand: IRequest<Result>  
{
    public Guid EquipmentId { get; set; }
    public EntityName EquipmentName { get; set; }
    public Money EquipmentPrice { get; set; }
    public EquipmentType EquipmentType { get; set; }
    public DateTimeOffset PurchaseDate { get; set; }
}