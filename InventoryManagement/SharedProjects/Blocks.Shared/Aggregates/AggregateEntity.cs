namespace Blocks.Shared.Aggregates;

public abstract class AggregateEntity
{
    public string Id { get; set; }
    public Guid EntityId { get; set; }
}