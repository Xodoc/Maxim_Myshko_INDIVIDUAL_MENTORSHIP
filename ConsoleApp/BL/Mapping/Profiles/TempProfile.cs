using AutoMapper;
using BL.DTOs;
using DAL.Entities.WeatherHistoryEntities;

namespace BL.Mapping.Profiles
{
    public class TempProfile : Profile
    {
        public TempProfile()
        {
            CreateMap<WeatherHistory, TempDTO>()
                .ForMember(dto => dto.Temp, src => src.MapFrom(entity => entity.Temp))
                .ForMember(dto => dto.City, src => src.MapFrom(entity => entity.City))
                .ReverseMap();
        }
    }
}
