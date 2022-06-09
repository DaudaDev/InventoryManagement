namespace Blocks.Shared.ValueObjects;

public record Period
{
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public TimeZoneInfo TimeZoneInfo  { get; set; } = TimeZoneInfo.Utc;
}