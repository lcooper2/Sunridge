using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.Models.ViewModels
{
    public class ClaimLossVM
    {

        public ClaimLoss ClaimLoss { get; set; }

        public IEnumerable<SelectListItem> LotList { get; set; }

    }
}
