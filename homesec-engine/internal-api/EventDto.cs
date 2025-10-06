namespace TestWebAPI1.internal_api;

public class EventDto
{
    public long timestamp { get; set; }

    public string event_type { get; set; }

    public override string ToString()
    {
        return $"{nameof(timestamp)}: {timestamp}, {nameof(event_type)}: {event_type}";
    }
}