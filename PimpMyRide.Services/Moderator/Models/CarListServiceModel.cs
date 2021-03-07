namespace PimpMyRide.Services.Moderator.Models
{
    using System;
    using System.Collections.Generic;

    public class ModeratorCarListModel
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Picture { get; set; }

        public DateTime Added { get; set; }

        public ICollection<ModeratorPartListModel> Parts { get; set; }
    }
}
