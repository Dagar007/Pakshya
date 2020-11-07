using AutoMapper;
using Domain;

namespace Application.Interests
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category,UserInterestDto>();
        }
    }
}