using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sunridge.Models
{
    public class Maintenance
    {
        [Key]
        public int Id { get; set; }
        public int CommonAreaAssetId { get; set; }

        public float Cost { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateCompleted { get; set; }
        //public string DateCompleted { get; set; }

        //Nav properties
        public CommonAreaAsset CommonAreaAsset { get; set; }
    }
}
