using AutoMapper;
using Dashboard_WEB_API.BLL.Dtos.Auth;
using Dashboard_WEB_API.DAL.Entities.Identity;

namespace PD421_Dashboard_WEB_API.BLL.MapperProfiles
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<RegisterDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

        }
    }
}