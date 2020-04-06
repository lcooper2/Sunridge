using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Models.ViewModels;

namespace Sunridge.Pages.Dashboard.AdminDash.Lots.History
{//lot history
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           
        }
        [BindProperty]
        public LotHistoryVM LotHistObj { get; set; }
        public IActionResult OnGet(int? id)
        {
            LotHistObj = new LotHistoryVM
            {
                LotList = _unitOfWork.Lot.GetLotListForDropDown(),
                UserList = _unitOfWork.ApplicationUser.GetUserListForDropDown(),

                OwnerLot = new Models.OwnerLot()
            };
            if (id != null)
            {
                LotHistObj.OwnerLot = _unitOfWork.OwnerLot.GetFirstOrDefault(u => u.Id == id);
                //LotHistObj.OwnerLot.LastModifiedDate = DateTime.Now;
                if (LotHistObj == null)
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
            if (LotHistObj.OwnerLot.Id == 0)
            {
                _unitOfWork.OwnerLot.Add(LotHistObj.OwnerLot);
            }
            else
            {
                _unitOfWork.OwnerLot.Update(LotHistObj.OwnerLot);
            }
            _unitOfWork.Save();
            return RedirectToPage("./History");
        }
    }
}