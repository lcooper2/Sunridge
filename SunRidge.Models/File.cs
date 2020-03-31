using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
    public class File
    {
        [Key]
        public int Id { get; set; }
        
        public string FileURL { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        //Nav properties
        [Display(Name = "Lot History")]
        public int LotHistoryId { get; set; }
        [ForeignKey("LotHistoryId")]
        public LotHistory LotHistory { get; set; }
    }
}
