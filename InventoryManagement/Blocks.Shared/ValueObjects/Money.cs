namespace Blocks.Shared.ValueObjects;

public record Money(Currency Currency, double Amount);

public record Currency(string Symbol)
{
    public static Currency NGN => new ("NGN");
}
