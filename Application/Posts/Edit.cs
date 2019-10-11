using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.Posts
{
  public class Edit
  {
    public class Command : IRequest
    {
      public Guid Id { get; set; }
      public string Heading { get; set; }
      public string Description { get; set; }
      public string Category { get; set; }
      public DateTime? Date { get; set; }
      public string Url { get; set; }
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
        var post = await _context.Posts.FindAsync(request.Id);
        if (post == null)
          throw new Exception("could not find the post");
        post.Heading = request.Heading ?? post.Heading;
        post.Description = request.Description ?? post.Description;
        post.Category = request.Category ?? post.Category;
        post.Date = request.Date ?? post.Date;
        post.Url = request.Url ?? post.Url;


        var success = await _context.SaveChangesAsync() > 0;
        if (success) return Unit.Value;

        throw new Exception($"Problem saving {request.Id} post");
      }
    }
  }
}