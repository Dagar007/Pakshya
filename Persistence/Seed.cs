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
        UserManager<AppUser> userManager, RoleManager<Role> roleManager)
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
                        UserName = "bob",
                        Email = "bob@test.com",
                        Birthday = DateTime.Now.AddYears(-18),
                        Gender = "male",
                        IsActive = true
                    },
                    new AppUser
                    {
                        Id = "b",
                        DisplayName = "Tom",
                        UserName = "tom",
                        Email = "tom@test.com",
                        Birthday = DateTime.Now.AddYears(-20),
                        Gender = "male",
                        IsActive = true
                    },
                    new AppUser
                    {
                        Id="c",
                        DisplayName = "Jane",
                        UserName = "jane",
                        Email = "jane@test.com",
                         Birthday = DateTime.Now.AddYears(-22),
                        Gender = "female",
                        IsActive = true
                    },

                };
                foreach(var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                    await userManager.AddToRoleAsync(user, "User");
                }

                 var adminUser = new AppUser 
                {
                    Id= "d",
                    DisplayName= "Pakshya",
                    UserName = "Admin",
                    Email = "admin@test.com",
                    EmailConfirmed = true,
                    IsActive = true
                };

                var result = userManager.CreateAsync(adminUser, "Pa$$w0rd").Result;
                if(result.Succeeded) 
                {
                    var admin = userManager.FindByNameAsync("Admin").Result;
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
                            UserPosts = new List<UserPost>
                            {
                                new UserPost 
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
                      UserPosts = new List<UserPost>
                            {
                                new UserPost 
                                {
                                    AppUserId = "b",
                                    IsAuthor = true,
                                    DateLiked = DateTime.Now.AddMonths(-2)
                                },
                                new UserPost 
                                {
                                    AppUserId = "c",
                                    IsAuthor = false,
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
                     UserPosts = new List<UserPost>
                            {
                                new UserPost 
                                {
                                    AppUserId = "a",
                                    IsAuthor = true,
                                    DateLiked = DateTime.Now.AddMonths(-2)
                                },
                                new UserPost 
                                {
                                    AppUserId = "c",
                                    IsAuthor = false,
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
                     UserPosts = new List<UserPost>
                            {
                                new UserPost 
                                {
                                    AppUserId = "c",
                                    IsAuthor = true,
                                    DateLiked = DateTime.Now.AddMonths(-2)
                                },
                                new UserPost 
                                {
                                    AppUserId = "a",
                                    IsAuthor = false,
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
                     UserPosts = new List<UserPost>
                            {
                                new UserPost 
                                {
                                    AppUserId = "b",
                                    IsAuthor = true,
                                    DateLiked = DateTime.Now.AddMonths(-2)
                                },
                                new UserPost 
                                {
                                    AppUserId = "c",
                                    IsAuthor = false,
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
                     UserPosts = new List<UserPost>
                            {
                                new UserPost 
                                {
                                    AppUserId = "b",
                                    IsAuthor = true,
                                    DateLiked = DateTime.Now.AddMonths(-2)
                                },
                                new UserPost 
                                {
                                    AppUserId = "c",
                                    IsAuthor = false,
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
                     UserPosts = new List<UserPost>
                            {
                                new UserPost 
                                {
                                    AppUserId = "a",
                                    IsAuthor = true,
                                    DateLiked = DateTime.Now.AddMonths(-2)
                                },
                                new UserPost 
                                {
                                    AppUserId = "b",
                                    IsAuthor = false,
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
                     UserPosts = new List<UserPost>
                            {
                                new UserPost 
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
        }
    }
}