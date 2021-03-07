namespace PimpMyRide.Web.Models.ManageViewModels
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(NameMinLength, ErrorMessage = "First name must be longer than {1} symbols.")]
        [MaxLength(NameMaxLength, ErrorMessage = "First name must be no longer than {1} symbols.")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(NameMinLength, ErrorMessage = "Last name must be longer than {1} symbols.")]
        [MaxLength(NameMaxLength, ErrorMessage = "Last name must be no longer than {1} symbols.")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}
