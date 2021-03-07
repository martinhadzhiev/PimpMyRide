namespace PimpMyRide.Web.Models.AccountViewModels
{
    using static Data.DataConstants;
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [MinLength(5,ErrorMessage = "Username should be at least {1} symbols.")]
        [MaxLength(20,ErrorMessage = "Username should be no longer than {1} symbols.")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MinLength(NameMinLength, ErrorMessage = "First name must be longer than {1} symbols.")]
        [MaxLength(NameMaxLength, ErrorMessage = "First name must be no longer than {1} symbols.")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(NameMinLength, ErrorMessage = "Last name must be longer than {1} symbols.")]
        [MaxLength(NameMaxLength, ErrorMessage = "Last name must be no longer than {1} symbols.")]
        public string LastName { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }
    }
}
