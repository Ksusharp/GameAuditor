using GameAuditor.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace GameAuditor.Models
{
    public class User : IdentityUser
    {
        //public string Username { get; set; } = string.Empty;
        //public string UserEmail { get; set; }
        //public byte[] PasswordHash { get; set; }
        //public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}
