using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunridge.Models
{
    public class BlogReply
    {
        public int Id { get; set; }
        [Required]
        public int BlogCommentId { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }
        public DateTime WhenPosted { get; set; }
        public string ReplyText { get; set; }
        public int Depth { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}