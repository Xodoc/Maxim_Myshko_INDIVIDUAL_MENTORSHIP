using AutoMapper;
using BL.DTOs;
using DAL.Entities.WeatherHistoryEntities;

namespace BL.Mapping.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityDTO>()
                .ForMember(dto => dto.Id, src => src.MapFrom(entity => entity.Id))
                .ForMember(dto => dto.CityName, src => src.MapFrom(entity => entity.CityName))
                .ReverseMap();
        }
    }
}
