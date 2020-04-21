using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        [Display(Name = "Archive")]

        public bool IsArchive { get; set; }

        [Display(Name = "Last Modified By")]

        public string LastModifiedBy { get; set; }

        [Display(Name = "Last Modified Date")]

        public DateTime LastModifiedDate { get; set; }

    
        //public virtual ICollection<LotInventory> LotInventories { get; set; }
    }
}
