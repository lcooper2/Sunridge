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
    public class OthersModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public OthersModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public ClassifiedsVM ClassifiedsObj { get; set; }
        public void OnGet()
        {
            var categoryId = _unitOfWork.ClassifiedCategory.GetFirstOrDefault(c => c.Description == "Other Services");

            //if Other Services doesn't exist in db
            if (categoryId == null)
            {
                _unitOfWork.ClassifiedCategory.Add(new ClassifiedCategory { Description = "Other Services" });
                _unitOfWork.Save();
                categoryId = _unitOfWork.ClassifiedCategory.GetFirstOrDefault(c => c.Description == "Other Services");
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