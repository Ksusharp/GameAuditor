﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameAuditor.Models.ViewModels
{
    public class UpdatePostViewModel
    {
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [Required]
        [MaxLength(12000)]
        public string Content { get; set; }

        [Required]
        public IEnumerable<PostTag> Tags { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
