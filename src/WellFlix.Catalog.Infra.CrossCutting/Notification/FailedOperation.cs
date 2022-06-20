using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using WellFlix.Catalog.Infra.CrossCutting.Notification;
using WellFlix.Infra.CrossCutting.Notification.Interfaces;

namespace WellFlix.Infra.CrossCutting.Notification;

[ExcludeFromCodeCoverage]
public class FailedOperation : IOperation
{
    // public FailedOperation()
    // {
    // }

    public FailedOperation(MessagesDetail detail)
    {
        Messages = new Messages(detail);
    }

    public FailedOperation(List<MessagesDetail> detail)
    {
        Messages = new Messages(detail);
    }

    [JsonConstructor]
    public FailedOperation(Messages messages)
    {
        Messages = messages;
    }

    [JsonConstructor]
    public FailedOperation(List<Messages> messages)
    {
        var list = new List<MessagesDetail>();
        foreach (var msg in messages) 
            list.AddRange(msg.Fields);

        Messages = new Messages(list);
    }


    [JsonPropertyName("messages")] public Messages Messages { get; }

    public IOperation<T> GetTyped<T>()
    {
        return new FailedOperation<T>(Messages);
    }
}

[ExcludeFromCodeCoverage]
public class FailedOperation<T> : FailedOperation, IOperation<T>
{
    public FailedOperation(Messages messages) : base(messages)
    {
    }

    public FailedOperation(MessagesDetail detail) : base(detail)
    {
    }

    public FailedOperation(List<MessagesDetail> detail) : base(detail)
    {
    }
}