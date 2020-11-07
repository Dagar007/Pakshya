using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, 
        UserManager<AppUser> userManager, 
        RoleManager<Role> roleManager)
        {
            if (!userManager.Users.Any())
            {
                 var roles = new List<Role>
                {
                    new Role { Id= Guid.NewGuid().ToString(), Name = "User"},
                    new Role { Id= Guid.NewGuid().ToString(), Name = "Moderator"},
                    new Role { Id= Guid.NewGuid().ToString(), Name = "Admin"},
                };

                foreach(var role in roles)
                {
                    roleManager.CreateAsync(role).Wait();
                     
                }
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        Id = "a",
                        DisplayName = "Bob",
                        Email = "bob@test.com",
                        UserName = "bob@test.com",
                        EmailConfirmed = true,
                        Birthday = DateTime.Now.AddYears(-18),
                        Gender = "male",
                        IsActive = true
                    },
                    new AppUser
                    {
                        Id = "b",
                        DisplayName = "Tom",
                        Email = "tom@test.com",
                        UserName= "tom@test.com",
                        EmailConfirmed = true,
                        Birthday = DateTime.Now.AddYears(-20),
                        Gender = "male",
                        IsActive = true
                    },
                    new AppUser
                    {
                        Id="c",
                        DisplayName = "Jane",
                        Email = "jane@test.com",
                        UserName = "jane@test.com",
                        EmailConfirmed = true,
                        Birthday = DateTime.Now.AddYears(-22),
                        Gender = "female",
                        IsActive = true
                    },

                };
                foreach(var user in users)
                {
                    userManager.CreateAsync(user, "Pa$$w0rd").Wait();
                }
                foreach(var user in userManager.Users)
                {
                    userManager.AddToRolesAsync(user, new[] {"User"}).Wait();
                }
               

                 var adminUser = new AppUser 
                {
                    Id= "d",
                    DisplayName= "Pakshya",
                    Email = "admin@test.com",
                    UserName = "admin@test.com",
                    EmailConfirmed = true,
                    IsActive = true
                };

                var result = userManager.CreateAsync(adminUser, "Pa$$w0rd").Result;
                if(result.Succeeded) 
                {
                    var admin = userManager.FindByEmailAsync("admin@test.com").Result;
                    await userManager.AddToRolesAsync(admin, new[] {"Admin"});
                }

            }

            if (!context.Posts.Any())
            {
                var posts = new List<Post>
                {
                    new Post
                        {
                            Heading = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue.",
                            Date = DateTime.Now.AddMonths(-7),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Etiam maximus tortor vitae ex ultrices, nec maximus arcu gravida. Sed orci odio, suscipit non feugiat sit amet, viverra eget tellus. Ut vestibulum vitae tellus et sollicitudin. Aenean augue leo, dignissim eu libero in, mollis auctor nibh. Quisque lobortis tempor lorem id bibendum. Sed mattis nisl maximus fringilla pretium. Curabitur et leo enim. Morbi sapien leo, malesuada vel neque vel, lobortis porttitor magna.",
                            
                            
                            //Url = null,
                            For = 3,
                            Against = 10,
                            IsActive = true,
                            UserPostLikes = new List<UserPostLike>
                            {
                                new UserPostLike 
                                {
                                    AppUserId = "a",
                                    IsAuthor = true,
                                    DateLiked = DateTime.Now.AddMonths(-2)
                                }
                            }
                            
                        },
                    new Post
                    {
                        Heading = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue.",
                        Date = DateTime.Now.AddMonths(-6),
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Etiam maximus tortor vitae ex ultrices, nec maximus arcu gravida. Sed orci odio, suscipit non feugiat sit amet, viverra eget tellus. Ut vestibulum vitae tellus et sollicitudin. Aenean augue leo, dignissim eu libero in, mollis auctor nibh. Quisque lobortis tempor lorem id bibendum. Sed mattis nisl maximus fringilla pretium. Curabitur et leo enim. Morbi sapien leo, malesuada vel neque vel, lobortis porttitor magna.",
                         
                        //Url = "https://dummyimage.com/640x4:3",
                        For = 13,
                     Against = 2,
                     IsActive = true,
                      UserPostLikes = new List<UserPostLike>
                            {
                                new UserPostLike 
                                {
                                    AppUserId = "b",
                                    IsAuthor = true,
                                    DateLiked = DateTime.Now.AddMonths(-2)
                                },
                                new UserPostLike 
                                {
                                    AppUserId = "c",
                                    IsAuthor = false,
                                     IsLiked = true,
                                    DateLiked = DateTime.Now.AddMonths(-2)
                                },
                            }
                    },
                    new Post
                    {
                        Heading = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue.",
                        Date = DateTime.Now.AddMonths(-5),
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Etiam maximus tortor vitae ex ultrices, nec maximus arcu gravida. Sed orci odio, suscipit non feugiat sit amet, viverra eget tellus. Ut vestibulum vitae tellus et sollicitudin. Aenean augue leo, dignissim eu libero in, mollis auctor nibh. Quisque lobortis tempor lorem id bibendum. Sed mattis nisl maximus fringilla pretium. Curabitur et leo enim. Morbi sapien leo, malesuada vel neque vel, lobortis porttitor magna.",
                      
                        //Url = "https://dummyimage.com/640x4:3",
                        For = 1400,
                    Against = 10,
                    IsActive = true,
                     UserPostLikes = new List<UserPostLike>
                            {
                                new UserPostLike 
                                {
                                    AppUserId = "a",
                                    IsAuthor = true,
                                    DateLiked = DateTime.Now.AddMonths(-2)
                                },
                                new UserPostLike 
                                {
                                    AppUserId = "c",
                                    IsAuthor = false,
                                     IsLiked = true,
                                    DateLiked = DateTime.Now.AddMonths(-2)
                                },
                            }
                    },
                    new Post
                    {
                        Heading = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue.",
                        Date = DateTime.Now.AddMonths(-4),
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Etiam maximus tortor vitae ex ultrices, nec maximus arcu gravida. Sed orci odio, suscipit non feugiat sit amet, viverra eget tellus. Ut vestibulum vitae tellus et sollicitudin. Aenean augue leo, dignissim eu libero in, mollis auctor nibh. Quisque lobortis tempor lorem id bibendum. Sed mattis nisl maximus fringilla pretium. Curabitur et leo enim. Morbi sapien leo, malesuada vel neque vel, lobortis porttitor magna.",
                        
                        //Url = "https://dummyimage.com/200x200/000/fff",
                        IsActive = true,
                        For = 0,
                    Against = 1010,
                     UserPostLikes = new List<UserPostLike>
                            {
                                new UserPostLike 
                                {
                                    AppUserId = "c",
                                    IsAuthor = true,
                                    DateLiked = DateTime.Now.AddMonths(-2)
                                },
                                new UserPostLike 
                                {
                                    AppUserId = "a",
                                    IsAuthor = false,
                                     IsLiked = true,
                                    DateLiked = DateTime.Now.AddMonths(-2)
                                },
                            }
                    },
                    new Post
                    {
                        Heading = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue.",
                        Date = DateTime.Now.AddMonths(-3),
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Etiam maximus tortor vitae ex ultrices, nec maximus arcu gravida. Sed orci odio, suscipit non feugiat sit amet, viverra eget tellus. Ut vestibulum vitae tellus et sollicitudin. Aenean augue leo, dignissim eu libero in, mollis auctor nibh. Quisque lobortis tempor lorem id bibendum. Sed mattis nisl maximus fringilla pretium. Curabitur et leo enim. Morbi sapien leo, malesuada vel neque vel, lobortis porttitor magna.",
                         
                        //Url = null,
                        IsActive = true,
                        For = 30,
                    Against = 31,
                     UserPostLikes = new List<UserPostLike>
                            {
                                new UserPostLike 
                                {
                                    AppUserId = "b",
                                    IsAuthor = true,
                                    DateLiked = DateTime.Now.AddMonths(-2)
                                },
                                new UserPostLike 
                                {
                                    AppUserId = "c",
                                    IsAuthor = false,
                                     IsLiked = true,
                                    DateLiked = DateTime.Now.AddMonths(-2)
                                },
                            }
                    },
                    new Post
                    {
                        Heading = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue.",
                        Date = DateTime.Now.AddMonths(-2),
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Etiam maximus tortor vitae ex ultrices, nec maximus arcu gravida. Sed orci odio, suscipit non feugiat sit amet, viverra eget tellus. Ut vestibulum vitae tellus et sollicitudin. Aenean augue leo, dignissim eu libero in, mollis auctor nibh. Quisque lobortis tempor lorem id bibendum. Sed mattis nisl maximus fringilla pretium. Curabitur et leo enim. Morbi sapien leo, malesuada vel neque vel, lobortis porttitor magna.",
                         
                        //Url = "https://dummyimage.com/200x200/000/fff",
                        For = 3,
                    Against = 10,
                    IsActive = true,
                     UserPostLikes = new List<UserPostLike>
                            {
                                new UserPostLike 
                                {
                                    AppUserId = "b",
                                    IsAuthor = true,
                                    DateLiked = DateTime.Now.AddMonths(-2)
                                },
                                new UserPostLike 
                                {
                                    AppUserId = "c",
                                    IsAuthor = false,
                                    IsLiked = true,
                                    DateLiked = DateTime.Now.AddMonths(-2)
                                },
                            }
                    },
                    new Post
                    {
                        Heading = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue.",
                        Date = DateTime.Now.AddMonths(-2),
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Etiam maximus tortor vitae ex ultrices, nec maximus arcu gravida. Sed orci odio, suscipit non feugiat sit amet, viverra eget tellus. Ut vestibulum vitae tellus et sollicitudin. Aenean augue leo, dignissim eu libero in, mollis auctor nibh. Quisque lobortis tempor lorem id bibendum. Sed mattis nisl maximus fringilla pretium. Curabitur et leo enim. Morbi sapien leo, malesuada vel neque vel, lobortis porttitor magna.",
                         
                        //Url = "https://dummyimage.com/200x200/000/fff",
                        For = 113,
                    Against = 10,
                    IsActive = true,
                     UserPostLikes = new List<UserPostLike>
                            {
                                new UserPostLike 
                                {
                                    AppUserId = "a",
                                    IsAuthor = true,
                                    DateLiked = DateTime.Now.AddMonths(-2)
                                },
                                new UserPostLike 
                                {
                                    AppUserId = "b",
                                    IsAuthor = false,
                                    IsLiked = true,
                                    DateLiked = DateTime.Now.AddMonths(-2)
                                },
                            }
                    },
                    new Post
                    {
                        Heading = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue.",
                        Date = DateTime.Now,
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Etiam maximus tortor vitae ex ultrices, nec maximus arcu gravida. Sed orci odio, suscipit non feugiat sit amet, viverra eget tellus. Ut vestibulum vitae tellus et sollicitudin. Aenean augue leo, dignissim eu libero in, mollis auctor nibh. Quisque lobortis tempor lorem id bibendum. Sed mattis nisl maximus fringilla pretium. Curabitur et leo enim. Morbi sapien leo, malesuada vel neque vel, lobortis porttitor magna.",
                        IsActive = true,
                       // Url = "https://dummyimage.com/200x200/000/fff",
                        For = 37,
                        Against = 13,
                        UserPostLikes = new List<UserPostLike>
                            {
                                new UserPostLike 
                                {
                                    AppUserId = "a",
                                    IsAuthor = true,
                                    DateLiked = DateTime.Now.AddMonths(-2)
                                },
                               
                            }
                    }


                };
                context.Posts.AddRange(posts);
                context.SaveChanges();
            }

            if(!context.Categories.Any())
            {
            context.Categories.Add(new Category { Id = Guid.NewGuid(), Value = "Politics", IsActive= true });
              context.Categories.Add(new Category { Id = Guid.NewGuid(), Value = "Economics", IsActive= true });
              context.Categories.Add(new Category { Id = Guid.NewGuid(), Value = "India", IsActive= true });
              context.Categories.Add(new Category { Id = Guid.NewGuid(), Value = "World", IsActive= true });
              context.Categories.Add(new Category { Id = Guid.NewGuid(), Value = "Sports", IsActive= true });
              context.Categories.Add(new Category { Id = Guid.NewGuid(), Value = "Random", IsActive= true });
              context.Categories.Add(new Category { Id = Guid.NewGuid(), Value = "Entertainment", IsActive= true });
              context.Categories.Add(new Category { Id = Guid.NewGuid(), Value = "Good Life", IsActive= true });
              context.Categories.Add(new Category { Id = Guid.NewGuid(), Value = "Fashion And Style", IsActive= true });
              context.Categories.Add(new Category { Id = Guid.NewGuid(), Value = "Writing", IsActive= true });
              context.Categories.Add(new Category { Id = Guid.NewGuid(), Value = "Computers", IsActive= true });
              context.Categories.Add(new Category { Id = Guid.NewGuid(), Value = "Philosophy", IsActive= true });
            await context.SaveChangesAsync();
                
            }
        }
    }
}