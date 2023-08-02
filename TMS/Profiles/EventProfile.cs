using AutoMapper;
using TMS.Models;
using TMS.Models.Dto;

namespace TMS.Api.Profiles;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<Event, EventDto>()
            .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.Venue.Location))
            .ForMember(dest => dest.EventType, opt => opt.MapFrom(src => src.EventType.EventTypeName))
            .ReverseMap();
        CreateMap<Event, EventPatchDto>().ReverseMap();
    }
}