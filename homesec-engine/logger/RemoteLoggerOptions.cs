namespace TestWebAPI1.logger;

public sealed class RemoteLoggerOptions
{
    public string Url { get; set; } = null!;
    
    public int Port { get; set; } = 5555;
}