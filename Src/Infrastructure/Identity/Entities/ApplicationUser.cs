using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BoardSlide.API.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<RefreshTokenInfo> RefreshTokens { get; set; }
        
        public ApplicationUser()
            :base()
        {
            RefreshTokens = new HashSet<RefreshTokenInfo>();
        }
    }
}