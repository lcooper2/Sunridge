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
        [StringLength(3, MinimumLength = 1)]
        public string FormType { get; set; }

        [Display(Name = "Listing Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime SubmitDate { get; set; }

        public String PrivacyLevel { get; set; }
        public bool Resolved { get; set; }
        [Display(Name = "Resolve Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? ResolveDate { get; set; }

        [Display(Name = "Resolved by")]
        public string ResolveUser { get; set; }

        [Display(Name = "FormSubmissions")]
        public int FormSubmissionsId { get; set; }
        [ForeignKey("FormSubmissionsId")]
        public virtual FormSubmissions FormSubmissions { get; set; }

        // Calculated properties
        [Display(Name = "Form Type")]
        public string FormTypeName
        {
            get
            {
                switch (FormType)
                {
                    case "SC":
                        return "Suggestion / Complaint";
                    case "WIK":
                        return "Work in kind";
                    case "CL":
                        return "Loss claim";
                    case "BR":
                        return "Building request";
                    default:
                        return FormType;
                }
            }
        }
        public string FormAction
        {
            get
            {
                switch (FormType)
                {
                    case "SC":
                        return "SuggestionComplaint";
                    case "WIK":
                        return "InKindWork";
                    case "CL":
                        return "Loss claim";
                    case "BR":
                        return "Building request";
                    default:
                        return FormType;
                }
            }
        }
    }
}
