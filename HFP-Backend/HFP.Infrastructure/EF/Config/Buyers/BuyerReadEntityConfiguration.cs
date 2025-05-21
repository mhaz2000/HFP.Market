using HFP.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HFP.Infrastructure.EF.Config.Buyers
{
    internal class BuyerReadEntityConfiguration : IEntityTypeConfiguration<BuyerReadModel>
    {
        public void Configure(EntityTypeBuilder<BuyerReadModel> builder)
        {
            builder.ToTable("Buyers");

            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.HasMany(u => u.DiscountBuyers)
                .WithOne(p => p.Buyer)
                .HasForeignKey(ur => ur.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
