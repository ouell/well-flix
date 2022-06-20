using System.Diagnostics.CodeAnalysis;
using WellFlix.Infra.CrossCutting.Notification;
using WellFlix.Infra.CrossCutting.Notification.Interfaces;

namespace WellFlix.Catalog.Infra.CrossCutting.Notification;

[ExcludeFromCodeCoverage]
public static class Result
{
    public static IOperation CreateSuccess()
    {
        return new SuccessfulOperation();
    }

    public static IOperation<T> CreateSuccess<T>(T value)
    {
        return new SuccessfulOperation<T>(value);
    }

    public static IOperation<T> CreateFailure<T>(MessagesDetail field)
    {
        return new FailedOperation<T>(field);
    }

    public static IOperation<T> CreateFailure<T>(List<MessagesDetail> field)
    {
        return new FailedOperation<T>(field);
    }

    public static IOperation CreateFailure(MessagesDetail field)
    {
        return new FailedOperation(field);
    }

    public static IOperation CreateFailure(List<MessagesDetail> field)
    {
        return new FailedOperation(field);
    }

    public static IOperation<T> CreateFailure<T>(Messages messages)
    {
        return new FailedOperation<T>(messages);
    }
}