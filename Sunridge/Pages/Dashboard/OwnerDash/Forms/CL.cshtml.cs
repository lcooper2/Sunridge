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
    public class CLModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public CLModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        [BindProperty]
        public ClaimLossVM FormResObj { get; set; }
        public IActionResult OnGet(int? id)
        {
            FormResObj = new ClaimLossVM
            {
                LotList = _unitOfWork.Lot.GetLotListForDropDown(),

                ClaimLoss = new Models.ClaimLoss(),

                FormResponse = new Models.FormResponse(),

                FormSubmissions = new Models.FormSubmissions()
            };
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                
                if (id != null)
                {
                    FormResObj.ClaimLoss = _unitOfWork.ClaimLoss.GetFirstOrDefault(u => u.Id == id);
                    FormResObj.FormSubmissions = _unitOfWork.FormSubmissions.GetFirstOrDefault(u => u.Id == id);
                    FormResObj.FormResponse = _unitOfWork.FormResponse.GetFirstOrDefault(u => u.Id == id);
                    FormResObj.ClaimLoss.ApplicationUserId = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value).Id;
                    FormResObj.ClaimLoss.Type = "CL";
                    FormResObj.FormResponse.FormType = "CL";
                    FormResObj.FormSubmissions.IsCl = true;
                    FormResObj.FormSubmissions.IsSC = false;
                    FormResObj.FormSubmissions.IsWik = false;
                    FormResObj.FormSubmissions.SubmitDate = DateTime.Now;
                    FormResObj.FormResponse.SubmitDate = DateTime.Now;
                    FormResObj.FormResponse.Resolved = false;
                    FormResObj.FormResponse.ResolveUser = "None";
                    FormResObj.FormSubmissions.FormId = id.ToString();
                    //FormResObj.FormResponse.SubmitDate = DateTime.Now;
                    FormResObj.ClaimLoss.ApplicationUserId = claim.Value;
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
            if (FormResObj.ClaimLoss.Id == 0)
            {

                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                FormResObj.ClaimLoss.ApplicationUserId = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value).Id;
                 FormResObj.ClaimLoss.Type = "CL";
                FormResObj.FormSubmissions.IsCl = true;
                FormResObj.FormSubmissions.IsSC = false;
                FormResObj.FormSubmissions.IsWik = false;
               
                FormResObj.FormResponse.Resolved = false;
                FormResObj.FormResponse.ResolveUser = "None";
                // FormResObj.FormSubmissions.FormId = FormResObj.ClaimLoss.Id.ToString();
                _unitOfWork.ClaimLoss.Add(FormResObj.ClaimLoss);
                FormResObj.FormSubmissions.FormId = FormResObj.ClaimLoss.Id.ToString();
                _unitOfWork.FormSubmissions.Add(FormResObj.FormSubmissions);
                _unitOfWork.FormResponse.Add(FormResObj.FormResponse);
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                FormResObj.ClaimLoss.ApplicationUserId = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value).Id;
                FormResObj.ClaimLoss.Type = "CL";
                FormResObj.FormSubmissions.IsCl = true;
                FormResObj.FormSubmissions.IsSC = false;
                FormResObj.FormSubmissions.IsWik = false;
                //FormResObj.FormResponse.SubmitDate = DateTime.Now;
                FormResObj.FormResponse.Resolved = false;
                FormResObj.FormResponse.ResolveUser = "None";
                _unitOfWork.ClaimLoss.Update(FormResObj.ClaimLoss);
                FormResObj.FormSubmissions.FormId = FormResObj.ClaimLoss.Id.ToString();
                _unitOfWork.FormSubmissions.Update(FormResObj.FormSubmissions);
                _unitOfWork.FormResponse.Update(FormResObj.FormResponse);
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}