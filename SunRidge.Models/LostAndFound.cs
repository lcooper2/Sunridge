using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
    public class LostAndFound
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        public string Discription { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}",
            ConvertEmptyStringToNull = true, NullDisplayText = "[None listed]")]
        public DateTime? ListedDate { get; set; }

        public string ListerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public bool Claimed { get; set; }
        public string ClaimedBy { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}",
            ConvertEmptyStringToNull = true, NullDisplayText = "[None listed]")]
        public DateTime? ClaimedDate { get; set; }

        [NotMapped]
        public List<LostAndFoundImage> Images { get; set; }

    }
}
