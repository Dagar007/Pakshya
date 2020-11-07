using System.Linq;
using Domain;

namespace Application.Profiles
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<UserPostLike, PostPostedByUserDto>()
              .ForMember(d => d.Id, o => o.MapFrom(s => s.PostId))
              .ForMember(d => d.Heading, o => o.MapFrom(s => s.Post.Heading))
              .ForMember(d => d.NoOfLikes, o => o.MapFrom(s => s.Post.UserPostLikes.Count()))
              .ForMember(d => d.NoOfComments, o => o.MapFrom(s => s.Post.Comments.Count()));

            CreateMap<UserCommentLike, CommentPostedByUserDto>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.CommentId))
            .ForMember(d => d.Body, o => o.MapFrom(s => (s.Comment.Body.Count() < 50) ? s.Comment.Body : s.Comment.Body.Substring(0, 50)))
            .ForMember(d => d.NoLikes, o => o.MapFrom(s => s.Comment.UserCommentLikes.Count));

            CreateMap<UserInterest,InterestDTO>()
            .ForMember(x => x.Id, o => o.MapFrom(s => s.CategoryId))
            .ForMember(x => x.Value, o => o.MapFrom(x => x.Category.Value));
        }
    }
}