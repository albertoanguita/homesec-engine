namespace TestWebAPI1.manager;

/// <summary>
/// The SecurityEngine class handles the general logic of the home security system
/// </summary>
public class SecurityEngine
{
    private static SecurityEngine? _instance = null;

    public static SecurityEngine GetInstance()
    {
        _instance ??= new SecurityEngine();
        return _instance;
    }

    private SecurityEngine()
    {
        // initialize. Set callback api, set initial state
    }

    public void KnownPersonDetected()
    {
        
    }

    public void UnknownPersonDetected()
    {
        
    }
}