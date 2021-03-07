namespace PimpMyRide.Services.Models
{
    using System;

    public class CarListServiceModel
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Picture { get; set; }

        public DateTime Added { get; set; }
    }
}
