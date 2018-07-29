using AutoMapper;
using Fawn.DAL.Models;

namespace Fawn.WebAPI.Models.Goods
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<GoodsDTO, GoodsApiDTO>()
                .ForMember(d => d.ItemPrice, o => o.MapFrom(s => s.Price.ItemPrice));

            CreateMap<GoodsModel, GoodsDTO>();
        }
    }
}