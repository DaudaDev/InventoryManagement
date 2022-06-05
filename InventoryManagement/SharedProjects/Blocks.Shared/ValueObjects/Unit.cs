namespace Blocks.Shared.ValueObjects;

public record Unit(string description)
{
    public static Unit Bags => new("Bags");
    public static Unit Kilograms => new("KG");
}