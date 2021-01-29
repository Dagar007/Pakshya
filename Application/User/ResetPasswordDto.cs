using System.ComponentModel.DataAnnotations;

namespace Application.User
{
    public class ResetPasswordDto
    {
        [Required]
        public string User { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=[^A-Z]*[A-Z])(?=[^a-z]*[a-z])(?=\\D*\\d).{8,}$", ErrorMessage = "Password should contain at least 8 characters including 1 special character, digit and a capital letter")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password must match")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Token { get; set; }
    }
}