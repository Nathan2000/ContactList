namespace ContactList.Api
{
    public class JwtSettings
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string? Secret { get; set; }
        public int ExpirationMinutes { get; set; } = 0;
    }
}
