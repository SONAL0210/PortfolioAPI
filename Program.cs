using PortfolioApi.Services;
using PortfolioApi.Repositories;
using PortfolioApi.Middleware;
using Microsoft.EntityFrameworkCore;
using PortfolioApi.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<PortfolioService>();

builder.Services.AddScoped<IPortfolioRepository,PortfolioRepository>();

builder.Services.AddControllers();

builder.Services.AddMemoryCache();
builder.Services.AddScoped<IStockService, StockService>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "PortfolioApi_";
});

builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseSqlite("Data Source=portfolio.db"));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

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
