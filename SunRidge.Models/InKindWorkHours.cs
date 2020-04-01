using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
    public class InKindWorkHours
    {
        [Key]
        public int Id { get; set; }
        public string Activity { get; set; }

        public string Equipment { get; set; }
        public double? Hours { get; set; }
        public string Type { get; set; }

        [Display(Name = "Application User")]
        public string ApplicationUserId { get; set; }
        
        [NotMapped]
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
