using AutoMapper;
using BL.DTOs;
using DAL.Entities;

namespace BL.Mapping.Profiles
{
    public class SubscriptionProfile : Profile
    {
        public SubscriptionProfile()
        {
            CreateMap<Subscription, SubscriptionDTO>()
                .ForMember(dto => dto.Id, src => src.MapFrom(entity => entity.Id))
                .ForMember(dto => dto.IsActive, src => src.MapFrom(entity => entity.IsActive))
                .ForMember(dto => dto.UserId, src => src.MapFrom(entity => entity.UserId))
                .ForMember(dto => dto.Cron, src => src.MapFrom(entity => entity.Cron))
                .ForMember(dto => dto.FromDate, src => src.MapFrom(entity => entity.FromDate))
                .ReverseMap();
        }
    }
}
