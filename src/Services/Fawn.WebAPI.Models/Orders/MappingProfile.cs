using AutoMapper;
using Fawn.DAL.Models;

namespace Fawn.WebAPI.Models.Orders
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderDTO, OrderApiDTO>()
                .ForMember(d => d.Goods, o => o.MapFrom(s => s.OrderGoods));

            CreateMap<OrderGoodsDTO, OrderItemApiDTO>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.GoodsItem.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.GoodsItem.Name))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.GoodsItem.Description));

            CreateMap<NewOrderApiDTO, OrderDTO>()
                .ForMember(d => d.OrderGoods, o => o.MapFrom(s => s.Goods));

            CreateMap<GoodsToOrderApiDTO, OrderGoodsDTO>()
                .ForMember(d => d.GoodsId, o => o.MapFrom(s => s.Id));

        }
    }
}