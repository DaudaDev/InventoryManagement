using CSharpFunctionalExtensions;

namespace Blocks.Shared.Extenstions;

public static class ResultExtensions
{
    public static Result ExecuteSuccess<T>(this Result<T> result, Func<T, Result> onSuccess)
    {
        return result
            .Match(onSuccess, _ => result);
    }
}