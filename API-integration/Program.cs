using Arquitectura_hexagonal.Ports.Primary;
using Arquitectura_Hexagonal.Entities;
using Arquitectura_Hexagonal.Ports.Secundary;
using Arquitectura_Hexagonal.Repositories.JsonRepository;
using Arquitectura_Hexagonal.Repositories.XMLRepository;
using Arquitectura_Hexagonal.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "products.xml");

builder.Services.AddSingleton<IRepository<Products>, XmlProductRepository>(provider => new XmlProductRepository(filePath));
builder.Services.AddTransient<IService, ProductService>();

// swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // Enable Swagger UI in development environment
    app.UseSwaggerUI();
    app.UseSwagger();
}

app.UseHttpsRedirection();


app.MapGet("/products", (IService service) =>
{
    var products = service.GetAll();
    return Results.Ok(products);
});

app.MapPost("/products", (IService service, string name, int price) =>
{
    service.Register(name, price);
    return Results.Ok();
});


/*
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");
*/

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
