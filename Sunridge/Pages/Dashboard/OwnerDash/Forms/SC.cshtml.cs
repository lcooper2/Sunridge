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
                FormResponse = new Models.FormResponse(),

                FormSubmissions = new Models.FormSubmissions()
            };
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (id != null)
            {
                FormResObj.SuggestionComplaint = _unitOfWork.SuggestionComplaint.GetFirstOrDefault(u => u.Id == id);
                FormResObj.FormSubmissions = _unitOfWork.FormSubmissions.GetFirstOrDefault(u => u.Id == id);
                FormResObj.SuggestionComplaint.ApplicationUserId = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value).Id;
                FormResObj.FormResponse.FormType = "SC";
                FormResObj.FormSubmissions.IsCl = false;
                FormResObj.FormSubmissions.IsSC = true;
                FormResObj.FormSubmissions.IsWik = false;
                FormResObj.FormSubmissions.SubmitDate = DateTime.Now;
                FormResObj.FormResponse.SubmitDate = DateTime.Now;
                FormResObj.FormResponse.Resolved = false;
                FormResObj.FormResponse.ResolveUser = "None";
                FormResObj.FormResponse.FormSubmissionsId = FormResObj.FormSubmissions.Id;
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
                FormResObj.FormResponse.FormType = "SC";
                FormResObj.FormSubmissions.IsCl = false;
                FormResObj.FormSubmissions.IsSC = true;
                FormResObj.FormSubmissions.IsWik = false;
                FormResObj.FormSubmissions.SubmitDate = DateTime.Now;
                FormResObj.FormResponse.SubmitDate = DateTime.Now;
                FormResObj.FormResponse.Resolved = false;
                FormResObj.FormResponse.ResolveUser = "None";
                FormResObj.FormResponse.FormSubmissionsId = FormResObj.FormSubmissions.Id;
                FormResObj.FormSubmissions.FormId = FormResObj.SuggestionComplaint.Id;
                FormResObj.SuggestionComplaint.ApplicationUserId = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value).Id;
                _unitOfWork.SuggestionComplaint.Add(FormResObj.SuggestionComplaint);
                FormResObj.FormSubmissions.FormId = FormResObj.SuggestionComplaint.Id;
                _unitOfWork.FormSubmissions.Add(FormResObj.FormSubmissions);
                FormResObj.FormResponse.FormSubmissions = FormResObj.FormSubmissions;
                _unitOfWork.FormResponse.Add(FormResObj.FormResponse);
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                FormResObj.FormResponse.FormType = "SC";
                FormResObj.FormSubmissions.IsCl = false;
                FormResObj.FormSubmissions.IsSC = true;
                FormResObj.FormSubmissions.IsWik = false;
                FormResObj.FormSubmissions.SubmitDate = DateTime.Now;
                FormResObj.FormResponse.SubmitDate = DateTime.Now;
                FormResObj.FormResponse.Resolved = false;
                FormResObj.FormResponse.ResolveUser = "None";
                FormResObj.FormResponse.FormSubmissionsId = FormResObj.FormSubmissions.Id;
                FormResObj.FormSubmissions.FormId = FormResObj.SuggestionComplaint.Id;
                FormResObj.SuggestionComplaint.ApplicationUserId = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value).Id;
                _unitOfWork.SuggestionComplaint.Update(FormResObj.SuggestionComplaint);
                FormResObj.FormSubmissions.FormId = FormResObj.SuggestionComplaint.Id;
                _unitOfWork.FormSubmissions.Update(FormResObj.FormSubmissions);
                FormResObj.FormResponse.FormSubmissions = FormResObj.FormSubmissions;
                _unitOfWork.FormResponse.Update(FormResObj.FormResponse);
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}