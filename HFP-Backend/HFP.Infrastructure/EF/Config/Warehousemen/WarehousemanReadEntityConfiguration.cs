using HFP.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HFP.Infrastructure.EF.Config.Warehousemen;

internal class WarehousemanReadEntityConfiguration : IEntityTypeConfiguration<WarehousemanReadModel>
{
    public void Configure(EntityTypeBuilder<WarehousemanReadModel> builder)
    {
        builder.ToTable("Warehousemen");

        builder.HasKey(b => b.Id);
        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}