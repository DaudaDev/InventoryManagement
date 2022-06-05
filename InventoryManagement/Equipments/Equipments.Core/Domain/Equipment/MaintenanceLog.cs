using Blocks.Shared.ValueObjects;
using CSharpFunctionalExtensions;

namespace Equipments.Core.Domain.Equipment;

public class MaintenanceLog
{
    public Guid Id { get; private set; }
    public EntityName Name { get; set; }
    public DateTimeOffset DateStarted { get; private set; }
    public DateTimeOffset DateEnded { get; private set; }
    public Vendor Vendor { get; private set; }
    public Money? TotalCosts { get; private set; }
    public IList<Comment> Comments { get; private set; } = Array.Empty<Comment>();

    public MaintenanceLog(Guid id)
    {
        Id = id;
    }

    public void SetName(string name)
    {
        Name = new EntityName(name);
    }

    public void SetDateStarted(DateTimeOffset? dateStarted)
    {
        DateStarted = dateStarted ?? DateTimeOffset.Now;
    }

    public void SetDateEnded(DateTimeOffset? dateEnded)
    {
        DateEnded = dateEnded ?? DateTimeOffset.Now;
    }

    public void SetVendor(Vendor vendor)
    {
        Vendor = vendor;
    }

    public void AddCost(Currency currency, double amount)
    {
        TotalCosts = new(currency, amount);
    }

    public void UpdateCost(Currency currency, double amount)
    {
        TotalCosts = TotalCosts == null
            ? new(currency, amount)
            : new(currency, TotalCosts.Amount + amount);
    }

    public void AddComment(string comment)
    {
        Comments.Add(new()
        {
            Id = Guid.NewGuid(),
            CommentDate = DateTimeOffset.Now,
            CommentValue = comment
        });
    }

    public Result UpdateComment(Guid commentId, string commentText)
    {
        var result = GetComment(commentId);
        if (result.IsFailure)
        {
            return result;
        }

        result.Value.CommentValue = commentText;

        return new Result();
    }

    private Result<Comment> GetComment(Guid commentId)
    {
        var comment = Comments.SingleOrDefault(comment => comment.Id == commentId);

        if (comment is null)
        {
            return Result.Failure<Comment>($"Comment with ID {commentId} cannot be found");
        }

        return Result.Success(comment);
    }
}