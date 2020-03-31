using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
       
       // public int OwnerId { get; set; }

        public string Content { get; set; }
        public DateTime Date { get; set; }
        public bool Private { get; set; }

        [Display(Name = "Lot History")]
        public int? LotHistoryId { get; set; }
        [ForeignKey("LotHistoryId")]
        public LotHistory LotHistory { get; set; }
        [Display(Name = "Form Response")]
        public int? FormResponseId { get; set; }
        [ForeignKey("FormResponseId")]
        public FormResponse FormResponse { get; set; }

        [Display(Name = "Application User")]
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

    }
}
