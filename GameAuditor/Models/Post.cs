using GameAuditor.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameAuditor.Models
{
    public class Post : EntityBase
    {
        [Required]
        [NotNull]
        [MaxLength(250)]
        public string Title { get; set; }

        [Required]
        [NotNull]
        [MaxLength(12000)]
        public string Content { get; set; }

        [Required]
        [NotNull]
        public string Tags { get; set; }

        [NotNull]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [NotNull]
        public DateTime UpdatedDate { get; set;} = DateTime.Now;
    }
}
