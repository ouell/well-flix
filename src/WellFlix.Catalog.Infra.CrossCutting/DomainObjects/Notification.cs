namespace WellFlix.Infra.CrossCutting.DomainObjects;

public class Notification
{
    public string Field { get; }
    public string Message { get; }
    public object Value { get; }

    public Notification(string field, string message, object value = null!)
    {
        Field = field;
        Message = message;
        Value = value;
    }
}