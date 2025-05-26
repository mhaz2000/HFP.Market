using HFP.Domain.Entities;
using HFP.Domain.ValueObjects.Products;
using HFP.Domain.ValueObjects.PurchaseInvoices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HFP.Infrastructure.EF.Config.PurchaseInvoices
{
    internal class PurchaseInvoiceWriteEntityConfiguration : IEntityTypeConfiguration<PurchaseInvoice>, IEntityTypeConfiguration<PurchaseInvoiceItem>
    {
        public void Configure(EntityTypeBuilder<PurchaseInvoice> builder)
        {
            builder.HasKey(ur => ur.Id);

            builder.Property(u => u.IsDeleted)
                .IsRequired();

            builder.HasMany(ur => ur.Items)
                .WithOne(t => t.PurchaseInvoice)
                .HasForeignKey(t => t.PurchaseInvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(u => u.Date)
                .HasConversion(date => date.Value, date => PurchaseInvoiceDate.Create(date))
                .IsRequired();

            builder.HasQueryFilter(p => !p.IsDeleted);
            builder.ToTable("PurchaseInvoices");

        }

        public void Configure(EntityTypeBuilder<PurchaseInvoiceItem> builder)
        {
            builder.HasKey(ur => ur.Id);

            builder.Property(u => u.Quantity)
                .HasConversion(quantity => quantity.Value, quantity => ProductQuantity.Create(quantity))
                .IsRequired();

            builder.Property(u => u.Name)
                .HasConversion(name => name.Value, name => ProductName.Create(name))
                .IsRequired();

            builder.Property(u => u.PurchasePrice)
                .HasConversion(price => price.Value, price => ProductPrice.Create(price))
                .IsRequired();

            builder.HasOne(ur => ur.PurchaseInvoice)
                .WithMany(u => u.Items)
                .HasForeignKey(ur => ur.PurchaseInvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("PurchaseInvoiceItems");

        }
    }
}
