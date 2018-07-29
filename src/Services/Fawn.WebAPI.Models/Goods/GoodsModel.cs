namespace Fawn.WebAPI.Models.Goods
{
    using System.ComponentModel.DataAnnotations;

    public class GoodsModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}