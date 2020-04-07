using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
    public class FormSubmissions
    {
        [Key]
        public int Id { get; set; }

        public DateTime SubmitDate { get; set;}

        public string FormType { get; set; }

        /*To port info of person who is filling */
        [Display(Name = "FormId")]
        public string FormId { get; set; }


        [NotMapped]
        [ForeignKey("ClaimLossId")]
        public virtual ClaimLoss ClaimLoss { get; set; }
    }
}
