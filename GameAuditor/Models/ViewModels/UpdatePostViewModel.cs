using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameAuditor.Models.ViewModels
{
    public class UpdatePostViewModel
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
        public virtual IEnumerable<PostTag> Tags { get; set; }

        [NotNull]
        public DateTime CreatedDate { get; set; }

        [NotNull]
        public DateTime UpdatedDate { get; set; }
    }
}
