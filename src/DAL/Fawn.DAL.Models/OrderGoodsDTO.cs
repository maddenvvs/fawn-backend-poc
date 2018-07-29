namespace Fawn.DAL.Models
{
    public class OrderGoodsDTO
    {
        public int OrderId { get; set; }

        public OrderDTO Order { get; set; }

        public int GoodsId { get; set; }

        public GoodsDTO GoodsItem { get; set; }

        public int Amount { get; set; }

        public decimal ItemPrice { get; set; }
    }
}