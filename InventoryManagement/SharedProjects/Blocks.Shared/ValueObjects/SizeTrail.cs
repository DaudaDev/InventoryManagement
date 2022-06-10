using Blocks.Shared.enums;
using CSharpFunctionalExtensions;

namespace Blocks.Shared.ValueObjects;

public class SizeTrail
{
    public SizeTrail(Size currentValue)
    {
        CurrentValue = currentValue;
    }

    public Size CurrentValue { get; set; }
    public IList<SizeChangeLog> ChangeLog { get; set; } = new List<SizeChangeLog>();

    public Result AddAmount(double amount)
    {
        CurrentValue.Amount += amount;
        ChangeLog.Add(new (DateTimeOffset.UtcNow, OperationType.Addition, CurrentValue));
        
        return Result.Success();
    }
    
    public Result SubtractAmount(double amount)
    {
        if (CurrentValue.Amount < amount)
        {
            return Result.Failure($"There are only {CurrentValue.Amount} items remaining");
        }
        
        CurrentValue.Amount -= amount;
        ChangeLog.Add(new (DateTimeOffset.UtcNow, OperationType.Subtraction, CurrentValue));
        
        return Result.Success();
    }
}

public record SizeChangeLog(DateTimeOffset ChangeDate, OperationType OperationType, Size Size);