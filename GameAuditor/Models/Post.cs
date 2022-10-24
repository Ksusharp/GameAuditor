using GameAuditor.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameAuditor.Models
{
    public class Post : EntityBase
    {
        [Required]
        public string OwnerId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [Required]
        [MaxLength(12000)]
        public string Content { get; set; }

        [Required]
        public IList<TagNavigation> TagNavigation { get; set; }

        [NotNull]
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        [NotNull]
        public DateTime? UpdatedDate { get; set;} = DateTime.Now;
    }
}
