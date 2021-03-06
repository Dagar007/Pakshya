using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Profiles;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Followers
{
    public class List
    {
        public class Query : IRequest<List<Profile>>
        {
            public string Email { get; set; }
            public string Predicate { get; set; }
        }
        public class Handler : IRequestHandler<Query, List<Profile>>
        {
            private readonly DataContext _context;
            private readonly IProfileReader _profileReader;

            public Handler(DataContext context, IProfileReader profileReader)
            {
                _profileReader = profileReader;
                _context = context;
            }

            public async Task<List<Profile>> Handle(Query request, CancellationToken cancellationToken)
            {
                var queryable = _context.Followings.AsQueryable();

                List<UserFollowing> userFollowing;
                var profiles = new List<Profile>();

                switch (request.Predicate)
                {
                    case "followers":
                    {
                        userFollowing = await queryable
                            .Where(x => x.Target.Email == request.Email)
                            .ToListAsync(cancellationToken: cancellationToken);
                        // to get the followers where this user is a target.
                        foreach (var follower in userFollowing)
                        {
                            profiles.Add(await _profileReader.ReadProfile(follower.Observer.Email));
                        }

                        break;
                    }
                    case "following":
                    {
                        userFollowing = await queryable
                            .Where(x => x.Observer.Email == request.Email)
                            .ToListAsync(cancellationToken: cancellationToken);
                        // to get the observer, where current user is an observer.

                        foreach (var follower in userFollowing)
                        {
                            profiles.Add(await _profileReader.ReadProfile(follower.Target.Email));
                        }

                        break;
                    }
                }

                return profiles;
            }
        }
    }
}