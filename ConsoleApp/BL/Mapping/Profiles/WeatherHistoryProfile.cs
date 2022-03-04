using AutoMapper;
using BL.DTOs;
using DAL.Entities.WeatherHistoryEntities;

namespace BL.Mapping.Profiles
{
    public class WeatherHistoryProfile : Profile
    {
        public WeatherHistoryProfile()
        {
            CreateMap<WeatherHistory, WeatherHistoryDTO>()
                .ForMember(dto => dto.Temp, src => src.MapFrom(entity => entity.Temp))
                .ForMember(dto => dto.CityName, src => src.MapFrom(entity => entity.CityName))
                .ForMember(dto => dto.Time, src => src.MapFrom(entity => entity.Time))
                .ReverseMap();
        }
    }
}
