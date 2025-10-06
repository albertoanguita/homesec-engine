namespace TestWebAPI1;

// todo use dependency injection where possible to avoid this class
public static class Configuration
{
    private static IConfiguration? _config;
    
    private static SensorsApi? _sensorsApi;
    
    private static Notifications? _notifications;

    public static void SetConfig(IConfiguration configuration)
    {
        _config ??= configuration;
        _sensorsApi = new SensorsApi(configuration.GetSection("SensorsApi"));
    }

    public static SensorsApi SensorsApi()
    {
        return _sensorsApi!;
    }

    public static string UserFileName()
    {
        return _config!.GetValue<string>("UserFileName")!;
    }
}

public class SensorsApi
{
    private readonly IConfiguration _sensorsApi;

    public SensorsApi(IConfiguration sensorsApi)
    {
        _sensorsApi = sensorsApi;
    }

    public string Url()
    {
        return _sensorsApi.GetValue<string>("Url")!;
    }

    public int Port()
    {
        return _sensorsApi.GetValue<int>("Port");
    }

    public string BasePath()
    {
        return _sensorsApi.GetValue<string>("BasePath")!;
    }

    public string Command()
    {
        return _sensorsApi.GetValue<string>("Command")!;
    }
}

public class Notifications
{
    private readonly IConfiguration _notifications;

    public Notifications(IConfiguration notifications)
    {
        _notifications = notifications;
    }

    public string SenderEmail()
    {
        return _notifications.GetValue<string>("SenderEmail")!;
    }

    public string SenderPassword()
    {
        return _notifications.GetValue<string>("SenderPassword")!;
    }

    public string SenderName()
    {
        return _notifications.GetValue<string>("SenderName")!;
    }

    public string EmailHeader()
    {
        return _notifications.GetValue<string>("EmailHeader")!;
    }

    public string EmailBody()
    {
        return _notifications.GetValue<string>("EmailBody")!;
    }
}