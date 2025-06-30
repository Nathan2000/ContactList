using System.ComponentModel.DataAnnotations;

namespace ContactList.Shared.Auth
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "The email address is required.")]
        [EmailAddress(ErrorMessage = "The email address is not valid.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "The password is required.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{6,}$",
    ErrorMessage = "Password must contain uppercase, lowercase, number, and special character")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm your password.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
