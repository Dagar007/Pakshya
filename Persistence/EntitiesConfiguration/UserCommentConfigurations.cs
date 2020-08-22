using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntitiesConfiguration
{
    public class UserCommentConfigurations : IEntityTypeConfiguration<UserComment>
    {
        public void Configure(EntityTypeBuilder<UserComment> builder)
        {
              builder.HasKey(ua => new { ua.AppUserId, ua.CommentId });
            builder.HasOne(u => u.AppUser)
              .WithMany(c => c.UserComments)
              .HasForeignKey(u => u.AppUserId);

            builder.HasOne(c => c.Comment)
              .WithMany(u => u.UserComments)
              .HasForeignKey(c => c.CommentId);
        }
    }
}