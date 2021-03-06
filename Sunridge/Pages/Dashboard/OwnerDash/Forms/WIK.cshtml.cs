﻿using System;
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
    public class WIKModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public WIKModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        [BindProperty]
        public WIKVM FormResObj { get; set; }
        public IActionResult OnGet(int? id)
        {
            FormResObj = new WIKVM
            {
                InKindWorkHours = new Models.InKindWorkHours(),

                FormResponse = new Models.FormResponse(),


                FormSubmissions = new Models.FormSubmissions()
            };
            //FormResObj = new Models.InKindWorkHours();
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            FormResObj.InKindWorkHours.ApplicationUserId = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value).Id;
            if (claim != null)
            {
                //FormResObj.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);
                if (id != null)
                {
                    FormResObj.InKindWorkHours = _unitOfWork.InKindWorkHours.GetFirstOrDefault(u => u.Id == id);
                    FormResObj.InKindWorkHours.ApplicationUserId = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value).Id;
                    FormResObj.InKindWorkHours.Type = "WIK";
                    FormResObj.FormResponse.FormType = "WIK";
                    FormResObj.FormSubmissions.IsCl = false;
                    FormResObj.FormSubmissions.IsSC = false;
                    FormResObj.FormSubmissions.IsWik = true;
                    FormResObj.FormSubmissions.SubmitDate = DateTime.Now;
                   // FormResObj.FormResponse.SubmitDate = DateTime.Now;
                    FormResObj.FormResponse.Resolved = false;
                    FormResObj.FormResponse.ResolveUser = "None";
                    FormResObj.FormResponse.FormSubmissionsId = FormResObj.FormSubmissions.Id;
                    FormResObj.FormSubmissions.FormId = FormResObj.InKindWorkHours.Id;
                    //FormResObj.FormResponse.SubmitDate = DateTime.Now;
                    FormResObj.InKindWorkHours.ApplicationUserId = claim.Value;
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
            if (FormResObj.InKindWorkHours.Id == 0)
            {
                FormResObj.InKindWorkHours.Type = "WIK";
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                FormResObj.InKindWorkHours.Type = "WIK";
                FormResObj.FormResponse.FormType = "WIK";
                FormResObj.FormSubmissions.IsCl = false;
                FormResObj.FormSubmissions.IsSC = false;
                FormResObj.FormSubmissions.IsWik = true;
                FormResObj.FormSubmissions.SubmitDate = DateTime.Now;
                //FormResObj.FormResponse.SubmitDate = DateTime.Now;
                FormResObj.FormResponse.Resolved = false;
                FormResObj.FormResponse.ResolveUser = "None";
                FormResObj.FormResponse.FormSubmissionsId = FormResObj.FormSubmissions.Id;
                FormResObj.FormSubmissions.FormId = FormResObj.InKindWorkHours.Id;
                FormResObj.InKindWorkHours.ApplicationUserId = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value).Id;
                _unitOfWork.InKindWorkHours.Add(FormResObj.InKindWorkHours);

                _unitOfWork.Save();

                FormResObj.FormSubmissions.FormId = FormResObj.InKindWorkHours.Id;
                _unitOfWork.FormSubmissions.Add(FormResObj.FormSubmissions);
                FormResObj.FormResponse.FormSubmissions = FormResObj.FormSubmissions;
                _unitOfWork.FormResponse.Add(FormResObj.FormResponse);
            }
            else
            {
                FormResObj.InKindWorkHours.Type = "WIK";
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                FormResObj.InKindWorkHours.Type = "WIK";
                FormResObj.FormSubmissions.IsCl = false;
                FormResObj.FormSubmissions.IsSC = false;
                FormResObj.FormSubmissions.IsWik = true;
                FormResObj.InKindWorkHours.ApplicationUserId = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value).Id;
                _unitOfWork.InKindWorkHours.Update(FormResObj.InKindWorkHours);
                FormResObj.FormSubmissions.FormId = FormResObj.InKindWorkHours.Id;
                _unitOfWork.FormSubmissions.Update(FormResObj.FormSubmissions);
                FormResObj.FormResponse.FormSubmissions = FormResObj.FormSubmissions;
                _unitOfWork.FormResponse.Update(FormResObj.FormResponse);
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}
