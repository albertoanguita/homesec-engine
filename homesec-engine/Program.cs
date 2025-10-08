using TestWebAPI1;
using TestWebAPI1.users;
using TestWebAPI1.util.filestore;
using TestWebAPI1.util.genericstore;
using TestWebAPI1.util.genericstore.binders;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
ILogger logger = factory.CreateLogger("Program");
logger.LogInformation("Hello World! Logging is {Description}.", "fun");

HttpClient client = new HttpClient();
string url = "http://localhost:5501";
string url2 = "http://localhost:5501/test1";

HttpResponseMessage response;
string responseBody;

// Ejemplo: Obtener datos con GET
// HttpResponseMessage response = await client.GetAsync(url);
// response.EnsureSuccessStatusCode(); // Lanza una excepción si la solicitud no fue exitosa
// string responseBody = await response.Content.ReadAsStringAsync();
// Console.WriteLine(responseBody);

// Ejemplo: Enviar datos con POST
/*string jsonData = "{ \"key\": \"value\" }";
HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
response = await client.PostAsync(url2, content);
response.EnsureSuccessStatusCode();
responseBody = await response.Content.ReadAsStringAsync();
Console.WriteLine(responseBody);*/

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

var config = app.Configuration.GetSection("Config");

Configuration.SetConfig(config);

// await Task.Run(async () =>
// {
//     Task.Delay(5000).Wait();
//     await SensorsBridge.Command(Command.START);
// });


var instance = UserManager.GetInstance();




var connectionStr = "Server=127.0.0.1;Port=3307;Database=test;User=root;Password=my-secret-pw;";


var dbFileStore = new DbFileStore(connectionStr, "FileStore");

var ba = new byte[] { 0, 1, 2, 3 };

var exists = dbFileStore.Exists("path");
dbFileStore.Write("path", ba);
exists = dbFileStore.Exists("path");
var size = dbFileStore.Size("path");
var copy = dbFileStore.Read("path");
dbFileStore.Delete("path");
exists = dbFileStore.Exists("path");



var sqlBinder = new SqlBinder(connectionStr, "GenericStore");

GenericStore gs = new GenericStore(sqlBinder);

gs.SetValue("", "i", (int[])[5, 6, 7]);

var ii = gs.GetValue("", "i", (int[])[]);

sqlBinder.SetValue("grupo", "var1", 6);

var x = sqlBinder.GetIntValue("grupo", "var1");



// using var con = new MySqlConnection(connectionStr);
// try
// {
//     con.Open();
//     Console.WriteLine("Connection succeeded.");
//     
//     var sql = "SELECT * FROM tabla1";
//     var products = await con.QueryAsync<Tabla1>(sql, new { categoryID = 1});
//     
//     foreach (var product in products)
//     {
//         Console.WriteLine(product.Id);
//     }
// }
// catch (Exception ex)
// {
//     Console.WriteLine($"Connection error: {ex.Message}");
// }


// await SensorsBridge.Command(Command.START);


var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}