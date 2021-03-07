namespace PimpMyRide.Services.Models
{
    using System;
    using System.Collections.Generic;
    using Data.Enums;
    using Data.Models;

    public class CarDetailsServiceModel
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int YearOfProduction { get; set; }

        public EngineType EngineType { get; set; }

        public BodyType BodyType { get; set; }

        public decimal? TotalPrice { get; set; }
        
        public string Description { get; set; }

        public int Views { get; set; }

        public DateTime Added { get; set; }

        public string OwnerUsername { get; set; }

        public string Owner { get; set; }

        public string PhoneNumber { get; set; }
        
        public string Picture { get; set; }

        public ICollection<Part> Parts { get; set; }
    }
}
