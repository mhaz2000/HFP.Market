using HFP.Entrance.Components;

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
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapGet("/api/room/status", () =>
{
    return Results.Ok(new { isOccupied });
});

app.MapPost("/api/room/enter/{id}", (string id) =>
{
    Console.WriteLine($"User entered with ID: {id}");

    // Example logic:
    isOccupied = true; // or some logic based on id
    return Results.Ok(new { Message = $"User {id} entered.", IsOccupied = isOccupied });
});

app.MapPost("/api/room/exit/{id}", (string id) =>
{
    isOccupied = false;
    return Results.Ok();
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
