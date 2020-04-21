using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
    public class SuggestionComplaint
    {
        [Key]
        public int Id { get; set; }

        public string Suggestion { get; set; }

        public string Complaint { get; set; }

        [Display(Name = "Application User")]
        public string ApplicationUserId { get; set; }

        [NotMapped]
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
