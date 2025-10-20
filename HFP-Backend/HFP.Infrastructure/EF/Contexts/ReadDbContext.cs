using HFP.Infrastructure.EF.Config;
using Microsoft.EntityFrameworkCore;
using HFP.Infrastructure.EF.Models;
using HFP.Domain.Entities;

namespace HFP.Infrastructure.EF.Contexts
{
    internal sealed class ReadDbContext : DbContext
    {
        public DbSet<UserReadModel> Users { get; set; }
        public DbSet<RoleReadModel> Roles { get; set; }
        public DbSet<BuyerReadModel> Buyers { get; set; }
        public DbSet<ShelfReadModel> Shelves { get; set; }
        public DbSet<ProductReadModel> Products { get; set; }
        public DbSet<DiscountReadModel> Discounts { get; set; }
        public DbSet<UserRoleReadModel> UserRoles { get; set; }
        public DbSet<TransactionReadModel> Transactions { get; set; }
        public DbSet<DiscountBuyerReadModel> DiscountBuyers { get; set; }
        public DbSet<PurchaseInvoiceReadModel> PurchaseInvoices { get; set; }
        public DbSet<ProductTransactionReadModel> ProductTransactions { get; set; }
        public DbSet<PurchaseInvoiceItemReadModel> PurchaseInvoiceItems { get; set; }

        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DbContextConfigurationApplier.ApplyReadConfigurations(modelBuilder);
        }
    }
}
