var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Check if the application is in development environment.
// If true, enable Swagger and Swagger UI for API documentation and visualization.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirect HTTP requests to HTTPS for enhanced security.
app.UseHttpsRedirection();

// Array of weather summaries used to generate random weather forecast data.
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// Define an endpoint "/weatherforecast" that returns a randomly generated weather forecast for the next 5 days.
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
// Name the endpoint explicitly for Swagger documentation.
// Enable OpenAPI documentation for this endpoint.
.WithName("GetWeatherForecast")
.WithOpenApi();

// Start executing the web application.
app.Run();

// Define the WeatherForecast record representing weather forecast data.
// Automatically calculates temperature in Fahrenheit from Celsius.
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
