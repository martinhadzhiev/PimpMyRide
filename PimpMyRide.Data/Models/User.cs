namespace PimpMyRide.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;
    using static DataConstants;
    
    public class User : IdentityUser
    {
        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string LastName { get; set; }

        public int? Rating { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();

        public ICollection<Order> MyOrders { get; set; } = new List<Order>();

        public ICollection<Order> MySales { get; set; } = new List<Order>();
    }
}
