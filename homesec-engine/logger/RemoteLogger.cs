using System.Diagnostics.CodeAnalysis;

namespace TestWebAPI1.logger;

public class RemoteLogger(
    string name,
    Func<RemoteLoggerOptions> getCurrentConfig) : ILogger
{
    // source: https://dev.to/mohammedahmed/building-a-custom-logging-provider-in-aspnet-core-437n
    // source: https://learn.microsoft.com/en-us/dotnet/core/extensions/custom-logging-provider
    
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        throw new NotImplementedException();
    }

    // todo
    public bool IsEnabled(LogLevel logLevel) => true;
        // getCurrentConfig().LogLevelToColorMap.ContainsKey(logLevel);

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }
        
        string message = formatter(state, exception);
        int lg = (int)logLevel;
        
        // todo invoke
        // todo use queue system to avoid blocking the log writing
        // todo send log to URL
        Console.WriteLine($"{logLevel}: {message} to {getCurrentConfig().Url}");
    }
}