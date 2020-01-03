using System.Linq;
using AutoMapper;
using Domain;

namespace Application.Comments
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
             //CreateMap<Comment,CommentDto>();
            CreateMap<Comment, CommentDto>()
                .ForMember(d => d.Username, o => o.MapFrom(s => s.Author.UserName))
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.Author.DisplayName))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.Author.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(d => d.Liked, o=> o.MapFrom(s => s.UserComments.Count))
                .ForMember(d => d.IsLikedByUser, o =>o.MapFrom<CommentLikeResolver>());
                
            CreateMap<UserComment,LikeDto>()
                .ForMember(d => d.Username, o=> o.MapFrom(s => s.AppUser.UserName))
                .ForMember(d => d.DisplayName, o=> o.MapFrom(s => s.AppUser.DisplayName));
                
        }
    }
}