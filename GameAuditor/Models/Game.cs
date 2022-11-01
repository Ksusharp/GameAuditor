using GameAuditor.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameAuditor.Models
{
    public class Game : EntityBase
    {
        [Required]        
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        public IList<Platform> Platform { get; set; }

        [Required]
        public IList<Genre> Genre { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Range(typeof(DateTime), "01/01/1950", "01/01/2150", ErrorMessage = "Введено некорректное значение даты")]
        public DateTime? ReleaseDate { get; set; }
    }
}
