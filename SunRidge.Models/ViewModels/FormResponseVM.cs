using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.Models.ViewModels
{
    public class FormResponseVM
    {
        public FormResponse FormResponse { get; set; }

        public IEnumerable<InKindWorkHours> WIKList { get; set; }

    }
}
