using AutoMapper;
using Domain;

namespace Application.Interests
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category,UserInterestDto>();
            CreateMap<UserInterest, UserInterestDto>()
            .ForMember(s => s.Id, o=> o.MapFrom(x => x.CategoryId))
            .ForMember(s => s.Value, o => o.MapFrom(x => x.Category.Value));
        }
    }
}