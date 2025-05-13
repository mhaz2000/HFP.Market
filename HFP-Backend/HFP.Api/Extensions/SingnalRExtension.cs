using HFP.Api.Hubs;

namespace HFP.Api.Extensions
{
    public static class SingnalRExtension
    {
        public static IServiceCollection AddSignalRConfig(this IServiceCollection services)
        {
            services.AddSignalR();
            
            return services;
        }

        public static WebApplication UseSignalR(this WebApplication app)
        {
            app.MapHub<SystemHub>("/hubs/ws");
            return app;
        }
    }
}
