using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models.ViewModels;

namespace Sunridge.Pages.Dashboard.AdminDash.Forms
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public FormResponseVM FormResObj { get; set; }

        
        public IActionResult OnGet(int? id)
        {
            FormResObj = new FormResponseVM
            {
                FormResponse = new Models.FormResponse(),
                ClaimLoss = new Models.ClaimLoss(),
                SuggestionComplaint = new Models.SuggestionComplaint(),
                InKindWorkHours = new Models.InKindWorkHours(),

                FormSubmissions = new Models.FormSubmissions()
            };
            if (id != null)
            {
                FormResObj.FormResponse = _unitOfWork.FormResponse.GetFirstOrDefault(u => u.Id == id);
                FormResObj.FormSubmissions = _unitOfWork.FormSubmissions.GetFirstOrDefault(u => u.Id == FormResObj.FormResponse.FormSubmissionsId);
                if (FormResObj.FormSubmissions.IsCl == true)
                {
                    FormResObj.ClaimLoss = _unitOfWork.ClaimLoss.GetFirstOrDefault(u => u.Id == FormResObj.FormSubmissions.FormId);
                    FormResObj.FormResponse.FormType = "CL";
                    FormResObj.FormResponse.FormDisplay = "is Attorney: " + FormResObj.ClaimLoss.isAttorney.ToString() + "," + "\r\n" + "Date: " + FormResObj.ClaimLoss.DateofIncident.ToString() + "," + "\r\n" + "Address: "+ FormResObj.ClaimLoss.ClaimAddress.ToString();
                }
                if(FormResObj.FormSubmissions.IsSC == true)
                {
                    FormResObj.SuggestionComplaint = _unitOfWork.SuggestionComplaint.GetFirstOrDefault(u => u.Id == FormResObj.FormSubmissions.FormId);
                    FormResObj.FormResponse.FormType = "SC";
                    FormResObj.FormResponse.FormDisplay = "Suggestion: " + FormResObj.SuggestionComplaint.Suggestion + "," + "\r\n" + "Complaint: " + FormResObj.SuggestionComplaint.Complaint;
                       
                }
                if (FormResObj.FormSubmissions.IsWik == true)
                {
                    FormResObj.InKindWorkHours = _unitOfWork.InKindWorkHours.GetFirstOrDefault(u => u.Id == FormResObj.FormSubmissions.FormId);
                    FormResObj.FormResponse.FormType = "WIK";
                    FormResObj.FormResponse.FormDisplay = "Activity: " + FormResObj.InKindWorkHours.Activity + "," + "\r\n" + "Equipment: " + FormResObj.InKindWorkHours.Equipment + "," + "\r\n" + "Hours: " + FormResObj.InKindWorkHours.Hours.ToString();

                }
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
            if (FormResObj.FormResponse.Id == 0)
            {
                _unitOfWork.FormResponse.Add(FormResObj.FormResponse);
            }
            else
            {
                _unitOfWork.FormResponse.Update(FormResObj.FormResponse);
            }
            _unitOfWork.Save();
            return RedirectToPage("./FormRespnsepage");
        }
    }
}