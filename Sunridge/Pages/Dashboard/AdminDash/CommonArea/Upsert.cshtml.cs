using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;

namespace Sunridge.Pages.Dashboard.AdminDash.CommonArea
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        [BindProperty]
        public Models.CommonAreaAsset ComAAObj { get; set; }
        public IActionResult OnGet(int? id)
        {
            ComAAObj = new Models.CommonAreaAsset();
            if (id != null)
            {
                ComAAObj = _unitOfWork.CommonAreaAsset.GetFirstOrDefault(u => u.Id == id);
                if (ComAAObj == null)
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
            if (ComAAObj.Id == 0)
            {
                _unitOfWork.CommonAreaAsset.Add(ComAAObj);
            }
            else
            {
                _unitOfWork.CommonAreaAsset.Update(ComAAObj);
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}