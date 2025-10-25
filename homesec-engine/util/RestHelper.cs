namespace TestWebAPI1.util;

public static class RestHelper
{
    public static string BuildUrl(string baseUrl, int port, string path, params string[] subPaths)
    {
        var url = new UriBuilder(baseUrl)
        {
            Port = port
        };

        // var pathString = new PathString(path).Add(subPath).ToString();
        var pathString = new PathString(path);
        foreach (var subPath in subPaths)
        {
            pathString.Add(subPath);
        }
        url.Path = pathString.ToString();
        
        return url.Uri.ToString();
    }
}