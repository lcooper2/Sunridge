using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Models.ViewModels;

namespace Sunridge
{
    public class PhotoGalleryModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public PhotoGalleryModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<UserPhotos> userPhotosList { get; set; }
        [BindProperty]
        public List<PhotosGalleryVM> pgVMList { get; set; }

        public void OnGet(int id)
        {
            userPhotosList = new List<UserPhotos>();
            userPhotosList = _unitOfWork.UserPhotos.GetAll(u => u.CategoryId == id).ToList();

            pgVMList = new List<PhotosGalleryVM>();
            foreach (var item in userPhotosList) {
                var user = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == item.UserId);
                PhotosGalleryVM pgVMItem = new PhotosGalleryVM()
                {
                    userPhotos = item,
                    userName = user.FullName
                };
                pgVMList.Add(pgVMItem);
            }


        }


    }
}