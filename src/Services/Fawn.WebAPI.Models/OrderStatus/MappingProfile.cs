using AutoMapper;

namespace Fawn.WebAPI.Models.OrderStatus
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Fawn.DAL.Models.OrderStatus, OrderStatusApiDTO>()
                .ForMember(d => d.Code, o => o.MapFrom(s => (int)s))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));
        }
    }
}