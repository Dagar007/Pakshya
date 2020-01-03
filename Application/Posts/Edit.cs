using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using FluentValidation;
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
            public Category Category { get; set; }
            public DateTime? Date { get; set; }
            public string Url { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Heading).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.Category).NotEmpty();
                RuleFor(x => x.Date).NotEmpty();

            }
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
                    throw new RestException(HttpStatusCode.NotFound, new { post = "Not found" });
                if (request.Category != null)
                {
                    var category = await _context.Categories.FindAsync(request.Category.Id);
                    if (category == null)
                        throw new RestException(HttpStatusCode.NotFound, new { Category = "Not found" });
                    post.Category = category;
                }

                post.Heading = request.Heading ?? post.Heading;
                post.Description = request.Description ?? post.Description;
                
                post.Date = DateTime.Now;
                post.Url = request.Url ?? post.Url;


                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception($"Problem saving {request.Id} post");
            }
        }
    }
}