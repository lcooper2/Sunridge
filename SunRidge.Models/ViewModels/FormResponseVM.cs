﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.Models.ViewModels
{
    class FormResponseVM
    {
        public FormResponse FormResponse { get; set; }

        public IEnumerable<SelectListItem> WIKList { get; set; }

    }
}