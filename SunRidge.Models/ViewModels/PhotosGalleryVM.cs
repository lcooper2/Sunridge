using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.Models.ViewModels
{
    public class PhotosGalleryVM
    {
        public UserPhotos userPhotos { get; set; }
        public string userName { get; set; }

    }
}
