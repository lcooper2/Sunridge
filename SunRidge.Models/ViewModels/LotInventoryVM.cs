using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.Models.ViewModels
{
    public class LotInventoryVM
    {
        public LotInventory LotInventory { get; set; }

        public List<Inventory> InventoryList { get; set; }

    }
}
