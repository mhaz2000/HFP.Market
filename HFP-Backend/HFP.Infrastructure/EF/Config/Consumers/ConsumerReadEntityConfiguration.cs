using HFP.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HFP.Infrastructure.EF.Config.Consumers;

internal class ConsumerReadEntityConfiguration : IEntityTypeConfiguration<ConsumerReadModel>
{
    public void Configure(EntityTypeBuilder<ConsumerReadModel> builder)
    {
        builder.ToTable("Consumers");

        builder.HasKey(b => b.Id);
        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}