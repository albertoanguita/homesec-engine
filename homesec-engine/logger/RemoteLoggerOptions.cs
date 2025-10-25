namespace TestWebAPI1.logger;

public sealed class RemoteLoggerOptions
{
    public string SystemName { get; set; } = null!;
    
    public string Url { get; set; } = null!;
    
    public int Port { get; set; }

    public string AuthHeader { get; set; } = null!;
}