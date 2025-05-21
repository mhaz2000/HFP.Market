using HFP.Domain.Entities;
using HFP.Domain.ValueObjects.Transactinos;
using HFP.Infrastructure.EF.Config;
using HFP.Shared.Abstractions.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HFP.Infrastructure.EF.Contexts
{
    internal sealed class WriteDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<DiscountBuyer> DiscountBuyers { get; set; }
        public DbSet<ProductTransaction> ProductTransactions { get; set; }

        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DbContextConfigurationApplier.ApplyWriteConfigurations(modelBuilder);


            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(CreateSoftDeleteFilter(entityType.ClrType));
                }
            }
        }

        private static LambdaExpression CreateSoftDeleteFilter(Type entityType)
        {
            var parameter = Expression.Parameter(entityType, "e");
            var property = Expression.Property(parameter, nameof(ISoftDeletable.IsDeleted));
            var filter = Expression.Lambda(Expression.Not(property), parameter);
            return filter;
        }
    }
}
