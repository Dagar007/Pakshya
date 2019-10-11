using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Posts
{
  public class Create
  {
    public class Command : IRequest
    {
      public Guid Id { get; set; }
      public string Heading { get; set; }
      public string Description { get; set; }
      public string Category { get; set; }
      public DateTime Date { get; set; }
      public string Url { get; set; }
      public int For { get; set; }
      public int Against { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
      private readonly DataContext _context;
      public Handler(DataContext context)
      {
        _context = context;
      }

      public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
      {
        var post = new Post
        {
          Id = request.Id,
          Heading = request.Heading,
          Description = request.Description,
          Category = request.Category,
          Date = request.Date,
          Url = request.Url,
          For = 0,
          Against = 0
        };
        _context.Posts.Add(post);
        var success = await _context.SaveChangesAsync() > 0;
        if (success) return Unit.Value;

        throw new Exception("problem saving new post.");
      }
    }
  }
}