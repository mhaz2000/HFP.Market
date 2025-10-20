using DocumentFormat.OpenXml.Drawing.Charts;
using HFP.Domain.Entities;
using HFP.Domain.ValueObjects.Transactinos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HFP.Infrastructure.EF.Config.Buyers
{
    internal class BuyerWriteEntityConfiguration : IEntityTypeConfiguration<Buyer>
    {
        public void Configure(EntityTypeBuilder<Buyer> builder)
        {
            builder.ToTable("Buyers");
            builder.HasKey(x => x.Id);

            builder.Property(u => u.BuyerId)
                .HasConversion(buyerId => buyerId.Value, buyerId => BuyerId.Create(buyerId))
                .IsRequired();

            builder.HasMany(u => u.Discounts)
                .WithOne()
                .HasForeignKey(ur => ur.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
