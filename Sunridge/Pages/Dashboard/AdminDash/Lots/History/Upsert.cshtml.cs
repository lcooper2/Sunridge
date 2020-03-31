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

                LotHistory = new Models.LotHistory()
            };
            if (id != null)
            {
                LotHistObj.LotHistory = _unitOfWork.LotHistory.GetFirstOrDefault(u => u.Id == id);
                LotHistObj.LotHistory.LastModifiedDate = DateTime.Now;
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
            if (LotHistObj.LotHistory.Id == 0)
            {
                _unitOfWork.LotHistory.Add(LotHistObj.LotHistory);
            }
            else
            {
                _unitOfWork.LotHistory.Update(LotHistObj.LotHistory);
            }
            _unitOfWork.Save();
            return RedirectToPage("./History");
        }
    }
}