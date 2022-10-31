using GameAuditor.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace GameAuditor.Models
{
    public class User : IdentityUser
    {
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}
