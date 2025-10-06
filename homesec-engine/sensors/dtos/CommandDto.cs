namespace TestWebAPI1.sensors.dtos;

public enum Command
{
    START,
    STOP,
    RESET
}

public class CommandDto
{
    // todo uppercase but lowercase in json
    public required string command { get; set; }
}