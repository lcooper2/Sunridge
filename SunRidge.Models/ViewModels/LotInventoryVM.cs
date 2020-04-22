using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.Models.ViewModels
{
    public class LotInventoryVM
    {
        public int LotId { get; set; }
        public List<int> InventoryList { get; set; }

    }
}
