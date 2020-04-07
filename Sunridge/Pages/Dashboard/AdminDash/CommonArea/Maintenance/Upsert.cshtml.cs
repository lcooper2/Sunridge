using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models.ViewModels;

namespace Sunridge.Pages.Dashboard.AdminDash.CommonArea.Maintenance
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        [BindProperty]
        public MaintenanceVM MantObj { get; set; }
        public IActionResult OnGet(int? id)
        {
            MantObj = new MaintenanceVM
            {
                CommonAssetList = _unitOfWork.CommonAreaAsset.GetCommonAssetListForDropDown(),

                Maintenance = new Models.Maintenance()
            };
            if (id != null)
            {
                MantObj.Maintenance = _unitOfWork.Maintenance.GetFirstOrDefault(u => u.Id == id);
                if (MantObj == null)
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
            if (MantObj.Maintenance.Id == 0)
            {
                _unitOfWork.Maintenance.Add(MantObj.Maintenance);
            }
            else
            {
                _unitOfWork.Maintenance.Update(MantObj.Maintenance);
            }
            _unitOfWork.Save();
            return RedirectToPage("./Maintenance");
        }
    }
}