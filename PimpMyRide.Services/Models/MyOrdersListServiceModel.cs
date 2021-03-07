namespace PimpMyRide.Services.Models
{
    using System.Collections.Generic;

    public class MyOrdersListServiceModel
    {
        public int Id { get; set; }

        public string Seller { get; set; }

        public Dictionary<int, string> Parts { get; set; }

        public decimal? TotalPrice { get; set; }

        public bool IsCompleted { get; set; }
    }
}
