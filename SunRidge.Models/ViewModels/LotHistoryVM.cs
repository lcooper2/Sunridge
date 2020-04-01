using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.Models.ViewModels
{
    public class LotHistoryVM
    {
        public LotHistory LotHistory { get; set; }
        public IEnumerable<SelectListItem> LotList { get; set; }

        public IEnumerable<SelectListItem> UserList { get; set; }

    }
}
