using System.ComponentModel.DataAnnotations;

namespace ConverseStore.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "City")]
        [Required(ErrorMessage = "City Name is required")]
        public string City { get; set; }
        public string? Street { get; set; }
        public string? AddressDetails { get; set; }
        public string? PostalCode { get; set; }


        [Display(Name = "UserName")]
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Address is required")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm The Password!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords Are Different")]
        public string ConfirmPassword { get; set; }
    }
}
