namespace TestWebAPI1.internal_api;

public class ResponseDto
{
    public long timestamp2 { get; set; }

    public string event_type2 { get; set; }

    public override string ToString()
    {
        return $"{nameof(timestamp2)}: {timestamp2}, {nameof(event_type2)}: {event_type2}";
    }

}