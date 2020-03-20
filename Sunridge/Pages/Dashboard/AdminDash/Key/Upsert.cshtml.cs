using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;

namespace Sunridge.Pages.Dashboard.AdminDash.Key
{
    public class UpsertModel : PageModel
    {
            private readonly IUnitOfWork _unitOfWork;
            public UpsertModel(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            [BindProperty]
            public Models.Key KeyObj { get; set; }
            public IActionResult OnGet(int? id)
            {
                KeyObj = new Models.Key();
                if (id != null)
                {
                    KeyObj = _unitOfWork.Key.GetFirstOrDefault(u => u.Id == id);
                    if (KeyObj == null)
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
                if (KeyObj.Id == 0)
                {
                    _unitOfWork.Key.Add(KeyObj);
                }
                else
                {
                    _unitOfWork.Key.Update(KeyObj);
                }
                _unitOfWork.Save();
                return RedirectToPage("./Key");
            }
    }
}