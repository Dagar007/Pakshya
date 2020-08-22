using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntitiesConfiguration
{
    public class UserInterestConfigurations : IEntityTypeConfiguration<UserInterest>
    {
        public void Configure(EntityTypeBuilder<UserInterest> builder)
        {
            builder.HasKey(x => new {x.AppUserId, x.CategoryId });
            builder.HasOne(x => x.AppUser).WithMany(x => x.UserInterests).HasForeignKey(x => x.AppUserId);
            builder.HasOne(x => x.Category).WithMany(x => x.UserInterests).HasForeignKey(x => x.CategoryId);
        }
    }
}