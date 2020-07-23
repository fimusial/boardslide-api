namespace BoardSlide.API.Infrastructure.Settings
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public int ExpirationDurationInSeconds { get; set; }
        public int RefreshTokenExpirationDurationInDays { get; set; }
    }
}