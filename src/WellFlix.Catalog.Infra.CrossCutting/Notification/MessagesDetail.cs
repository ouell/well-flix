using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace WellFlix.Catalog.Infra.CrossCutting.Notification;

[ExcludeFromCodeCoverage]
public class MessagesDetail
{
    [JsonPropertyName("field")]
    public string Field { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }

    //public MessagesDetail() { }

    [JsonConstructor]
    public MessagesDetail(string Field, string Message)
    {
        this.Field = Field;
        this.Message = Message;
    }
    
    [JsonConstructor]
    public MessagesDetail(string Field, string Message, string Value)
    {
        this.Field = Field;
        this.Message = Message;
        this.Value = Value;
    }
}