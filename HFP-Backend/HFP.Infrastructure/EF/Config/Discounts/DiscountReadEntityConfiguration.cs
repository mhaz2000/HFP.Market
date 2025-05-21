using HFP.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HFP.Infrastructure.EF.Config.Discounts
{

    internal sealed class DiscountReadEntityConfiguration : IEntityTypeConfiguration<DiscountReadModel>, IEntityTypeConfiguration<DiscountBuyerReadModel>
    {
        public void Configure(EntityTypeBuilder<DiscountReadModel> builder)
        {
            builder.ToTable("Discounts");

            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.HasMany(u => u.DiscountBuyers)
                .WithOne(p => p.Discount)
                .HasForeignKey(ur => ur.DiscountId)
                .OnDelete(DeleteBehavior.Restrict);

        }

        public void Configure(EntityTypeBuilder<DiscountBuyerReadModel> builder)
        {

            builder.ToTable("DiscountBuyers");
            builder.HasKey(b => b.Id);


            builder.HasOne(ur => ur.Discount)
                .WithMany(u => u.DiscountBuyers)
                .HasForeignKey(ur => ur.DiscountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ur => ur.Buyer)
                .WithMany(r => r.DiscountBuyers)
                .HasForeignKey(ur => ur.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
