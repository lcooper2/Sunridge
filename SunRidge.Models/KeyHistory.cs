using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
    public class KeyHistory
    {
        [Key]
        public int Id { get; set; }
       
        public string Status { get; set; }
        [Display(Name = "Date Issued")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateIssued { get; set; }
        [Display(Name = "Date Returned")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DateReturned { get; set; }
        [Display(Name = "Paid Amount")]
        public float PaidAmount { get; set; }

        
        [Display(Name = "Key")]
        public int KeyId { get; set; }
        [ForeignKey("KeyId")]
        //Nav properties
        public virtual Key Key { get; set; }

        [Display(Name = "Lot")]
        public int LotId { get; set; }
        [ForeignKey("LotId")]
        public virtual Lot Lot { get; set; }
    }
}
