namespace Equipments.Core.Domain.Equipment;

public class Comment
{
    public Guid Id { get; set; }
    public DateTimeOffset CommentDate { get; set; }
    public string? CommentValue { get; set; }
}