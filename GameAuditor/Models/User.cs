using GameAuditor.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace GameAuditor.Models
{
    public class User : IdentityUser
    {
        public string RefreshToken { get; set; }

        public DateTime TokenCreated { get; set; }

        public DateTime TokenExpires { get; set; }
    }
}
