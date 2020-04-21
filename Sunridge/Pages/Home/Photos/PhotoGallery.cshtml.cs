using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge
{
    public class PhotoGalleryModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public PhotoGalleryModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<UserPhotos> userPhotosList { get; set; }



        public void OnGet(int id)
        {
            userPhotosList = _unitOfWork.UserPhotos.GetAll(u => u.CategoryId == id);
        }


    }
}