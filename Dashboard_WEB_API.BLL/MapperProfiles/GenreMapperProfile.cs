using AutoMapper;
using Dashboard_WEB_API.BLL.Dtos.Genre;
using Dashboard_WEB_API.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard_WEB_API.BLL.MapperProfiles
{
    internal class GenreMapperProfile : Profile
    {
        public GenreMapperProfile()
        {
            // UpdateGanreDto to GenreEntity
            CreateMap<UpdateGenreDto, GenreEntity>()
                .ForMember(dest => dest.NormalizedName, opt => opt.MapFrom(src => src.Name.ToUpper()));
        }
    }
}
