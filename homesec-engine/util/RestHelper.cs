namespace TestWebAPI1.util;

public static class RestHelper
{
    public static string BuildUrl(string baseUrl, int port, string path, string subPath)
    {
        var url = new UriBuilder(baseUrl);
        url.Port = port;
        
        var pathString = new PathString(path).Add(subPath).ToString();
        url.Path = pathString;
        
        return url.Uri.ToString();
    }
}