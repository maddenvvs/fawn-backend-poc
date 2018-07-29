namespace Fawn.DAL.Models
{
    public class GoodsImageDTO
    {
        public int Id { get; set; }

        public int GoodsId { get; set; }

        public GoodsDTO GoodsItem { get; set; }

        public byte[] Image { get; set; }
    }
}