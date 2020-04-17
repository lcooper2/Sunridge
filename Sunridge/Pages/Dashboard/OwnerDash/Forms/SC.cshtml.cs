using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models.ViewModels;

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
        public SugComVM FormResObj { get; set; }
        public IActionResult OnGet(int? id)
        {

            FormResObj = new SugComVM
            {
                SuggestionComplaint = new Models.SuggestionComplaint(),

                FormSubmissions = new Models.FormSubmissions()
            };
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (id != null)
            {
                FormResObj.SuggestionComplaint = _unitOfWork.SuggestionComplaint.GetFirstOrDefault(u => u.Id == id);
                FormResObj.FormSubmissions = _unitOfWork.FormSubmissions.GetFirstOrDefault(u => u.Id == id);
                FormResObj.SuggestionComplaint.ApplicationUserId = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value).Id;
                //FormResObj.SuggestionComplaint.Type = "SC";
                FormResObj.FormSubmissions.IsCl = false;
                FormResObj.FormSubmissions.IsSC = true;
                FormResObj.FormSubmissions.IsWik = false;
                FormResObj.FormSubmissions.SubmitDate = DateTime.Now;
                FormResObj.FormSubmissions.FormId = FormResObj.SuggestionComplaint.Id;
                FormResObj.SuggestionComplaint.ApplicationUserId = claim.Value;
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
            if (FormResObj.SuggestionComplaint.Id == 0)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                //FormResObj.SuggestionComplaint.Type = "SC";
                FormResObj.FormSubmissions.IsCl = false;
                FormResObj.FormSubmissions.IsSC = true;
                FormResObj.FormSubmissions.IsWik = false;
                FormResObj.SuggestionComplaint.ApplicationUserId = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value).Id;
                _unitOfWork.SuggestionComplaint.Add(FormResObj.SuggestionComplaint);
                FormResObj.FormSubmissions.FormId = FormResObj.SuggestionComplaint.Id;
                _unitOfWork.FormSubmissions.Add(FormResObj.FormSubmissions);
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                FormResObj.FormSubmissions.IsCl = false;
                FormResObj.FormSubmissions.IsSC = true;
                FormResObj.FormSubmissions.IsWik = false;
                FormResObj.SuggestionComplaint.ApplicationUserId = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value).Id;
                _unitOfWork.SuggestionComplaint.Update(FormResObj.SuggestionComplaint);
                FormResObj.FormSubmissions.FormId = FormResObj.SuggestionComplaint.Id;
                _unitOfWork.FormSubmissions.Update(FormResObj.FormSubmissions);
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}