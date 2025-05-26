using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HFP.Infrastructure.EF.Models;

namespace HFP.Infrastructure.EF.Config.PurchaseInvoices
{
    internal sealed class PurchaseInvoiceReadEntityConfiguration : IEntityTypeConfiguration<PurchaseInvoiceReadModel>, IEntityTypeConfiguration<PurchaseInvoiceItemReadModel>
    {
        public void Configure(EntityTypeBuilder<PurchaseInvoiceItemReadModel> builder)
        {
            builder.ToTable("PurchaseInvoiceItems");

            builder.HasOne(u => u.PurchaseInvoice)
                .WithMany(p => p.Items)
                .HasForeignKey(ur => ur.PurchaseInvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

        }

        public void Configure(EntityTypeBuilder<PurchaseInvoiceReadModel> builder)
        {
            builder.ToTable("PurchaseInvoices");
            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.HasMany(u => u.Items)
                .WithOne(p => p.PurchaseInvoice)
                .HasForeignKey(ur => ur.PurchaseInvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }

}
