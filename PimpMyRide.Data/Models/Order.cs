namespace PimpMyRide.Data.Models
{
    using System.Collections.Generic;

    public class Order
    {
        public int Id { get; set; }

        public string SellerId { get; set; }

        public User Seller { get; set; }

        public string BuyerId { get; set; }

        public User Buyer { get; set; }

        public ICollection<Part> Parts { get; set; } = new List<Part>();

        public decimal? Price { get; set; }

        public bool IsCompleted { get; set; }
    }
}
