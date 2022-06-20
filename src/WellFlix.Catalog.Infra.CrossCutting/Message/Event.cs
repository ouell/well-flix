using MediatR;

namespace WellFlix.Infra.CrossCutting.Message;

public class Event : Message, INotification
{
    public DateTime Timestamp { get; private set; }

    protected Event()
    {
        Timestamp = DateTime.Now;
    }
}