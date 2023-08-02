using AutoMapper;
using TMS.Models;
using TMS.Models.Dto;

namespace TMS.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.TicketCategory, opt => opt.MapFrom(src => src.TicketCategory.EventId))
                .ForMember(dest => dest.TicketCategory, opt => opt.MapFrom(src => src.TicketCategory.TicketCategoryId))
                .ReverseMap();
            CreateMap<Order, OrderPatchDto>().ReverseMap();
        }
    }
}