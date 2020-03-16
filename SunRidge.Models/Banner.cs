using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sunridge.Models
{
   public class Banner
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Header")]
        public string Header { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public Boolean Active { get; set; } // to make it so they can activate or swap banner items
    }
}
