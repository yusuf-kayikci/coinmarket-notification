using CoinMarket.Application.Common.Interfaces;
using CoinMarket.Infrastructure.MessageQueue;
using CoinMarket.WebAPI.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ModelStateValidtor>();
    options.Filters.Add<ApiExceptionFilterAttribute>();
}).AddNewtonsoftJson();

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddScoped<IMessagePublisher, RabbitMqMessagePublisher>()
    .AddSingleton<QueueInitializerService>();

var app = builder.Build();


app.Services.GetRequiredService<QueueInitializerService>().Initialize();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }