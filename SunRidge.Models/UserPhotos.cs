﻿using System;
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
        [Required]

        public string Category { get; set; }

        [Required(ErrorMessage = "Please type a title")]

        public string Title { get; set; }

        public int Year { get; set; }

        public string Image { get; set; }

    }
}
