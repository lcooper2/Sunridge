using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunridge.Models
{
    public class BlogImage : IComparable
    {
        public int Id { get; set; }
        [Required]
        public string ImgPath { get; set; }
        [Required]
        public int BlogCommentId { get; set; }  // comment id optional as image may be associated with thread id

        [ForeignKey("BlogCommentId")]
        public virtual BlogComment BlogComment { get; set; }

        public int CompareTo(object obj)
        {
            BlogImage other = (BlogImage)obj;
            if(Id > other.Id) { return 1; };
            if(Id == other.Id) { return 0; }
            return -1;
        }
    }
}