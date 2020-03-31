﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
       
        //Nav properties
        [Display(Name = "Form Response")]
        public int FormResponseId { get; set; }
        [ForeignKey("FormResponseId")]
        public FormResponse FormResponse { get; set; }
    }
}
