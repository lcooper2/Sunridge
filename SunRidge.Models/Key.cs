using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sunridge.Models
{
    public class Key
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 1)]
        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }

        [Required]
        [Range(2000, 2030)]
        public int Year { get; set; }

        public string FullSerial
        {
            get
            {
                return $"{Year}-{SerialNumber}";
            }
        }
    }
}
