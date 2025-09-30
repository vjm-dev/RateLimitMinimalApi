using RateLimitMinimalApi.Api.Configs;
using RateLimitMinimalApi.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure dependencies
builder.Services.ConfigureDependencies();

// Configure Rate Limiting
builder.Services.ConfigureRateLimiting();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable Rate Limiting middleware
app.UseRateLimiter();

// Map endpoints
app.MapSystemEndpoints();
app.MapProductEndpoints();

app.Run();
