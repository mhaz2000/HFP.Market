using HFP.Application.Commands.Interactive.Handlers;
using HFP.Domain.Factories;
using HFP.Domain.Factories.interfaces;
using HFP.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HFP.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<SendDoorCodeCommandHandler>();

            services.AddScoped<IUserFactory, UserFactory>();
            services.AddScoped<IRoleFactory, RoleFactory>();
            services.AddScoped<IDiscountFactory, DiscountFactory>();
            services.AddScoped<IProductFactory, ProductFactory>();
            services.AddScoped<ITransactionFactory, TransactionFactory>();
            services.AddScoped<IBuyerFactory, BuyerFactory>();
            services.AddScoped<IWarehousemanFactory, WarehousemanFactory>();
            services.AddScoped<IShelfFactory, ShelfFactory>();
            services.AddScoped<IPurchaseInvoiceFactory, PurchaseInvoiceFactory>();

            services.AddShared(configuration);


            return services;
        }
    }
}
