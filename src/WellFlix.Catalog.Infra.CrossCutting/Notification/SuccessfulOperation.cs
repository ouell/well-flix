using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using WellFlix.Infra.CrossCutting.Notification.Interfaces;

namespace WellFlix.Catalog.Infra.CrossCutting.Notification;

[ExcludeFromCodeCoverage]
public class SuccessfulOperation : IOperation
{
    public SuccessfulOperation() { }
}

[ExcludeFromCodeCoverage]
public class SuccessfulOperation<T> : SuccessfulOperation, IOperation<T>
{
    public SuccessfulOperation(T data) : base()
    {
        Data = data;
    }

    [JsonPropertyName("data")]
    public T Data { get; private set; }

    public static implicit operator SuccessfulOperation<T>(T data)
    {
        return new SuccessfulOperation<T>(data);
    }
}