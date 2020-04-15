using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Models.ViewModels;

namespace Sunridge.Pages.Dashboard.AdminDash.Forms
{
    public class FormRespnsepageModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IEnumerable<FormResponse> FormList { get; set; }
        public IEnumerable<FormSubmissions> SubList { get; set; }
        public FormRespnsepageModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public FormResponseVM FormResObj { get; set; }
       
            //FormList = _unitOfWork.FormResponse.GetAll(null, q => q.OrderBy(c => c.Resolved), null);
        public IActionResult OnGet(int? id)
        {
            //SubList = _unitOfWork.FormSubmissions.GetAll(null, q => q.OrderBy(c => c.FormType), null);
            FormList = _unitOfWork.FormResponse.GetAll(null, null, "FormSubmissions");
            FormResObj = new FormResponseVM
            {
               FSList = _unitOfWork.FormSubmissions.GetFormSubmissionsListForDropDown(),
                
               FormSubmissions = new Models.FormSubmissions(),
                FormResponse = new Models.FormResponse()
            };
            SubList = _unitOfWork.FormSubmissions.GetAll(null, null);
            if (id != null)
            {
                FormResObj.FormResponse = _unitOfWork.FormResponse.GetFirstOrDefault(u => u.Id == id);
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