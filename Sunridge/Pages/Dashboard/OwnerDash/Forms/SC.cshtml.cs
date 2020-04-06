using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;

namespace Sunridge.Pages.Dashboard.OwnerDash.Forms
{
    public class SCModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public SCModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        [BindProperty]
        public Models.SuggestionComplaint FormResObj { get; set; }
        public IActionResult OnGet(int? id)
        {
            FormResObj = new Models.SuggestionComplaint();
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (id != null)
            {
                FormResObj = _unitOfWork.SuggestionComplaint.GetFirstOrDefault(u => u.Id == id);
              
                FormResObj.ApplicationUserId = claim.Value;
                if (FormResObj == null)
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
            if (FormResObj.Id == 0)
            {
                _unitOfWork.SuggestionComplaint.Add(FormResObj);
            }
            else
            {
                _unitOfWork.SuggestionComplaint.Update(FormResObj);
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}