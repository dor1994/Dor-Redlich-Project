using RateFetcherService.Repository;
using RateFetcherService.Repository.Interfaces;
using RateFetcherService.Services;
using RatePrinter.Services;
using RatePrinter.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Configure file-based repository
builder.Services.AddSingleton<IExchangeRateRepository>(
    new ExchangeRateRepository("exchange_rates.json")
);

// Add background service for rate fetching
builder.Services.AddScoped<IExchangeRateService, ExchangeRateService>();
builder.Services.AddHostedService<ExchangeRateFetcherService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
