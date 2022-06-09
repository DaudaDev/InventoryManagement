namespace Blocks.Shared.ValueObjects;

public class Size
{
    public Unit Unit { get; set; } = Unit.Empty;
    public double Amount { get; set; }
}