namespace PimpMyRide.Services.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class PartCrudServiceModel : IValidatableObject
    {
        [Required]
        [MinLength(PartMinLength)]
        [MaxLength(PartMaxLength)]
        public string Name { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public decimal? Price { get; set; }
        
        public IFormFile Picture { get; set; }

        public int CarId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Picture?.Length > PictureMaxLength)
            {
                yield return new ValidationResult("Picture must be up to 2MB.");
            }
        }
    }
}
