using Microsoft.EntityFrameworkCore;
using HFP.Domain.Entities;
using HFP.Infrastructure.EF.Config.Users;
using HFP.Infrastructure.EF.Models;
using HFP.Infrastructure.EF.Config.Transactions;

namespace HFP.Infrastructure.EF.Config
{
    public static class DbContextConfigurationApplier
    {
        public static void ApplyReadConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<UserReadModel>(new UserReadEntityConfiguration());
            modelBuilder.ApplyConfiguration<RoleReadModel>(new UserReadEntityConfiguration());
            modelBuilder.ApplyConfiguration<UserRoleReadModel>(new UserReadEntityConfiguration());

            modelBuilder.ApplyConfiguration<ProductReadModel>(new ProductReadEntityConfiguration());
            modelBuilder.ApplyConfiguration<TransactionReadModel>(new TransactionReadEntityConfiguration());
            modelBuilder.ApplyConfiguration<ProductTransactionReadModel>(new TransactionReadEntityConfiguration());
        }


        public static void ApplyWriteConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<User>(new UserWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<Role>(new UserWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<UserRole>(new UserWriteEntityConfiguration());

            modelBuilder.ApplyConfiguration<Product>(new ProductWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<Transaction>(new TransactionWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<ProductTransaction>(new TransactionWriteEntityConfiguration());
        }
    }
}
