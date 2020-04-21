using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.Models.ViewModels
{
    public class UserPhotosVM
    {
        public UserPhotos UserPhotos { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }

    }
}
