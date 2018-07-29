using System;
using System.Collections.Generic;

namespace Fawn.WebAPI.Models.Orders
{
    public class OrderApiDTO
    {
        public int Id { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime PickupDateTime { get; set; }

        public string Status { get; set; }

        public int? CustomerId { get; set; }

        public bool IsPaid { get; set; }

        public List<OrderItemApiDTO> Goods { get; set; }
    }
}