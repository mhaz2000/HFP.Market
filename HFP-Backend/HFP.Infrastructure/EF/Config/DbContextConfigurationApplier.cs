using HFP.Domain.Entities;
using HFP.Infrastructure.EF.Config.Buyers;
using HFP.Infrastructure.EF.Config.Discounts;
using HFP.Infrastructure.EF.Config.Products;
using HFP.Infrastructure.EF.Config.PurchaseInvoices;
using HFP.Infrastructure.EF.Config.Shelves;
using HFP.Infrastructure.EF.Config.Transactions;
using HFP.Infrastructure.EF.Config.Users;
using HFP.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace HFP.Infrastructure.EF.Config
{
    public static class DbContextConfigurationApplier
    {
        public static void ApplyReadConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<UserReadModel>(new UserReadEntityConfiguration());
            modelBuilder.ApplyConfiguration<RoleReadModel>(new UserReadEntityConfiguration());
            modelBuilder.ApplyConfiguration<UserRoleReadModel>(new UserReadEntityConfiguration());

            modelBuilder.ApplyConfiguration(new BuyerReadEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ShelfReadEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductReadEntityConfiguration());
            modelBuilder.ApplyConfiguration<DiscountReadModel>(new DiscountReadEntityConfiguration());
            modelBuilder.ApplyConfiguration<DiscountBuyerReadModel>(new DiscountReadEntityConfiguration());
            modelBuilder.ApplyConfiguration<TransactionReadModel>(new TransactionReadEntityConfiguration());
            modelBuilder.ApplyConfiguration<ProductTransactionReadModel>(new TransactionReadEntityConfiguration());
            modelBuilder.ApplyConfiguration<PurchaseInvoiceReadModel>(new PurchaseInvoiceReadEntityConfiguration());
            modelBuilder.ApplyConfiguration<PurchaseInvoiceItemReadModel>(new PurchaseInvoiceReadEntityConfiguration());
        }


        public static void ApplyWriteConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ShelfWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BuyerWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductWriteEntityConfiguration());

            modelBuilder.ApplyConfiguration<User>(new UserWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<Role>(new UserWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<UserRole>(new UserWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<Discount>(new DiscountWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<DiscountBuyer>(new DiscountWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<Transaction>(new TransactionWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<ProductTransaction>(new TransactionWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<PurchaseInvoice>(new PurchaseInvoiceWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<PurchaseInvoiceItem>(new PurchaseInvoiceWriteEntityConfiguration());
        }
    }
}
