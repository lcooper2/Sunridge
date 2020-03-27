using System;
using System.ComponentModel.DataAnnotations;

namespace Sunridge.Models
{
    public class Reply
    {
        public int Id { get; set; }
        [Required]
        public int CommentId { get; set; }
        public DateTime WhenPosted { get; set; }
        public string ReplyText { get; set; }
        public int Depth { get; set; }
    }
}
