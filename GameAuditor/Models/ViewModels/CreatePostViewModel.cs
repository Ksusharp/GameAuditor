using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameAuditor.Models.ViewModels
{
    public class CreatePostViewModel
    {
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [Required]
        [MaxLength(12000)]
        public string Content { get; set; }

        [Required]
        [NotNull]
        public virtual IEnumerable<PostTag> Tags { get; set; }

        [NotNull]
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        [NotNull]
        public DateTime? UpdatedDate { get; set; } = DateTime.Now;
    } 
}
