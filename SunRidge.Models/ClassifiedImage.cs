using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
    public class ClassifiedImage
    {
        public int Id { get; set; }
        public int ClassifiedListingId { get; set; }
        [ForeignKey("ClassifiedListingId")]
        public virtual ClassifiedListing ClassifiedListing { get; set; }
        public bool IsMainImage { get; set; }
        public string ImageURL { get; set; }
    }
}
