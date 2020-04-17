using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
   public class LotInventory
    {
        [Key]
        public int LotInventoryId { get; set; }

        public string Description { get; set; }

        public bool IsArchive { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }

        
        public string LotId { get; set; }
        [ForeignKey("LotId")]
        public virtual Lot Lot { get; set; }

        public string InventoryId { get; set; }
        [ForeignKey("InventoryId")]
        public virtual Inventory Inventory { get; set; }


    }
}
