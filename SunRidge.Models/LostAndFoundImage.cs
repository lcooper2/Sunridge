using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
    public class LostAndFoundImage
    {
        public int Id { get; set; }
        public int LostAndFoundId { get; set; }
        [ForeignKey("LostAndFoundId")]
        public virtual LostAndFound LostAndFound { get; set; }
        public bool IsMainImage { get; set; }
        public string ImageURL { get; set; }
    }
}
