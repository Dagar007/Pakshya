using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntitiesConfiguration
{
    public class UserCommentLikeConfigurations : IEntityTypeConfiguration<UserCommentLike>
    {
        public void Configure(EntityTypeBuilder<UserCommentLike> builder)
        {
            builder.HasKey(ua => new { ua.AppUserId, ua.CommentId });
            builder.HasOne(u => u.AppUser)
              .WithMany(c => c.UserCommentLikes)
              .HasForeignKey(u => u.AppUserId);

            builder.HasOne(c => c.Comment)
              .WithMany(u => u.UserCommentLikes)
              .HasForeignKey(c => c.CommentId);
        }
    }
}