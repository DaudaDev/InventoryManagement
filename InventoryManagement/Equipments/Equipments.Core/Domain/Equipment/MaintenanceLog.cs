﻿using Blocks.Shared.ValueObjects;
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

    private MaintenanceLog(Guid id)
    {
        Id = id;
    }

    public void SetName(string name)
    {
        Name = new EntityName(name);
    }

    public Result SetDateStarted(DateTimeOffset dateStarted)
    {
        DateStarted = dateStarted;
        
        return Result.Success();
    }

    public Result SetDateEnded(DateTimeOffset dateEnded)
    {
        if (dateEnded < DateStarted)
        {
            return Result.Failure($"The end date cannot be before the start date {dateEnded}");
        }
        
        DateEnded = dateEnded;
        
        return Result.Success();
    }

    public void SetVendor(Vendor vendor)
    {
        Vendor = vendor;
    }

    public Result AddCost(Currency currency, double amount)
    {
        if (amount < 0)
        {
            return Result.Failure("Amount cannot be less than zero");
        }
        
        TotalCosts = new(currency, amount);
        
        return Result.Success();
    }

    public Result UpdateCost(Currency currency, double amount)
    {
        if (amount < 0)
        {
            return Result.Failure("Amount cannot be less than zero");
        }
        
        TotalCosts = TotalCosts == null
            ? new(currency, amount)
            : new(currency, TotalCosts.Amount + amount);
        
        return Result.Success();
    }

    public Result AddComment(string comment)
    {
        Comments.Add(new()
        {
            Id = Guid.NewGuid(),
            CommentDate = DateTimeOffset.Now,
            CommentValue = comment
        });
        
        return Result.Success();
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


    public static MaintenanceLog CreateMaintenanceLog(Guid logId)
    {
        return new MaintenanceLog(logId);
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