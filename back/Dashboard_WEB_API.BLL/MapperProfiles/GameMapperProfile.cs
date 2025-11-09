using AutoMapper;
using Dashboard_WEB_API.BLL.Dtos.Game;
using Dashboard_WEB_API.DAL.Entities;


namespace PD421_Dashboard_WEB_API.BLL.MapperProfiles
{
    public class GameMapperProfile : Profile
    {
        public GameMapperProfile()
        {
            // GameImageEntity -> GameImageDto
            //CreateMap<GameImageEntity, GameImageDto>();

            // CreateGameDto -> GameEntity
            CreateMap<CreateGameDto, GameEntity>()
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate.ToUniversalTime()))
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.Genres, opt => opt.Ignore());

            // GameEntity -> GameDto
            //CreateMap<GameEntity, GameDto>()
            //    .ForMember(dest => dest.MainImage, opt => opt.MapFrom(src => src.Images.Count > 0 ? src.Images.First(i => i.IsMain) : null))
            //    .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Where(i => !i.IsMain)));
        }
    }
}