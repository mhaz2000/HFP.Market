using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HFP.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HFP.Infrastructure.EF.Config.Transactions
{
    internal class TransactionReadEntityConfiguration : IEntityTypeConfiguration<TransactionReadModel>, IEntityTypeConfiguration<ProductTransactionReadModel>
    {
        public void Configure(EntityTypeBuilder<ProductTransactionReadModel> builder)
        {
            builder.ToTable("ProductTransactions");
            builder.HasKey(b => b.Id);

            builder.HasOne(ur => ur.Transaction)
                .WithMany(u => u.ProductTransactions)
                .HasForeignKey(ur => ur.TransactionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ur => ur.Product)
                .WithMany(r => r.ProductTransactions)
                .HasForeignKey(ur => ur.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public void Configure(EntityTypeBuilder<TransactionReadModel> builder)
        {
            builder.ToTable("Transactions");

            builder.HasMany(u => u.ProductTransactions)
                .WithOne(p => p.Transaction)
                .HasForeignKey(ur => ur.TransactionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(p => !p.IsDeleted);

        }
    }
}
