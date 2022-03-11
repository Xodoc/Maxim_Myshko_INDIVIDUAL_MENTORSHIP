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
                .ForMember(dto => dto.CityId, src => src.MapFrom(entity => entity.CityId))
                .ForMember(dto => dto.Timestamp, src => src.MapFrom(entity => entity.Timestapm))
                .ReverseMap();
        }
    }
}
