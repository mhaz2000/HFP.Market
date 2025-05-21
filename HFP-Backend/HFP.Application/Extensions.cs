using HFP.Shared;
using Microsoft.Extensions.DependencyInjection;
using HFP.Domain.Factories.interfaces;
using HFP.Domain.Factories;
using Microsoft.Extensions.Configuration;

namespace HFP.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserFactory, UserFactory>();
            services.AddScoped<IRoleFactory, RoleFactory>();
            services.AddScoped<IDiscountFactory, DiscountFactory>();
            services.AddScoped<IProductFactory, ProductFactory>();
            services.AddScoped<ITransactionFactory, TransactionFactory>();

            services.AddShared(configuration);


            return services;
        }
    }
}
