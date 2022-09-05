using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameAuditor.Models.ViewModels
{
    public class UpdateGameViewModel
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [NotNull]
        public string Platform { get; set; }

        [Required]
        [NotNull]
        public string Genre { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Range(typeof(DateTime), "01/01/1950", "01/01/2150", ErrorMessage = "Введено некорректное значение даты")]
        public DateTime? ReleaseDate { get; set; }
    }
}
