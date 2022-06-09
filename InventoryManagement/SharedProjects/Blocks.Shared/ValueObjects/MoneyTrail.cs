using CSharpFunctionalExtensions;

namespace Blocks.Shared.ValueObjects;

public class MoneyTrail
{
    public MoneyTrail(Money currentValue)
    {
        CurrentValue = currentValue;
    }

    public Money CurrentValue { get; set; }
    public IList<MoneyChangeLog> ChangeLog { get; set; } = Array.Empty<MoneyChangeLog>();

    public Result UpdateMoney(Money money)
    {
        CurrentValue = money;
        ChangeLog.Add(new (DateTimeOffset.UtcNow, money));
        
        return Result.Success();
    }
}

public record MoneyChangeLog(DateTimeOffset ChangeDate, Money Money);