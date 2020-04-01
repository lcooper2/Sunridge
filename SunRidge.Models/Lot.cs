using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Sunridge.Models
{
    public class Lot
    {
        [Key]
        public int Id { get; set; }
       

        [Display(Name = "Lot Number")]
        [Required]
        public string LotNumber { get; set; }

        [Display(Name = "Tax ID")]
        public string TaxId { get; set; }

        public bool IsArchive { get; set; }
        public string LastModifiedBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime LastModifiedDate { get; set; }

        //NavigationalProperties
        [Display(Name = "Address")]
        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }
        //public int OwnerId { get; set; }
        //public virtual Owner Owner { get; set; }
        //public virtual ICollection<OwnerLot> OwnerLots { get; set; }
        //public virtual ICollection<LotInventory> LotInventories { get; set; }
        public virtual ICollection<LotHistory> LotHistories { get; set; }
        //public virtual ICollection<Transaction> Transactions { get; set; }

        public int CompareTo(Lot lot)
        {
            var thisParts = LotNumber.Split('-');
            var otherParts = lot.LotNumber.Split('-');

            if (thisParts.Count() < 2 || otherParts.Count() < 2)
            {
                return LotNumber.CompareTo(lot.LotNumber);
            }

            var thisNumber = Int32.Parse(thisParts[1]);
            var otherNumber = Int32.Parse(otherParts[1]);

            return thisNumber.CompareTo(otherNumber);
        }
    }
}

