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

        /*To port info of person who is filling */
        [Display(Name = "FormId")]
        public int FormId { get; set; }
       
        public bool IsWik { get; set; }

        public bool IsCl { get; set; }

        public bool IsSC { get; set; }
    }
}
