using System.Net.Mime;
using System.Text;
using System.Text.Json;
using TestWebAPI1.internal_api;
using TestWebAPI1.sensors.dtos;
using TestWebAPI1.util;

namespace TestWebAPI1.sensors;

public static class SensorsBridge
{
    public static async Task<bool> Command(Command command)
    {
        var dto = new CommandDto()
        {
            command = command.ToString()
        };
        var sensors = Configuration.SensorsApi();
        var url = RestHelper.BuildUrl(sensors.Url(), sensors.Port(), sensors.BasePath(), sensors.Command());
        
        var body = JsonSerializer.Serialize(dto);
        
        HttpContent content = new StringContent(body, Encoding.UTF8, MediaTypeNames.Application.Json);
        HttpClient client = new HttpClient();
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
    
}