using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Persistence
{
  public class Seed
  {
    public static void SeedData(DataContext context)
    {
      if (!context.Posts.Any())
      {
        var posts = new List<Post>
                {
                    new Post
                        {
                            Heading = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue.",
                            Date = DateTime.Now.AddMonths(-7),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Etiam maximus tortor vitae ex ultrices, nec maximus arcu gravida. Sed orci odio, suscipit non feugiat sit amet, viverra eget tellus. Ut vestibulum vitae tellus et sollicitudin. Aenean augue leo, dignissim eu libero in, mollis auctor nibh. Quisque lobortis tempor lorem id bibendum. Sed mattis nisl maximus fringilla pretium. Curabitur et leo enim. Morbi sapien leo, malesuada vel neque vel, lobortis porttitor magna.",
                            Category = "politics",
                            Url = null,
                            For = 3,
                            Against = 10,
                        },
                    new Post
                    {
                        Heading = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue.",
                        Date = DateTime.Now.AddMonths(-6),
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Etiam maximus tortor vitae ex ultrices, nec maximus arcu gravida. Sed orci odio, suscipit non feugiat sit amet, viverra eget tellus. Ut vestibulum vitae tellus et sollicitudin. Aenean augue leo, dignissim eu libero in, mollis auctor nibh. Quisque lobortis tempor lorem id bibendum. Sed mattis nisl maximus fringilla pretium. Curabitur et leo enim. Morbi sapien leo, malesuada vel neque vel, lobortis porttitor magna.",
                        Category = "economy",
                        Url = "https://dummyimage.com/640x4:3",
                        For = 13,
                    Against = 2,
                    },
                    new Post
                    {
                        Heading = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue.",
                        Date = DateTime.Now.AddMonths(-5),
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Etiam maximus tortor vitae ex ultrices, nec maximus arcu gravida. Sed orci odio, suscipit non feugiat sit amet, viverra eget tellus. Ut vestibulum vitae tellus et sollicitudin. Aenean augue leo, dignissim eu libero in, mollis auctor nibh. Quisque lobortis tempor lorem id bibendum. Sed mattis nisl maximus fringilla pretium. Curabitur et leo enim. Morbi sapien leo, malesuada vel neque vel, lobortis porttitor magna.",
                        Category = "sports",
                        Url = "https://dummyimage.com/640x4:3",
                        For = 1400,
                    Against = 10,
                    },
                    new Post
                    {
                        Heading = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue.",
                        Date = DateTime.Now.AddMonths(-4),
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Etiam maximus tortor vitae ex ultrices, nec maximus arcu gravida. Sed orci odio, suscipit non feugiat sit amet, viverra eget tellus. Ut vestibulum vitae tellus et sollicitudin. Aenean augue leo, dignissim eu libero in, mollis auctor nibh. Quisque lobortis tempor lorem id bibendum. Sed mattis nisl maximus fringilla pretium. Curabitur et leo enim. Morbi sapien leo, malesuada vel neque vel, lobortis porttitor magna.",
                        Category = "bussiness",
                        Url = "https://dummyimage.com/200x200/000/fff",
                        For = 0,
                    Against = 1010,
                    },
                    new Post
                    {
                        Heading = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue.",
                        Date = DateTime.Now.AddMonths(-3),
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Etiam maximus tortor vitae ex ultrices, nec maximus arcu gravida. Sed orci odio, suscipit non feugiat sit amet, viverra eget tellus. Ut vestibulum vitae tellus et sollicitudin. Aenean augue leo, dignissim eu libero in, mollis auctor nibh. Quisque lobortis tempor lorem id bibendum. Sed mattis nisl maximus fringilla pretium. Curabitur et leo enim. Morbi sapien leo, malesuada vel neque vel, lobortis porttitor magna.",
                        Category = "india",
                        Url = null,
                        For = 30,
                    Against = 31,
                    },
                    new Post
                    {
                        Heading = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue.",
                        Date = DateTime.Now.AddMonths(-2),
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Etiam maximus tortor vitae ex ultrices, nec maximus arcu gravida. Sed orci odio, suscipit non feugiat sit amet, viverra eget tellus. Ut vestibulum vitae tellus et sollicitudin. Aenean augue leo, dignissim eu libero in, mollis auctor nibh. Quisque lobortis tempor lorem id bibendum. Sed mattis nisl maximus fringilla pretium. Curabitur et leo enim. Morbi sapien leo, malesuada vel neque vel, lobortis porttitor magna.",
                        Category = "world",
                        Url = "https://dummyimage.com/200x200/000/fff",
                        For = 3,
                    Against = 10,
                    },
                    new Post
                    {
                        Heading = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue.",
                        Date = DateTime.Now.AddMonths(-2),
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Etiam maximus tortor vitae ex ultrices, nec maximus arcu gravida. Sed orci odio, suscipit non feugiat sit amet, viverra eget tellus. Ut vestibulum vitae tellus et sollicitudin. Aenean augue leo, dignissim eu libero in, mollis auctor nibh. Quisque lobortis tempor lorem id bibendum. Sed mattis nisl maximus fringilla pretium. Curabitur et leo enim. Morbi sapien leo, malesuada vel neque vel, lobortis porttitor magna.",
                        Category = "random",
                        Url = "https://dummyimage.com/200x200/000/fff",
                        For = 113,
                    Against = 10,
                    },
                    new Post
                    {
                        Heading = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue.",
                        Date = DateTime.Now,
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna erat, malesuada in felis at, tincidunt fringilla augue. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Etiam maximus tortor vitae ex ultrices, nec maximus arcu gravida. Sed orci odio, suscipit non feugiat sit amet, viverra eget tellus. Ut vestibulum vitae tellus et sollicitudin. Aenean augue leo, dignissim eu libero in, mollis auctor nibh. Quisque lobortis tempor lorem id bibendum. Sed mattis nisl maximus fringilla pretium. Curabitur et leo enim. Morbi sapien leo, malesuada vel neque vel, lobortis porttitor magna.",
                        Category = "random",
                        Url = "https://dummyimage.com/200x200/000/fff",
                        For = 37,
                    Against = 13,
                    }


                };
        context.Posts.AddRange(posts);
        context.SaveChanges();
      }
    }
  }
}