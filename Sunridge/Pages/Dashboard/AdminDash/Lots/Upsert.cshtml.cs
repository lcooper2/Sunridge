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
            //public Models.Address AddressObj { get; set; }
            public IActionResult OnGet(int? id)
            {
                LotObj = new LotAddressVM
                { 
                    Lot = new Models.Lot(),
                    Address = new Models.Address()
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
                LotObj.Lot.Address = LotObj.Address;
                _unitOfWork.Lot.Add(LotObj.Lot);
                    
                   
                }
                else
                {
                
                _unitOfWork.Address.Update(LotObj.Address);
                LotObj.Lot.AddressId = LotObj.Address.Id;
                _unitOfWork.Lot.Update(LotObj.Lot);
                  
                }
                _unitOfWork.Save();
                return RedirectToPage("./Index");
            }
    }
}