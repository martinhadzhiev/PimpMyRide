namespace PimpMyRide.Services.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Data.Enums;
    using Microsoft.AspNetCore.Http;

    using static Data.DataConstants;

    public class CarServiceModel : IValidatableObject
    {
        [Required]
        [MinLength(MakeMinLength, ErrorMessage = "Make must be at least {1} symbols.")]
        [MaxLength(MakeMaxLength, ErrorMessage = "Make must be no longer than {1} symbols.")]
        public string Make { get; set; }

        [Required]
        [MinLength(ModelMinLength, ErrorMessage = "Model must be at least {1} symbol.")]
        [MaxLength(ModelMaxLength, ErrorMessage = "Model must be no longer than {1} symbols.")]
        [DisplayName("Model")]
        public string CarModel { get; set; }

        [Required]
        [Range(MinYear, MaxYear, ErrorMessage = "Year of production must be between 1970 and 2017.")]
        [DisplayName("Year of Production")]
        public int YearOfProduction { get; set; }

        [DisplayName("Engine type")]
        public EngineType EngineType { get; set; }

        [DisplayName("Body type")]
        public BodyType BodyType { get; set; }

        [Range(MinPrice, MaxPrice, ErrorMessage = "Invalid price.")]
        [DisplayName("Total price")]
        public decimal? TotalPrice { get; set; }

        [MaxLength(DescriptionMaxLength, ErrorMessage = "Description must be no longer than {1} symbols.")]
        public string Description { get; set; }

        public IFormFile Picture { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Picture?.Length > PictureMaxLength)
            {
                yield return new ValidationResult("Picture must be up to 2MB.");
            }
        }
    }
}
