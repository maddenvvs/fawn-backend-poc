namespace Fawn.DAL.Models
{
    using System;
    using System.Collections.Generic;

    public class OrderDTO
    {
        public int Id { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime PickupDateTime { get; set; }

        public OrderStatus Status { get; set; }

        public int? CustomerId { get; set; }

        public bool IsPaid { get; set; }

        public virtual ICollection<OrderGoodsDTO> OrderGoods { get; set; }
    }
}
