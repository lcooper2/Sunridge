using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
    public class ClaimLoss
    {
        [Key]
        public int Id { get; set; }

        //is attorney is filling
        public bool isAttorney { get; set; }

        //public string? AttorneyInfo { get; set; }
        public string Type { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateofIncident { get; set; }

        /*To port info of person who is filling */
        [Display(Name = "Application User")]
        public string ApplicationUserId { get; set; }

        [NotMapped]
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        /*To fill in address of claimant and/ or address of attorney*/
        public string ClaimAddress { get; set; }

        /*To pull their lot info*/
        
    }
}
