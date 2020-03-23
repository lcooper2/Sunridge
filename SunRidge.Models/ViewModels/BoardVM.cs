using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.Models.ViewModels
{
    public class BoardVM
    {
        public Board Board { get; set; }

        public IEnumerable<SelectListItem> ApplicationUserList { get; set; }

    }
}
