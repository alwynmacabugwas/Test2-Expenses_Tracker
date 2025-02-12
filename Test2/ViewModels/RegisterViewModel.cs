using System.ComponentModel.DataAnnotations;
using Test2.Utilities;

namespace Test2.ViewModels
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [ValidEmail(allowedDomain: "uap.asia", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "Password and confirmation password do not match")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        public string ConfirmPassword { get; set; }
    }
}
