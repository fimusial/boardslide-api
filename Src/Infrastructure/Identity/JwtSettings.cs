namespace BoardSlide.API.Infrastructure.Identity
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public int ExpirationDurationInSeconds { get; set; }
        public int RefreshTokenExpirationDurationInDays { get; set; }
    }
}