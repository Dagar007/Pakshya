using System.Linq;
using AutoMapper;
using Domain;

namespace Application.Comments
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comment, CommentDto>()
                .ForMember(d => d.AuthorEmail, o => o.MapFrom(s => s.Author.UserName))
                .ForMember(d => d.AuthorDisplayName, o => o.MapFrom(s => s.Author.DisplayName))
                .ForMember(d => d.AuthorImage, o => o.MapFrom(s => s.Author.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(d => d.TotalLikes, o=> o.MapFrom(s => s.UserCommentLikes.Count(x => x.IsLiked)))
                .ForMember(d => d.HasLoggedInUserLiked, o =>o.MapFrom<CommentLikeResolver>());
                
            CreateMap<UserCommentLike,LikeDto>()
                .ForMember(d => d.Username, o=> o.MapFrom(s => s.AppUser.UserName))
                .ForMember(d => d.DisplayName, o=> o.MapFrom(s => s.AppUser.DisplayName));
                
        }
    }
}