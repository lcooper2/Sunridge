using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
    public class FormResponse
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FormType { get; set; }

        public string? FormDisplay { get; set; }


        //public String PrivacyLevel { get; set; }
        public bool Resolved { get; set; }
        [Display(Name = "Resolve Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ResolveDate { get; set; }

        [Display(Name = "Resolved by")]
        public string ResolveUser { get; set; }

        [Display(Name = "FormSubmissions")]
        public int FormSubmissionsId { get; set; }
        [ForeignKey("FormSubmissionsId")]
        public virtual FormSubmissions FormSubmissions { get; set; }

        
    }
}
