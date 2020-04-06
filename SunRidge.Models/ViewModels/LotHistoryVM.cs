using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.Models.ViewModels
{
    public class LotHistoryVM
    {
        public OwnerLot OwnerLot { get; set; }
        public IEnumerable<SelectListItem> LotList { get; set; }

        public IEnumerable<SelectListItem> UserList { get; set; }

    }
}
