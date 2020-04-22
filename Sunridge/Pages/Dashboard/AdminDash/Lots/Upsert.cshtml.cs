using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
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
        public LotAddressVM LotObj { get; set; }

        [BindProperty]
        public List<Models.Inventory> InList { get; set; }

        [BindProperty]
        public LotInventoryVM LotInVm { get; set; }
        //public Models.Address AddressObj { get; set; }
        public IActionResult OnGet(int? id)
        {
            LotObj = new LotAddressVM
            {
                Lot = new Models.Lot(),
                Address = new Models.Address()
            };


            InList = _unitOfWork.Inventory.GetAll().ToList();
            LotInVm = new LotInventoryVM()
            {
                LotId = new int(),
                InventoryList = new List<int>()

            };



            if (id != null)
            {

                LotObj.Lot = _unitOfWork.Lot.GetFirstOrDefault(u => u.Id == id);
                LotObj.Address.Id = LotObj.Lot.AddressId;
                LotObj.Address = _unitOfWork.Address.GetFirstOrDefault(u => u.Id == LotObj.Address.Id);
                LotObj.Lot.Address = LotObj.Address;


                LotObj.Lot.LastModifiedDate = DateTime.Now;

                LotObj.Address.LastModifiedDate = DateTime.Now;
                LotObj.Address.LastModifiedBy = "None";
                if (LotObj == null)
                {
                    return NotFound();
                }
                var list = _unitOfWork.LotInventory.GetAll(u => u.LotId == id).ToList();
                LotInVm.LotId = LotObj.Lot.Id;
                if (list.Count != 0)
                {
                    foreach (var item in list)
                    {
                        LotInVm.InventoryList.Add(item.InventoryId);
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
            if (LotObj.Lot.Id == 0)
            {

                _unitOfWork.Address.Add(LotObj.Address);
                _unitOfWork.Save();
                LotObj.Lot.AddressId = LotObj.Address.Id;
                _unitOfWork.Lot.Add(LotObj.Lot);
                _unitOfWork.Save();
            }
            else
            {

                _unitOfWork.Address.Update(LotObj.Address);
                LotObj.Lot.AddressId = LotObj.Address.Id;
                _unitOfWork.Lot.Update(LotObj.Lot);

                var list = _unitOfWork.LotInventory.GetAll(u => u.LotId == LotObj.Lot.Id);
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        _unitOfWork.LotInventory.Remove(item);

                    }
                }
            }

           
            if (LotInVm.InventoryList != null)
            {
                foreach (var item in LotInVm.InventoryList)
                {
                    var LotInItem = new Models.LotInventory()
                    {
                        LotId = LotObj.Lot.Id,
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