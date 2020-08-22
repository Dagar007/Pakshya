using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntitiesConfiguration
{
    public class UserRoleConfigurations : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserRole> builder)
        {
           builder.HasKey(ur => new {ur.RoleId, ur.UserId});
              builder.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId).IsRequired();
              builder.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId).IsRequired();
        }
    }
}