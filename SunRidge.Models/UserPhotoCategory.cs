using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sunridge.Models
{
   public class UserPhotoCategory
    {
        [Key]
         public int Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string Title { get; set; }
    }
}
