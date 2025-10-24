using System.Net.Mime;
using System.Text;
using System.Text.Json;
using TestWebAPI1.internal_api;
using TestWebAPI1.sensors.dtos;
using TestWebAPI1.util;

namespace TestWebAPI1.sensors;

public static class SensorsBridge
{
    public enum SensorsState
    {
        // todo
    }
    
    public static async Task<string?> State()
    {
        var sensors = Configuration.SensorsApi();
        var url = RestHelper.BuildUrl(sensors.Url(), sensors.Port(), sensors.BasePath(), sensors.State());
        
        var client = new HttpClient();
        var response = await client.GetAsync(url);
        try
        {
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsStringAsync().Result;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public static async Task<bool> Command(Command command)
    {
        var dto = new CommandDto()
        {
            Command = command.ToString()
        };
        var sensors = Configuration.SensorsApi();
        var url = RestHelper.BuildUrl(sensors.Url(), sensors.Port(), sensors.BasePath(), sensors.Command());
        
        var body = JsonSerializer.Serialize(dto);
        
        HttpContent content = new StringContent(body, Encoding.UTF8, MediaTypeNames.Application.Json);
        var client = new HttpClient();
        var response = await client.PostAsync(url, content);
        try
        {
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }

        //var responseBody = await response.Content.ReadFromJsonAsync<ResponseDto>();
        var responseStr = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseStr);

        return true;
    }

    public static async Task<bool> SetCallbackUrl(string callbackUrl)
    {
        var dto = new SetCallbackUrlDto()
        {
            Url = callbackUrl
        };
        var sensors = Configuration.SensorsApi();
        var url = RestHelper.BuildUrl(sensors.Url(), sensors.Port(), sensors.BasePath(), sensors.SetCallbackUrl());
        
        var body = JsonSerializer.Serialize(dto);
        
        HttpContent content = new StringContent(body, Encoding.UTF8, MediaTypeNames.Application.Json);
        var client = new HttpClient();
        var response = await client.PostAsync(url, content);
        try
        {
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadFromJsonAsync<SetCallbackUrlResponseDto>();
            
            return responseBody != null && responseBody.Success;
        }
        catch (Exception e)
        {
            return false;
        }

        //var responseBody = await response.Content.ReadFromJsonAsync<ResponseDto>();
        var responseStr = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseStr);

        return true;
    }
    
}