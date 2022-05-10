#nullable enable

using System;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie.Models
{
    public class Movie
    {
        public Guid Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        [Display(Name = "タイトル")]
        public string? Title { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "リリース日")]
        public DateTime ReleaseDate { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Required]
        [Display(Name = "ジャンル")]
        public string Genre { get; set; } = string.Empty;

        [Range(1, 10000)]
        [Display(Name = "価格")]
        [DisplayFormat(DataFormatString = "{0:#,0}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        [Display(Name = "登録日時")]
        public DateTime CreateDate { get; set; }
    }
}