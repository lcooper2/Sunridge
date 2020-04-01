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

        public DateTime DateofIncident { get; set; }

        public DateTime TimeofIncident { get; set; }

        /*To port info of person who is filling */
        [Display(Name = "Application User")]
        public string ApplicationUserId { get; set; }

        [NotMapped]
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        /*To fill in address of claimant and/ or address of attorney*/
        [Display(Name = "Address")]
        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }

        /*To pull their lot info*/
        [Display(Name = "Lot")]
        public int LotId { get; set; }
        [ForeignKey("LotId")]
        public virtual Lot Lot { get; set; }


    }
}
