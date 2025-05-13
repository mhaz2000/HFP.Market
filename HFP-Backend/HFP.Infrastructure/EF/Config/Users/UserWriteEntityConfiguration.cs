using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HFP.Domain.Entities;
using HFP.Domain.Consts;
using HFP.Domain.ValueObjects.Users;
using HFP.Domain.ValueObjects.Roles;

namespace HFP.Infrastructure.EF.Config.Users
{
    internal sealed class UserWriteEntityConfiguration : IEntityTypeConfiguration<User>, IEntityTypeConfiguration<Role>, IEntityTypeConfiguration<UserRole>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);

            builder.Property(u => u.Username)
                .HasConversion(username => username.Value, username => Username.Create(username)) 
                .IsRequired();


            builder.Property(u => u.PasswordHash)
                .HasConversion(hash => hash.Value, hash => PasswordHash.FromHash(hash))
                .IsRequired();

            builder.Property(u => u.IsActive)
                .IsRequired();
  
            builder.HasMany(u => u.Roles)
                .WithOne() 
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        }

        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .HasConversion(name => name.Value, name => RoleName.Create(name))
                .IsRequired();

            builder.HasMany(r => r.UserRoles)
            .WithOne()
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Roles");
        }

        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(ur => ur.Id);

            builder.HasOne(ur => ur.User)
                .WithMany(u => u.Roles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("UserRoles");
        }
    }

}
