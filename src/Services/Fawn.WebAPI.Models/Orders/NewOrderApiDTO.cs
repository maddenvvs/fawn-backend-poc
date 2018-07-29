using System;
using System.Collections.Generic;

namespace Fawn.WebAPI.Models.Orders
{
    public class NewOrderApiDTO
    {
        public int? CustomerId { get; set; }

        public bool IsPaid { get; set; }

        public List<GoodsToOrderApiDTO> Goods { get; set; }
    }
}