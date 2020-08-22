using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntitiesConfiguration
{
    public class UserPostConfigurations : IEntityTypeConfiguration<UserPost>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserPost> builder)
        {
            builder.HasKey(ua => new { ua.AppUserId, ua.PostId });
            builder.HasOne(u => u.AppUser)
                    .WithMany(p => p.UserPosts)
                    .HasForeignKey(u => u.AppUserId);

            builder.HasOne(p => p.Post)
                    .WithMany(u => u.UserPosts)
                    .HasForeignKey(a => a.PostId);
        }
    }
}