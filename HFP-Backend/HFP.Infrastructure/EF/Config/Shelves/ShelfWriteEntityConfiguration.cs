using HFP.Domain.Entities;
using HFP.Domain.ValueObjects.Shelves;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HFP.Infrastructure.EF.Config.Shelves
{
    internal class ShelfWriteEntityConfiguration : IEntityTypeConfiguration<Shelf>
    {
        public void Configure(EntityTypeBuilder<Shelf> builder)
        {
            builder.ToTable("Shelves");
            builder.HasKey(x => x.Id);

            builder.Property(u => u.Order)
                .HasConversion(order => order.Value, order => ShelfOrder.Create(order))
                .IsRequired();

            builder.HasMany(u => u.Products)
                .WithOne(c => c.Shelf)
                .HasForeignKey(c => c.ShelfId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
