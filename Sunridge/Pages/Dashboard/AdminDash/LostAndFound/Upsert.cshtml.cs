using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Dashboard.AdminDash.LostAndFound
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        

        [BindProperty]
        public Models.LostAndFound LAFObj { get; set; } // Lost And Found Object

        
        public IActionResult OnGet(int? id)
        {
            LAFObj = new Models.LostAndFound();

            if (id != null)// allows edit to be used
            {
                LAFObj = _unitOfWork.LostAndFound.GetFirstOrDefault(s => s.Id == id);
                 
                if (LAFObj == null) // catches the exception if it can not find the Lost and Found object
                {
                    return NotFound();
                }
                
            }
            return Page();
        }

        public IActionResult OnPost() {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (LAFObj.Id == 0)// is a new Lost And found Object
            {
                LAFObj.ListedDate = DateTime.Now;
                _unitOfWork.LostAndFound.Add(LAFObj);

            }
            else // edit 
            {
                if (LAFObj.Claimed == false)
                {
                    LAFObj.ClaimedBy = null;
                }
                else if (LAFObj.Claimed == true) {
                    LAFObj.ClaimedDate = DateTime.Now;
                }
                _unitOfWork.LostAndFound.Update(LAFObj);
            }

            _unitOfWork.Save();
            return RedirectToPage("./Index");

        }
    }
}