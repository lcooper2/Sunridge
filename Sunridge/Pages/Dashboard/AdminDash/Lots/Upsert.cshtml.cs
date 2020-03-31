using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;

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
            public Models.Lot LotObj { get; set; }
            //public Models.Address AddressObj { get; set; }
            public IActionResult OnGet(int? id)
            {
                LotObj = new Models.Lot();
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
                _unitOfWork.Save();
                return RedirectToPage("./Index");
            }
    }
}