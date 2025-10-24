using TestWebAPI1.logger;
using TestWebAPI1.sensors;

namespace TestWebAPI1.manager;

/// <summary>
/// The SecurityEngine class handles the general logic of the home security system
/// </summary>
public class SecurityEngine
{
    /// <summary>
    /// Singleton instance
    /// </summary>
    private static SecurityEngine? _instance;
    
    private static readonly Logger.NamedLogger Logger = new(nameof(SecurityEngine));
    
    private readonly object _lock = new();

    private EngineState _engineState;
    
    public static SecurityEngine GetInstance()
    {
        _instance ??= new SecurityEngine();
        return _instance;
    }

    private SecurityEngine()
    {
        // initialize: set callback api, read config
        Logger.Info("Initializing...");
        _engineState = EngineState.Init;
        SensorsBridge.SetCallbackUrl("callback").Wait();
        _engineState = EngineState.Off;
        Logger.Info("Engine state set to Off. Initialization complete");
    }

    private async Task Initialize()
    {
        var success = false;
        while (!success)
        {
            success = await SensorsBridge.SetCallbackUrl("callback");

            if (!success)
            {
                _engineState = EngineState.BadInitialization;
                Logger.Warn("Failed to set callback url");
                await Task.Delay(5000);
            }
        }
        Logger.Warn("Callback url set successfully");
    }

    public void KnownPersonDetected()
    {
        lock (_lock)
        {
            Logger.Info($"Known person detected. State is {_engineState}");
            switch (_engineState)
            {
                case EngineState.EnterDelay:
                case EngineState.Fired:
                    Logger.Info($"Event allows to disarm the system");
                    // todo disarm
                    break;
                default:
                    Logger.Info($"Event is ignored due to state");
                    break;
            }
        }
    }

    public void UnknownPersonDetected()
    {
        
    }
}