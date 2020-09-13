using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntitiesConfiguration
{
    public class UserPostConfigurations : IEntityTypeConfiguration<UserPostLike>
    {
        public void Configure(EntityTypeBuilder<UserPostLike> builder)
        {
            builder.HasKey(ua => new { ua.AppUserId, ua.PostId });
            builder.HasOne(u => u.AppUser)
                    .WithMany(p => p.UserPostLikes)
                    .HasForeignKey(u => u.AppUserId);

            builder.HasOne(p => p.Post)
                    .WithMany(u => u.UserPostLikes)
                    .HasForeignKey(a => a.PostId);
        }
    }
}