using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunridge.Models
{
    // An object that represents liking a post
    public class BlogLike
    {
        public int Id { get; set; }
        [Required]
        public string ApplicationUserId { get; set; } // The Id of the user who is doing the liking
        public int BlogCommentId { get; set; }  // comment id optional as like may be associated with thread id


        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("BlogCommentId")]
        public virtual BlogComment BlogComment { get; set; }
    }
}