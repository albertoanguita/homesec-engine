using TestWebAPI1.logger;

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
        // todo initialize sensors engine, set callback. Use retry system to let it start up
        _engineState = EngineState.Off;
        Logger.Info("Engine state set to Off. Initialization complete");
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