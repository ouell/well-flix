using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace WellFlix.Catalog.Infra.CrossCutting.Notification;

[ExcludeFromCodeCoverage]
public class Messages
{
    public Messages(MessagesDetail field)
    {
        AddField(field);
    }

    [JsonConstructor]
    public Messages(List<MessagesDetail>? fields)
    {
        this.Fields = fields ?? this.Fields;
    }
    
    [JsonPropertyName("fields")]
    public List<MessagesDetail> Fields { get; } = new();

    public void AddField(MessagesDetail detail)
    {
        if (detail is object)
            Fields.Add(detail);
    }
}