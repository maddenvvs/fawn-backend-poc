namespace Fawn.DAL.Models
{
    public class GoodsDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public GoodsPriceDTO Price { get; set; }

        public GoodsImageDTO Image { get; set; }
    }
}