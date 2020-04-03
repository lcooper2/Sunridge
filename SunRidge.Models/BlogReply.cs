using System;
using System.ComponentModel.DataAnnotations;

namespace Sunridge.Models
{
    public class BlogReply
    {
        public int Id { get; set; }
        [Required]
        public int BlogCommentId { get; set; }
        public DateTime WhenPosted { get; set; }
        public string ReplyText { get; set; }
        public int Depth { get; set; }
    }
}