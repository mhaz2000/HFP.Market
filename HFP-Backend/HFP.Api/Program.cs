using HFP.Api.Extensions;
using HFP.Infrastructure;
using HFP.Infrastructure.Logging;
using HFP.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalRConfig();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors(c =>
{
    c.WithOrigins("http://localhost:5173");
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowCredentials();
});

// Configure the HTTP request pipeline.
app.UseShared();
app.UseMiddleware<LoggingMiddleware>();
app.UseSignalR();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
