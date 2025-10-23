using HFP.Domain.Entities;
using HFP.Domain.ValueObjects.Consumers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HFP.Infrastructure.EF.Config.Consumers;

internal class ConsumerWriteEntityConfiguration : IEntityTypeConfiguration<Consumer>
{
    public void Configure(EntityTypeBuilder<Consumer> builder)
    {
        builder.HasKey(ur => ur.Id);

        builder.Property(u => u.Name)
            .HasConversion(name => name.Value, name => ConsumerName.Create(name))
            .IsRequired();

        builder.Property(u => u.UId)
            .HasConversion(uid => uid.Value, uid => ConsumerUId.Create(uid))
            .IsRequired();

        builder.ToTable("Consumers");
    }
}
