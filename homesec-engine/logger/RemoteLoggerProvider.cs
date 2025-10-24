using System.Collections.Concurrent;
using System.Runtime.Versioning;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;

namespace TestWebAPI1.logger;

[ProviderAlias("RemoteLogger")]
public class RemoteLoggerProvider : ILoggerProvider
{
    private readonly IDisposable? _onChangeToken;
    private RemoteLoggerOptions _options;
    private readonly ConcurrentDictionary<string, RemoteLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);

    public RemoteLoggerProvider(IOptionsMonitor<RemoteLoggerOptions> options)
    {
        _options = options.CurrentValue;
        _onChangeToken = options.OnChange(updatedConfig => _options = updatedConfig);
    }

    public ILogger CreateLogger(string categoryName) =>
        _loggers.GetOrAdd(categoryName, name => new RemoteLogger(name, GetCurrentConfig));

    private RemoteLoggerOptions GetCurrentConfig() => _options;
    
    public void Dispose()
    {
        _loggers.Clear();
        _onChangeToken?.Dispose();
    }
}

public static class RemoteLoggerExtensions
{
    public static ILoggingBuilder AddRemoteLogger(this ILoggingBuilder builder)
    {
        builder.AddConfiguration();

        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, RemoteLoggerProvider>());

        LoggerProviderOptions.RegisterProviderOptions<RemoteLoggerOptions, RemoteLoggerProvider>(builder.Services);

        return builder;
    }
    
    public static ILoggingBuilder AddRemoteLogger(this ILoggingBuilder builder, 
        Action<RemoteLoggerOptions> configure)
    {
        builder.AddRemoteLogger();
        builder.Services.Configure(configure);

        return builder;
    }
}