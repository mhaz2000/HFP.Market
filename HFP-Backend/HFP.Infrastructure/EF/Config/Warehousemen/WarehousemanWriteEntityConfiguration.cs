using HFP.Domain.Entities;
using HFP.Domain.ValueObjects.Warehouseman;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HFP.Infrastructure.EF.Config.Warehousemen;

internal class WarehousemanWriteEntityConfiguration : IEntityTypeConfiguration<Warehouseman>
{
    public void Configure(EntityTypeBuilder<Warehouseman> builder)
    {
        builder.HasKey(ur => ur.Id);

        builder.Property(u => u.Name)
            .HasConversion(name => name.Value, name => WarehousemanName.Create(name))
            .IsRequired();

        builder.Property(u => u.UId)
            .HasConversion(uid => uid.Value, uid => WarehousemanUId.Create(uid))
            .IsRequired();

        builder.ToTable("Warehousemen");
    }
}
