using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunridge.Models
{
    public class Board
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "[None listed]")]
        public string Phone { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "[None listed]")]
        public string Email { get; set; }

        
        [Display(Name = "Image")]
        public string Image { get; set; }

        public string Name { get; set; }

        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }

        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }

    }
}
