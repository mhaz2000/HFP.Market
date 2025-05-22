using System.Text;
using System.Text.Json;

namespace HFP.Entrance.EndPoints
{
    public static class RoomEndpoints
    {
        private static bool isOccupied = false;

        public static void MapRoomEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/room/status", () =>
            {
                return Results.Ok(new { isOccupied });
            });

            app.MapPost("/api/room/enter/{id}", async (HttpContext context, string id) =>
            {
                var logger = context.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("RoomEndpoints");
                var httpClientFactory = context.RequestServices.GetRequiredService<IHttpClientFactory>();
                var client = httpClientFactory.CreateClient("InteractiveAPI");

                logger.LogInformation("User entered with ID: {Id}", id);
                isOccupied = true;

                var payload = new { BuyerId = id };
                var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync("/api/Interactive/CustomerEntered", content);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to notify Interactive API");
                    return Results.Problem("Failed to notify Interactive API.");
                }

                return Results.Ok(new { Message = $"User {id} entered.", IsOccupied = isOccupied });
            });

            app.MapPost("/api/room/exit/{id}", (string id) =>
            {
                isOccupied = false;
                return Results.Ok();
            });
        }
    }
}
