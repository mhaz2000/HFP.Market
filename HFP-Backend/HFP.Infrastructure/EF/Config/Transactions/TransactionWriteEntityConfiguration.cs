using HFP.Domain.Entities;
using HFP.Domain.ValueObjects.Products;
using HFP.Domain.ValueObjects.Transactinos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HFP.Infrastructure.EF.Config.Transactions
{
    internal class TransactionWriteEntityConfiguration : IEntityTypeConfiguration<Transaction>, IEntityTypeConfiguration<ProductTransaction>
    {
        public void Configure(EntityTypeBuilder<ProductTransaction> builder)
        {
            builder.HasKey(ur => ur.Id);

            builder.HasOne(ur => ur.Transaction)
                .WithMany(u => u.Products)
                .HasForeignKey(ur => ur.TransactionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ur => ur.Product)
                .WithMany(r => r.Transactions)
                .HasForeignKey(ur => ur.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Property(u => u.Quantity)
                .HasConversion(quantity => quantity.Value, quantity => ProductTransactionQuantity.Create(quantity))
                .IsRequired();

            builder.ToTable("ProductTransactions");
        }

        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");
            builder.HasKey(x => x.Id);

            builder.Property(u => u.BuyerId)
                .HasConversion(buyerId => buyerId.Value, buyerId => BuyerId.Create(buyerId))
                .IsRequired();

            builder.Property(u => u.Date)
                .IsRequired();

            builder.Property(u => u.Status)
                .IsRequired();
            
            builder.Property(u => u.Type)
                .IsRequired();

            builder.HasMany(u => u.Products)
                .WithOne()
                .HasForeignKey(ur => ur.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(p => !p.IsDeleted);

        }
    }
}
