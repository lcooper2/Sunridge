using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
    public class LotHistory
    {
        [Key]
        public int Id { get; set; }

        public string PrivacyLevel { get; set; }
        public bool IsArchive { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "Lot")]
        public int LotId { get; set; }
        [ForeignKey("LotId")]
        public virtual Lot Lot { get; set; }

        //public virtual ICollection<File> Files { get; set; }
        //public ICollection<Comment> Comments { get; set; }

    }
}
