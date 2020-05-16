using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser, Role, string , IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Value> Values { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserPost> UserPosts { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserFollowing> Followings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserComment> UserComments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole =>{
              userRole.HasKey(ur => new {ur.RoleId, ur.UserId});
              userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId).IsRequired();
              userRole.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId).IsRequired();
            });

            builder.Entity<Value>().HasData(
              new Value { Id = 1, Name = "Value 101" },
              new Value { Id = 2, Name = "Value 102" },
              new Value { Id = 3, Name = "Value 103" }
            );
            builder.Entity<Category>().HasData(
              new Category { Id = "abc", Value = "Politics" },
              new Category { Id = "def", Value = "Economics" },
              new Category { Id = "ghi", Value = "India" },
              new Category { Id = "jkl", Value = "World" },
              new Category { Id = "mno", Value = "Sports" },
              new Category { Id = "pqr", Value = "Random" },
              new Category { Id = "stu", Value = "Entertainment" },
              new Category { Id = "vwx", Value = "Good Life" },
              new Category { Id = "yza", Value = "Fashion And Style" },
              new Category { Id = "bcd", Value = "Writing" },
              new Category { Id = "efg", Value = "Computers" },
              new Category { Id = "hij", Value = "Philosophy" }
            );
            builder.Entity<UserPost>(x => x.HasKey(ua => new { ua.AppUserId, ua.PostId }));
            builder.Entity<UserPost>()
                    .HasOne(u => u.AppUser)
                    .WithMany(p => p.UserPosts)
                    .HasForeignKey(u => u.AppUserId);

            builder.Entity<UserPost>()
                    .HasOne(p => p.Post)
                    .WithMany(u => u.UserPosts)
                    .HasForeignKey(a => a.PostId);

            builder.Entity<UserComment>(x => x.HasKey(ua => new { ua.AppUserId, ua.CommentId }));
            builder.Entity<UserComment>()
              .HasOne(u => u.AppUser)
              .WithMany(c => c.UserComments)
              .HasForeignKey(u => u.AppUserId);

            builder.Entity<UserComment>()
              .HasOne(c => c.Comment)
              .WithMany(u => u.UserComments)
              .HasForeignKey(c => c.CommentId);

            builder.Entity<UserFollowing>(b =>
            {
                b.HasKey(k => new { k.ObserverId, k.TargetId });
                b.HasOne(o => o.Observer)
                .WithMany(f => f.Followings)
                .HasForeignKey(o => o.ObserverId)
                .OnDelete(DeleteBehavior.Restrict);

                b.HasKey(k => new { k.ObserverId, k.TargetId });
                b.HasOne(o => o.Target)
                .WithMany(f => f.Followers)
                .HasForeignKey(o => o.TargetId)
                .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
