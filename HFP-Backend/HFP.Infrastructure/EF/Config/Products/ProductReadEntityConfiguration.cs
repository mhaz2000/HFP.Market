using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HFP.Infrastructure.EF.Models;

namespace HFP.Infrastructure.EF.Config.Users
{

    internal sealed class ProductReadEntityConfiguration : IEntityTypeConfiguration<ProductReadModel>
    {
        public void Configure(EntityTypeBuilder<ProductReadModel> builder)
        {
            builder.ToTable("Products");

            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.HasMany(u => u.ProductTransactions)
                .WithOne(p => p.Product)
                .HasForeignKey(ur => ur.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
