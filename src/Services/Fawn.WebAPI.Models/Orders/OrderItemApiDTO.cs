namespace Fawn.WebAPI.Models.Orders
{
    public class OrderItemApiDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Amount { get; set; }

        public decimal ItemPrice { get; set; }
    }
}