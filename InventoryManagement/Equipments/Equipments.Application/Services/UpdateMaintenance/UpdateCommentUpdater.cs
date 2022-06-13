using Equipments.Application.Commands;
using Equipments.Application.Model;
using Equipments.Core.Domain.Equipment;

namespace Equipments.Application.Services.UpdateMaintenance;

public class UpdateCommentUpdater : IMaintenanceUpdater
{
    public MaintenanceUpdateType MaintenanceUpdateType => MaintenanceUpdateType.UpdateComment;
    public void PerformUpdate(Equipment equipment, UpdateMaintenanceCommand updateMaintenanceCommand)
    {
        equipment.UpdateComment(updateMaintenanceCommand.LogId, updateMaintenanceCommand.CommentID, updateMaintenanceCommand.Comment);
    }
}