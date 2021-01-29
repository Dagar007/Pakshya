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


            CreateMap<Post, PostConcise>().
                ForMember(d => d.NoOfComments, o => o.MapFrom(s => s.Comments.Count()))
                .ForMember(d => d.NoOfLikes, o => o.MapFrom(s => s.UserPostLikes.Count(x => x.IsLiked)))
                .ForMember(d => d.HostId, o => o.MapFrom(s => 
                    s.UserPostLikes.SingleOrDefault(userPostLike => userPostLike.IsAuthor).AppUser.Id))
                .ForMember(d => d.HostDisplayName, o => o.MapFrom(s => 
                    s.UserPostLikes.SingleOrDefault(userPostLike => userPostLike.IsAuthor).AppUser.DisplayName))
                .ForMember(d => d.HostImage, o => o.MapFrom(s => 
                    s.UserPostLikes.SingleOrDefault(userPostLike => userPostLike.IsAuthor).AppUser.Photos.SingleOrDefault(p=> p.IsMain).Url))
               .ForMember(d => d.IsAuthor, o => o.MapFrom<PostConciseAuthorResolver>())
               .ForMember(d => d.IsPostLiked, o => o.MapFrom<PostConciseIsPostLikedResolver>());

            CreateMap<Category, PostCategoryDto>();



        }
    }
}