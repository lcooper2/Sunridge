using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;

namespace Sunridge.Pages.Dashboard.AdminDash.Key
{
    public class HistoryModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;
        public HistoryModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        [BindProperty]
        public Models.KeyHistory KeyHistObj { get; set; }
        public IActionResult OnGet(int? id)
        {
            KeyHistObj = new Models.KeyHistory();
            if (id != null)
            {
                KeyHistObj = _unitOfWork.KeyHistory.GetFirstOrDefault(u => u.KeyId == id);
                if (KeyHistObj == null)
                {
                    return NotFound();
                }
            }
            return Page();
        }
    }
}