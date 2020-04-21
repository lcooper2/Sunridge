using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sunridge.Models
{
    public class NewsItem
    {
        public int NewsItemId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Header { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int Year { get; set; }

        [Display(Name = "File Name")]
        public string FileName { get; set; }
        [Display(Name = "File Path")]
        public string FilePath { get; set; }
    }
}
