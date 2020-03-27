using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sunridge.Models
{
    public class InKindWorkHours
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public double? Hours { get; set; }
        public string Type { get; set; }
        public int FormResponseId { get; set; }
    }
}