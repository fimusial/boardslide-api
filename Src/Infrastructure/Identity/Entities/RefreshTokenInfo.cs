using System;

namespace BoardSlide.API.Infrastructure.Identity.Entities
{
    public class RefreshTokenInfo
    {
        public string Token { get; set; }
        public string Jti { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime ExpirationDateTime { get; set; }
        public bool WasUsed { get; set; }
        public bool WasInvalidated { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}