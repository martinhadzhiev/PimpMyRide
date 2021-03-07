namespace PimpMyRide.Data.Models
{
    using System;
    using System.Collections.Generic;
    using Enums;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Car
    {
        public int Id { get; set; }

        [Required]
        [MinLength(MakeMinLength)]
        [MaxLength(MakeMaxLength)]
        public string Make { get; set; }

        [Required]
        [MinLength(ModelMinLength)]
        [MaxLength(ModelMaxLength)]
        public string Model { get; set; }

        [Required]
        [Range(MinYear, MaxYear)]
        public int YearOfProduction { get; set; }

        public EngineType EngineType { get; set; }

        public BodyType BodyType { get; set; }

        [Range(MinPrice, MaxPrice)]
        public decimal? TotalPrice { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public int Views { get; set; }

        public DateTime Added { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public string OwnerId { get; set; }

        public User Owner { get; set; }

        [MaxLength(PictureMaxLength)]
        public string Picture { get; set; }

        public ICollection<Part> Parts { get; set; } = new List<Part>();
    }
}
