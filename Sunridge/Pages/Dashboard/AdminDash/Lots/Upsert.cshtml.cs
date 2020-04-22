using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models.ViewModels;

namespace Sunridge.Pages.Dashboard.AdminDash.Lots
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public List<Models.Inventory> InList { get; set; }

        [BindProperty]
        public Models.Lot LotObj { get; set; }

        [BindProperty]
        public LotInventoryVM LotInVm { get; set; }

        //public Models.Address AddressObj { get; set; }
        public IActionResult OnGet(int? id)
        {

            LotObj = new Models.Lot();

            InList = _unitOfWork.Inventory.GetAll().ToList();
            LotInVm = new LotInventoryVM();

            if (id != null)
            {
                LotObj = _unitOfWork.Lot.GetFirstOrDefault(u => u.Id == id);
                LotObj.LastModifiedDate = DateTime.Now;
                LotObj.Address = _unitOfWork.Address.GetFirstOrDefault(u => u.Id == id);
                LotObj.Address.LastModifiedDate = DateTime.Now;
                if (LotObj == null)
                {
                    return NotFound();
                }
                var list = _unitOfWork.LotInventory.GetAll(u => u.LotId == id).ToList();
                LotInVm.LotId = LotObj.Id;
                if (list.Count !=0) { 
                foreach (var item in list) {
                    LotInVm.InventoryList.Add(item.LotId);
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
            if (LotObj.Id == 0)
            {
                _unitOfWork.Lot.Add(LotObj);
                _unitOfWork.Save();
                //if(AddressObj.Id == 0)
                //{
                //  _unitOfWork.Address.Add(AddressObj);
                //}
            }
            //if (AddressObj.Id == 0)
            //{
            //  _unitOfWork.Address.Add(AddressObj);
            //}
            else
            {
                _unitOfWork.Lot.Update(LotObj);
                //_unitOfWork.Address.Update(AddressObj);
            }
           
            if(LotInVm.InventoryList != null)
            {
                foreach(var item in LotInVm.InventoryList)
                {
                    var LotInItem = new Models.LotInventory()
                    {
                        LotId = LotObj.Id,
                        InventoryId = item
                    };
                    _unitOfWork.LotInventory.Add(LotInItem);
                }

            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}