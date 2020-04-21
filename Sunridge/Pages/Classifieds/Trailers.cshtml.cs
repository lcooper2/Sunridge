using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Models.ViewModels;

namespace Sunridge.Pages.Classifieds
{
    public class TrailersModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrailersModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public ClassifiedsVM ClassifiedsObj { get; set; }
        public void OnGet()
        {
            var categoryId = _unitOfWork.ClassifiedCategory.GetFirstOrDefault(c => c.Description == "Trailer");

            //if Trailer doesn't exist in db
            if (categoryId == null)
            {
                _unitOfWork.ClassifiedCategory.Add(new ClassifiedCategory { Description = "Trailer" });
                _unitOfWork.Save();
                categoryId = _unitOfWork.ClassifiedCategory.GetFirstOrDefault(c => c.Description == "Trailer");
            }

            ClassifiedsObj = new ClassifiedsVM()
            {
                ClassifiedsList = _unitOfWork.ClassifiedListing.GetAll(c => c.ClassifiedCategoryId == categoryId.Id).ToList()
            };

            foreach (var item in ClassifiedsObj.ClassifiedsList)
            {
                item.Images = _unitOfWork.ClassifiedImage.GetAll(c => c.ClassifiedListingId == item.Id).ToList();
            }
        }
    }
}