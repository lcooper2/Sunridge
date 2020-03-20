using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.Models.ViewModels
{
    public class KeyHistoryVM
    {
        public KeyHistory KeyHistory { get; set; }
        public IEnumerable<SelectListItem> LotList { get; set; }

    }
}
