using System.Text.Json.Serialization;

namespace TestWebAPI1.sensors.dtos;

public enum Command
{
    START,
    STOP,
    RESET
}

public class CommandDto
{
    [JsonPropertyName("command")]
    public required string Command { get; set; }
}

public class SetCallbackUrlDto
{
    [JsonPropertyName("url")]
    public required string Url { get; set; }
}

public class SetCallbackUrlResponseDto
{
    [JsonPropertyName("success")]
    public required bool Success { get; set; }
}