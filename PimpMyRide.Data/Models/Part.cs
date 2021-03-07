namespace PimpMyRide.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Part
    {
        public int Id { get; set; }

        [Required]
        [MinLength(PartMinLength)]
        [MaxLength(PartMaxLength)]
        public string Name { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public decimal? Price { get; set; }

        public bool IsAvailable { get; set; }

        [MaxLength(PictureMaxLength)]
        public string Picture { get; set; }

        public int CarId { get; set; }

        public Car Car { get; set; }
    }
}
