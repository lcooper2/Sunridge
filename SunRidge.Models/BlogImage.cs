using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunridge.Models
{
    public class BlogImage
    {
        public int Id { get; set; }
        [Required]
        public string ImgPath { get; set; }
        [Required]
        public int CommentId { get; set; }  // comment id optional as image may be associated with thread id

        [ForeignKey("CommentId")]
        public virtual Comment Comment { get; set; }
    }
}
