using HFP.Domain.Entities;
using HFP.Domain.ValueObjects.Discount;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HFP.Infrastructure.EF.Config.Discounts
{
    internal class DiscountWriteEntityConfiguration : IEntityTypeConfiguration<Discount>, IEntityTypeConfiguration<DiscountBuyer>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable("Discounts");
            builder.HasKey(x => x.Id);

            builder.Property(u => u.Code)
                .HasConversion(discountCode => discountCode.Value, discountCode => DiscountCode.Create(discountCode))
                .IsRequired();

            builder.Property(u => u.Name)
                .HasConversion(discountName=> discountName.Value, discountName=> DiscountName.Create(discountName))
                .IsRequired();

            builder.Property(u => u.Percentage)
                .HasConversion(discountPercentage => discountPercentage.Value, discountPercentage => DiscountPercentage.Create(discountPercentage))
                .IsRequired();
           
            builder.Property(u => u.MaxAmount)
                .HasConversion(discountMaxAmount => discountMaxAmount != null ? discountMaxAmount.Value : (decimal?)null,
                value => value.HasValue ? DiscountMaxAmount.Create(value.Value) : null);


            builder.Property(u => u.Type)
                .IsRequired();

            builder.Property(u => u.UsageLimitPerUser)
                .HasConversion(usageLimitPerUser => usageLimitPerUser.Value, usageLimitPerUser => DiscountUsageLimitPerUser.Create(usageLimitPerUser))
                .IsRequired();

            builder.OwnsOne(u => u.Date, dd =>
            {
                dd.Property(d => d.StartDate)
                  .HasColumnName("StartDate")
                  .IsRequired();

                dd.Property(d => d.EndDate)
                  .HasColumnName("EndDate")
                  .IsRequired();
            });

            builder.HasMany(u => u.Buyers)
                .WithOne()
                .HasForeignKey(ur => ur.DiscountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(p => !p.IsDeleted);
        }

        public void Configure(EntityTypeBuilder<DiscountBuyer> builder)
        {

            builder.HasKey(ur => ur.Id);

            builder.HasOne(ur => ur.Discount)
                .WithMany(u => u.Buyers)
                .HasForeignKey(ur => ur.DiscountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ur => ur.Buyer)
                .WithMany(r => r.Discounts)
                .HasForeignKey(ur => ur.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("DiscountBuyers");
        }
    }
}
