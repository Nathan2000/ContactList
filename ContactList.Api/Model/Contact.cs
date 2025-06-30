using ContactList.Shared.Contacts;
using System.ComponentModel.DataAnnotations;

namespace ContactList.Api.Model
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$",
        ErrorMessage = "Password must contain upper and lower case letters and a number.")]
        public string Password { get; set; } = string.Empty;
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }

        public Category Category { get; set; }
        public int? SubcategoryId { get; set; }
        public Subcategory? Subcategory { get; set; }
    }
}
