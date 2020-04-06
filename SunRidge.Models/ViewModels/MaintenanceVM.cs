using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.Models.ViewModels
{
    public class MaintenanceVM
    {
        public Maintenance Maintenance { get; set; }

        public IEnumerable<SelectListItem> CommonAssetList { get; set; }
    }
}
