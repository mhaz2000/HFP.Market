using HFP.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HFP.Infrastructure.EF.Config.Shelves
{
    internal class ShelfReadEntityConfiguration : IEntityTypeConfiguration<ShelfReadModel>
    {
        public void Configure(EntityTypeBuilder<ShelfReadModel> builder)
        {
            builder.ToTable("Shelves");

            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.HasMany(u => u.Products)
                .WithOne(c => c.Shelf)
                .HasForeignKey(c => c.ShelfId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
