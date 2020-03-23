using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models.ViewModels;

namespace Sunridge.Pages.Dashboard.AdminDash.Key.History
{//key history
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           
        }
        [BindProperty]
        public KeyHistoryVM KeyHistObj { get; set; }
        public IActionResult OnGet(int? id)
        {
            KeyHistObj = new KeyHistoryVM
            {
                LotList = _unitOfWork.Lot.GetLotListForDropDown(),
                KeyList = _unitOfWork.Key.GetKeyListForDropDown(),

                KeyHistory = new Models.KeyHistory()
            };
            if (id != null)
            {
                KeyHistObj.KeyHistory = _unitOfWork.KeyHistory.GetFirstOrDefault(u => u.Id == id);
                if (KeyHistObj == null)
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
            if (KeyHistObj.KeyHistory.Id == 0)
            {
                _unitOfWork.KeyHistory.Add(KeyHistObj.KeyHistory);
            }
            else
            {
                _unitOfWork.KeyHistory.Update(KeyHistObj.KeyHistory);
            }
            _unitOfWork.Save();
            return RedirectToPage("./History");
        }
    }
}