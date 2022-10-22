using GameAuditor.Models.Interfaces;
using Microsoft.Build.Framework;

namespace GameAuditor.Models
{
    public class PostTag : EntityBase
    {
        [Required]
        public string Tag { get; set; }
    }
}
