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
    public class WIKModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public WIKModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        [BindProperty]
        public Models.InKindWorkHours FormResObj { get; set; }
        public IActionResult OnGet(int? id)
        {
            FormResObj = new Models.InKindWorkHours();
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                //FormResObj.InKindWorkHours = _unitOfWork.InKindWorkHours.GetFirstOrDefault(u => u.ApplicationUserId == claim.Value);
                if (id != null)
                {
                    FormResObj = _unitOfWork.InKindWorkHours.GetFirstOrDefault(u => u.Id == id);
                    //FormResObj.FormResponse = _unitOfWork.FormResponse.GetFirstOrDefault(u => u.Id == id);
                    FormResObj.Type = "WIK";
                    //FormResObj.FormResponse.SubmitDate = DateTime.Now;
                    FormResObj.ApplicationUserId = claim.Value;
                    if (FormResObj == null)
                    {
                        return NotFound();
                    }
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
                _unitOfWork.InKindWorkHours.Add(FormResObj);
            }
            else
            {
                _unitOfWork.InKindWorkHours.Update(FormResObj);
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}
