using HFP.Entrance.Components;
using HFP.Entrance.EndPoints;

bool isOccupied = true;
int counter = 0;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient("ServerAPI", client =>
{
    client.BaseAddress = new Uri("http://localhost:5022");
});

builder.Services.AddHttpClient("InteractiveAPI", client =>
{
    client.BaseAddress = new Uri("http://localhost:5000");
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRoomEndpoints();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
