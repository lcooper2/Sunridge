﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunridge.Models
{
    public class BlogComment
    {
        public int Id { get; set; } // Comment Id
        [Required]
        public string ApplicationUserId { get; set; } // Person who made the comment
        [Required]
        public int BlogThreadId { get; set; } // The thread that the comment belongs to
        public DateTime WhenPosted { get; set; } // When the comment was made
        public string BlogCommentText { get; set; } // What was said
        public virtual List<BlogImage> Images { get; set; } // Images uploaded with comment
        public virtual List<BlogLike> Likes { get; set; } // List of like objects representing somebody liking the post
        public virtual List<BlogReply> Replies { get; set; } // List of replies to a comment


        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("BlogThreadId")]
        public virtual BlogThread Thread { get; set; }
    }
}