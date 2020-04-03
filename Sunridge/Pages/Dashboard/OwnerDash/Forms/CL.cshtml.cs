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

                ClaimLoss = new Models.ClaimLoss()
            };

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                //FormResObj.ClaimLoss = _unitOfWork.ClaimLoss.GetAll(u => u.ApplicationUserId == claim.Value);
                if (id != null)
                {
                    FormResObj.ClaimLoss = _unitOfWork.ClaimLoss.GetFirstOrDefault(u => u.Id == id);
                    //FormResObj.FormResponse = _unitOfWork.FormResponse.GetFirstOrDefault(u => u.Id == id);
                    FormResObj.ClaimLoss.Type = "CL";
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
                FormResObj.ClaimLoss.Type = "CL";
                _unitOfWork.ClaimLoss.Add(FormResObj.ClaimLoss);
            }
            else
            {
                FormResObj.ClaimLoss.Type = "CL";
                _unitOfWork.ClaimLoss.Update(FormResObj.ClaimLoss);
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}