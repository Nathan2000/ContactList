using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ContactList.Shared.Auth
{
    public class LoginDto
    {
        [Required(ErrorMessage = "The email address is required.")]
        [EmailAddress]
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class TokenDto
    {
        [JsonPropertyName("token")]

        public string? Token { get; set; }
        [JsonPropertyName("expiration")]

        public DateTime Expiration { get; set; }
    }
}
