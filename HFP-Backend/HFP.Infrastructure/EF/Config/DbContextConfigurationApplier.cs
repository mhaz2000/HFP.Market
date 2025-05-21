using Microsoft.EntityFrameworkCore;
using HFP.Domain.Entities;
using HFP.Infrastructure.EF.Config.Users;
using HFP.Infrastructure.EF.Models;
using HFP.Infrastructure.EF.Config.Transactions;
using HFP.Infrastructure.EF.Config.Buyers;
using HFP.Infrastructure.EF.Config.Discounts;

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
            modelBuilder.ApplyConfiguration(new ProductReadEntityConfiguration());
            modelBuilder.ApplyConfiguration<DiscountReadModel>(new DiscountReadEntityConfiguration());
            modelBuilder.ApplyConfiguration<DiscountBuyerReadModel>(new DiscountReadEntityConfiguration());
            modelBuilder.ApplyConfiguration<TransactionReadModel>(new TransactionReadEntityConfiguration());
            modelBuilder.ApplyConfiguration<ProductTransactionReadModel>(new TransactionReadEntityConfiguration());
        }


        public static void ApplyWriteConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<User>(new UserWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<Role>(new UserWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<UserRole>(new UserWriteEntityConfiguration());

            modelBuilder.ApplyConfiguration(new BuyerWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<Discount>(new DiscountWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<DiscountBuyer>(new DiscountWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<Transaction>(new TransactionWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<ProductTransaction>(new TransactionWriteEntityConfiguration());
        }
    }
}
