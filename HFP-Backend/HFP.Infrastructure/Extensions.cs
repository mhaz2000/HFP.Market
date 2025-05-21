using HFP.Application;
using HFP.Application.Services;
using HFP.Infrastructure.EF;
using HFP.Infrastructure.Logging;
using HFP.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HFP.Infrastructure.Profiles;
using Microsoft.AspNetCore.Identity;
using HFP.Domain.Entities;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.SeedData;
using HFP.Infrastructure.Caching;

namespace HFP.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IPasswordHasher<object>, PasswordHasher<object>>();
            services.AddSql(configuration);
            services.AddApplication(configuration);

            services.AddAutoMapper(typeof(UserMappingProfile)); 
            services.AddAutoMapper(typeof(DiscountMappingProfile)); 

            services.AddSingleton<LoggingMiddleware>();

            services.AddSingleton<ITokenService, TokenService>();
            services.AddSingleton<ICaptchaService, CaptchaService>();
            services.AddSingleton<ICacheService, MemoryCacheService>();

            services.AddTransient<DatabaseSeeder>();

            return services;
        }

    }
}
