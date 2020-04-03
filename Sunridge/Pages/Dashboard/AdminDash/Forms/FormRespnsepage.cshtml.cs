using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Dashboard.AdminDash.Forms
{
    public class FormRespnsepageModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IEnumerable<FormResponse> FormList { get; set; }

        public FormRespnsepageModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            FormList = _unitOfWork.FormResponse.GetAll(null, q => q.OrderBy(c => c.Resolved), null);
        }
    }
}