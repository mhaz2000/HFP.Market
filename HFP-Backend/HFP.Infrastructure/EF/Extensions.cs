using HFP.Application.Services;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Options;
using HFP.Infrastructure.EF.Services;
using HFP.Shared.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HFP.Infrastructure.EF.Repositories.Base;
using HFP.Domain.Repositories.Base;
using HFP.Shared.Abstractions.Domain;

namespace HFP.Infrastructure.EF
{
    internal static class Extensions
    {
        public static IServiceCollection AddSql(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<DbInitializer>();

            var options = configuration.GetOptions<SqlOptions>("Sql");

            services.AddDbContext<ReadDbContext>(ctx => ctx.UseSqlServer(options.ConnectionString));
            services.AddDbContext<WriteDbContext>(ctx => ctx.UseSqlServer(options.ConnectionString));

            services.AddScoped<IUserReadService, UserReadService>();
            services.AddScoped<IRoleReadService, RoleReadService>();
            services.AddScoped<IProductReadService, ProductReadService>();
            services.AddScoped<IWarehousemanReadService, WarehousemanReadService>();
            services.AddScoped<ITransactionReadService, TransactionReadService>();


            services.Scan(scan => scan
           .FromAssemblyOf<GenericRepository<Entity<Guid>>>() 
           .AddClasses(classes => classes.AssignableTo(typeof(IGenericRepository<>)))
               .AsImplementedInterfaces()
               .WithScopedLifetime()
           .AddClasses(classes => classes.AssignableTo(typeof(GenericRepository<>)))
               .AsImplementedInterfaces()
               .WithScopedLifetime());


            return services;
        }
    }
}
