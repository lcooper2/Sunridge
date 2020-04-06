using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
    public class OwnerLot
    {
        [Key]
        public int Id { get; set; }
        
        public bool IsPrimary { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Nav properties
        [Display(Name = "Application User")]
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Lot")]
        public int LotId { get; set; }

        [ForeignKey("LotId")]
        public Lot Lot { get; set; }
    }
}
