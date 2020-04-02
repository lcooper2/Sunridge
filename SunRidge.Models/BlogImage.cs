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
        public int BlogCommentId { get; set; }  // comment id optional as image may be associated with thread id

        [ForeignKey("BlogCommentId")]
        public virtual BlogComment BlogComment { get; set; }
    }
}