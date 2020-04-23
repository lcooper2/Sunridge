using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Utility;

namespace Sunridge.Pages.Dashboard.AdminDash.Lots.InventoryCategory
{
    [Authorize(Roles = SD.AdminRole)]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Models.Inventory InventoryItem { get; set; }

        public IActionResult OnGet(int? id)
        {
            InventoryItem = new Models.Inventory();
            if (id != null)// edit
            {
                InventoryItem = _unitOfWork.Inventory.GetFirstOrDefault(u => u.Id == id);
                if (InventoryItem == null)
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

            if (InventoryItem.Id == 0) // is new foodtype
            {
                _unitOfWork.Inventory.Add(InventoryItem);
            }
            else // edit
            {
                _unitOfWork.Inventory.Update(InventoryItem);
            }

            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}