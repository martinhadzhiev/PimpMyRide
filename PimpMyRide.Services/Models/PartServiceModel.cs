namespace PimpMyRide.Services.Models
{
    using System.Collections.Generic;

    public class PartServiceModel
    {
        public string OwnerUsername { get; set; }

        public ICollection<PartListServiceModel> Parts { get; set; }
    }
}
