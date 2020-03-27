using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunridge.Models
{
    public class Thread
    {
        public int Id { get; set; } // The id of the thread
        [Required]
        public string ApplicationUserId { get; set; } // The individual who started the thread
        public DateTime WhenPosted { get; set; } // When the thread was started

        public virtual List<Comment> Comments { get; set; } // The list of replies to the OP

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}

