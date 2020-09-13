using System.Linq;
using AutoMapper;
using Domain;

namespace Application.Posts
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostDto>();
            CreateMap<UserPostLike, LikeDto>()
                .ForMember(d => d.Username, o => o.MapFrom(s => s.AppUser.UserName))
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.AppUser.DisplayName))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.AppUser.Photos.FirstOrDefault(x => x.IsMain).Url));

            // .ForMember(d => d.Following, o =>o.MapFrom<FollowingResolver>());

            CreateMap<Post, PostConcise>().
                ForMember(d => d.NoOfComments, o => o.MapFrom(s => s.Comments.Count()))
                .ForMember(d => d.NoOfLikes, o => o.MapFrom(s => s.UserPostLikes.Count))
                .ForMember(d => d.HostUsername, o => o.MapFrom(s => s.UserPostLikes.SingleOrDefault(s => s.IsAuthor == true).AppUser.UserName))
                .ForMember(d => d.HostDisplayName, o => o.MapFrom(s => s.UserPostLikes.SingleOrDefault(s => s.IsAuthor == true).AppUser.DisplayName))
                .ForMember(d => d.HostImage, o => o.MapFrom(s => s.UserPostLikes.SingleOrDefault(s => s.IsAuthor == true).AppUser.Photos.SingleOrDefault(p=> p.IsMain).Url))
                .ForMember(d => d.IsAuthor, o => o.MapFrom<PostConsiceAuthorResolver>())
                .ForMember(d => d.IsPostLiked, o => o.MapFrom<PostConsiceIsPostLikedResolver>());

            CreateMap<Category, CategoryDto>();



        }
    }
}