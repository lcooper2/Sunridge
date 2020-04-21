using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;

namespace Sunridge.Pages.Dashboard.AdminDash.PhotosManager.PhotoCategory
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Models.UserPhotoCategory PhotosCategoryObj { get; set; }

        public IActionResult OnGet(int? id)
        {
            PhotosCategoryObj = new Models.UserPhotoCategory();
            if (id != null)// edit
            {
                PhotosCategoryObj = _unitOfWork.UserPhotoCategory.GetFirstOrDefault(u => u.Id == id);
                if (PhotosCategoryObj == null)
                {
                    return NotFound();
                }
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (PhotosCategoryObj.Id == 0) // is new foodtype
            {
                _unitOfWork.UserPhotoCategory.Add(PhotosCategoryObj);
            }
            else // edit
            {
                _unitOfWork.UserPhotoCategory.Update(PhotosCategoryObj);
            }

            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}