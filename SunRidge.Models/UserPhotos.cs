using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
    public class UserPhotos
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public bool IsMainImage { get; set; }
        public string ImageURL { get; set; }

    }
}
