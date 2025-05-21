using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HFP.Domain.Entities;
using HFP.Domain.Consts;
using HFP.Domain.ValueObjects.Users;
using HFP.Domain.ValueObjects.Roles;
using HFP.Domain.ValueObjects.Products;

namespace HFP.Infrastructure.EF.Config.Users
{
    internal sealed class ProductWriteEntityConfiguration : IEntityTypeConfiguration<Product>
    {

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);

            builder.Property(u => u.Name)
                .HasConversion(productName => productName.Value, productName => ProductName.Create(productName))
                .IsRequired();

            builder.Property(u => u.Code)
                .HasConversion(productCode => productCode.Value, productCode => ProductCode.Create(productCode))
                .IsRequired();

            builder.Property(u => u.Quantity)
                .HasConversion(productQuantity => productQuantity.Value, productQuantity => ProductQuantity.Create(productQuantity))
                .IsRequired();

            builder.Property(u => u.Price)
                .HasConversion(productPrice => productPrice.Value, productPrice => ProductPrice.Create(productPrice))
                .IsRequired();

            builder.Property(u => u.PurchasePrice)
                .HasConversion(productPurchasePrice => productPurchasePrice.Value, productPurchasePrice => ProductPrice.Create(productPurchasePrice))
                .IsRequired();

            builder.HasMany(u => u.Transactions)
                .WithOne()
                .HasForeignKey(ur => ur.TransactionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(p => !p.IsDeleted);

        }
    }

}
