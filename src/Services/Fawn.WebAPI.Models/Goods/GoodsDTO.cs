using System;

namespace Fawn.WebAPI.Models.Goods
{
    public class GoodsApiDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal? ItemPrice { get; set; }
    }
}
